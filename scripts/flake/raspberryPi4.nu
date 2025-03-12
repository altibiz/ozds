#!/usr/bin/env nu

let dir = $env.FILE_PWD
let self = [ $dir "raspberryPi4.nu" ] | path join
let root = $dir | path dirname | path dirname
let artifacts = [ $root "artifacts" ] | path join
let flake = $"git+file:($root)"
let system = "aarch64-linux"
let format = "sd-aarch64"
let configuration = $"raspberryPi4-($system)"
let uri = $"($flake)#($configuration)"

def "main" [] {
  nu $self --help
}

def "main vpn" [] {
  let host = open --raw /etc/hostname | str trim

  let config = vault kv get -format=json "kv/ozds/vpn"
    | from json
    | get data.data
    | get $host

  let file = mktemp -t
  chmod 600 $file
  $config | save -f $file

  sudo nebula -config $file

  rm -f $file
}

def "main secrets" [] {
  rm -rf $artifacts
  mkdir $artifacts
  cd $artifacts

  spec | rumor stdin json --stay
}

def "main image" [] {
  rm -rf $artifacts
  mkdir $artifacts
  cd $artifacts

  let raw = (nixos-generate
    --system $system
    --format $format
    --flake $configuration)

  let compressed = ls ($raw
    | path dirname --num-levels 2
    | path join "sd-image")
    | get name
    | first
  unzstd $compressed -o image.img
  chmod 644 image.img

  let age = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.age-priv
    | str replace -a "\\" "\\\\"
    | str replace -a "\n" "\\n"
    | str replace -a "\"" "\\\""

  let commands = $"run
mount /dev/sda2 /
mkdir-p /root
chmod 700 /root
write /root/.sops.age \"($age)\"
chmod 400 /root/.sops.age
exit"

  echo $commands | guestfish --rw -a image.img
}

def "main ssh" [] {
  let key = vault secrets
    | get user-ssh-priv
    | str trim

  let ip = lib secrets
    | get ip
    | str trim

  ssh-agent bash -c $"echo '($key)' \\
    | ssh-add - \\
    && ssh altibiz@($ip)"
}

def "main pass" [] {
  vault secrets
    | get user-pass-priv
    | str trim
}

def "main deploy" [] {
  let secrets = vault secrets

  let key = $secrets
    | get user-ssh-priv
    | str trim

  let pass = $secrets
    | get user-pass-priv
    | str trim

  let ip = lib secrets
    | get ip
    | str trim

  ssh-agent bash -c $"echo '($key)' \\
    | ssh-add - \\
    && export SSHPASS='($pass)' \\
    && sshpass -e deploy \\
      --skip-checks \\
      --interactive-sudo true \\
      --hostname ($ip) \\
      -- \\
      '($root)#($configuration)'"
}

def --wrapped "main s3" [...args] {
  let secrets = vault kv get -format=json "kv/ozds/nix/s3.lvm.altibiz.com"
    | from json
    | get data.data

  with-env {
    AWS_ACCESS_KEY_ID: ($secrets."admin-aws-access-key-id"),
    AWS_SECRET_ACCESS_KEY: ($secrets."admin-aws-secret-access-key")
  } {
    (s3cmd
      --host=s3.lvm.altibiz.com
      "--host-bucket=s3.lvm.altibiz.com/%(bucket)"
      ...($args))
  }
}

def "main cache" [id?: string] {
  let secrets = vault kv get -format=json "kv/ozds/nix/s3.lvm.altibiz.com"
    | from json
    | get data.data

  let file = mktemp -t
  chmod 600 $file
  $secrets."private.pem" | save -f $file

  with-env {
    AWS_ACCESS_KEY_ID: ($secrets."aws-access-key-id"),
    AWS_SECRET_ACCESS_KEY: ($secrets."aws-secret-access-key")
  } {
    let cache = $"s3://nix-binary-cache?endpoint=s3.lvm.altibiz.com&secret-key=($file)"
    let ozds = $"($root)#nixosConfigurations.($configuration).config.system.build.toplevel"
    (nix copy --to $cache $ozds)
  }

  rm -f $file
}

def "main db user" [] {
  let pass = vault secrets
    | get postgres-user-pass
    | str trim

  let ip = lib secrets
    | get ip
    | str trim

  let auth = $"altibiz:($pass)"
  let conn = $"($ip):5432"

  usql $"postgres://($auth)@($conn)/ozds"
}

def "main db admin" [] {
  let pass = vault secrets
    | get postgres-pass
    | str trim

  let ip = lib secrets
    | get ip
    | str trim

  let auth = $"postgres:($pass)"
  let conn = $"($ip):5432"

  usql $"postgres://($auth)@($conn)/postgres"
}

def "spec" [] {
  let expr = $"\(builtins.getFlake \"($flake)\"\).lib.rumor.\"($configuration)\""
  let spec = nix eval --json --impure --expr $expr
  $spec | from json
}

def "lib secrets" [] {
  let expr = $"\(builtins.getFlake \"($flake)\"\).lib.secrets.\"($configuration)\""
  let secrets = nix eval --json --impure --expr $expr
  $secrets | from json
}

def "vault secrets" [] {
  vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data
}

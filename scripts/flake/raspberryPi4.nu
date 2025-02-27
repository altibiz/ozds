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

def "main secrets" [] {
  rm -rf $artifacts
  mkdir $artifacts
  cd $artifacts

  let expr = $"\(builtins.getFlake \"($flake)\"\).lib.rumor.\"($configuration)\""
  let spec = nix eval --json --impure --expr $expr
  $spec | rumor stdin json --stay
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
    | str replace "\\" "\\\\"
    | str replace "\n" "\\n"
    | str replace "\"" "\\\""

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
  let key = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-ssh-priv
    | str trim

  ssh-agent bash -c $"echo '($key)' \\
    | ssh-add - \\
    && ssh altibiz@192.168.1.69"
}

def "main pass" [] {
  vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-pass-priv
    | str trim
}

def "main deploy" [] {
  let key = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-ssh-priv
    | str trim

  let pass = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-pass-priv
    | str trim

  ssh-agent bash -c $"echo '($key)' \\
    | ssh-add - \\
    && export SSHPASS='($pass)' \\
    && sshpass -e deploy \\
      --remote-build \\
      --skip-checks \\
      --interactive-sudo true \\
      --hostname 192.168.1.69 \\
      -- \\
      '($root)#($configuration)'"
}

def "main db user" [] {
  let pass = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.postgres-user-pass
    | str trim

  let auth = $"altibiz:($pass)"
  let conn = $"192.168.1.69:5432"

  usql $"postgres://($auth)@($conn)/ozds"
}

def "main db admin" [] {
  let pass = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.postgres-pass
    | str trim

  let auth = $"postgres:($pass)"
  let conn = $"192.168.1.69:5432"

  usql $"postgres://($auth)@($conn)/postgres"
}

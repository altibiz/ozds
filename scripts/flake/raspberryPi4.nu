#!/usr/bin/env nu

let dir = $env.FILE_PWD
let self = [ $dir "raspberryPi4.nu" ] | path join
let root = $dir | path dirname | path dirname
let artifacts = [ $root "artifacts" ] | path join
let flake = $"git+file:($root)"
let system = "aarch64-linux"
let format = "sd-aarch64"
let configuration = $"($flake)#raspberryPi4-($system)"

def "main" [] {
  nu $self --help
}

def "main secrets" [] {
  rm -rf $artifacts
  mkdir $artifacts
  cd $artifacts

  let expr = $"\(builtins.getFlake \"($flake)\"\).lib.rumor.\"raspberryPi4-aarch64-linux\""
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
  let temp = mktemp -t
  chmod 600 $temp
  let key = vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-ssh-priv
    | str trim
  $"($key)\n" | save -f $temp
  try { ssh -i $temp altibiz@192.168.1.69 }
  rm $temp
}

def "main pass" [] {
  vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.user-pass-priv
    | str trim
}

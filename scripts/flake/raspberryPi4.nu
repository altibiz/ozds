#!/usr/bin/env nu

let dir = $env.FILE_PWD
let self = [ $dir "raspberryPi4.nu" ] | path join
let root = $dir | path dirname | path dirname
let artifacts = [ $root "artifacts" ] | path join
let age = [ $artifacts ".sops.age" ] | path join
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
  print $raw

  let compressed = ls ($raw
    | path dirname --num-levels 2
    | path join "sd-image")
    | get name
    | first
  unzstd $compressed -o image.img
  chmod 644 image.img

  vault kv get -format=json kv/ozds/ozds/test/current
    | from json
    | get data.data.age-priv
    | save -f $age 

  let commands = $"run
mount /dev/sda2 /
mkdir-p /root
chmod 700 /root
upload ($age) /root/.sops.age
chmod 400 /root/.sops.age
exit"

  echo $commands | guestfish --rw -a image.img
}

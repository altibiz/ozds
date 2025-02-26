#!/usr/bin/env nu

let dir = $env.FILE_PWD
let root = $dir | path dirname | path dirname
let artifacts = [ $root "artifacts" ] | path join
let secrets = [ $dir "raspberryPi4.yaml" ] | path join
let flake = $"git+file:($root)"
let system = "aarch64-linux"
let format = "sd-aarch64"
let configuration = $"($flake)#raspberryPi4-($system)"

rm -rf $artifacts
mkdir $artifacts
cd $artifacts

def "main secrets" [] {
  let expr = $"\(builtins.getFlake \"($flake)\"\).lib.rumor.\"raspberryPi4-aarch64-linux\""
  let spec = nix eval --json --impure --expr $expr
  $spec | rumor stdin json --stay
}

def "main image" [] {
  let compressed = ls (nixos-generate
    --system $system
    --format $format
    --flake $configuration
    --option "--no-out-link"
    | path dirname --num-levels 2
    | path join "sd-image")
    | get name
    | first
  unzstd $compressed -o image.img
  chmod 644 image.img

  let commands = $"run
mount /dev/sda2 /
mkdir /root
chmod 700 /root
upload ($secrets) /root/.sops.age
chmod 400 /root/.sops.age
exit"

  echo $commands | guestfish --rw -a image.img
}

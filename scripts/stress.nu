#!/usr/bin/env nu

def main [] { }

let pwd = pwd
let root =  $env.FILE_PWD | path dirname;
let ozds = $root
let pidgeon = "github.com/altibiz/pidgeon";
let test_dir = [ $root "test" "stress" ] | path join
let test_file = [ $test_dir "project.gns3" ] | path join
let project_files_dir = [ $test_dir "project-files" ] | path join
let test_zip = [ (mktemp -d) "project.gns3project" ] | path join
let project_id = open $"($test_dir)/project.gns3" | from json | get project_id

def "main import" [base: string = "http://localhost:3080"] {
  sudo docker pull frrouting/frr:v8.4.0
  open --raw (nix build $"($pidgeon)#docker" --print-out-paths --no-link) | sudo docker load
  open --raw (nix build $"($ozds)#docker" --print-out-paths --no-link) | sudo docker load

  cd $test_dir
  ^zip -r $test_zip . | ignore
  cd $pwd

  curl -s -X DELETE $"($base)/v2/projects/($project_id)" | ignore

  (curl -s -X POST
    -H 'Content-Type:application/x-www-form-urlencoded'
    --data-binary $"@($test_zip)"
    $"($base)/v2/projects/($project_id)/import")
  print $"Imported project at: '($base)/static/web-ui/server/1/project/($project_id)'"
}

def "main export" [base: string = "http://localhost:3080"] {
  curl -s -X POST $"($base)/v2/projects/($project_id)/close"
  curl -s $"($base)/v2/projects/($project_id)/export" -o $test_zip
  rm -rf $test_dir
  unzip $test_zip -d $test_dir | ignore

  prettier --write $test_file | ignore

  print $"Exported project at: 'http://localhost:3080/static/web-ui/server/1/project/($project_id)'"
}

rm -f $test_zip

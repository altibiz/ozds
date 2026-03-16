#!/usr/bin/env nu

let test_dir = ($env.FILE_PWD | path join ".." ".." "test" | path expand)

def find-project [name: string] {
  let dirs = (ls $test_dir
    | where type == "dir"
    | get name
    | each { $in | path basename })

  let exact = ($dirs | where { $in == $name })
  if ($exact | is-not-empty) {
    return ($exact | first)
  }

  let with_suffix = ($dirs | where { $in == $"($name).Test" })
  if ($with_suffix | is-not-empty) {
    return ($with_suffix | first)
  }

  let with_prefix = ($dirs | where { $in == $"Ozds.($name).Test" })
  if ($with_prefix | is-not-empty) {
    return ($with_prefix | first)
  }

  let partial = ($dirs | where { $in | str contains -i $name })
  if ($partial | is-not-empty) {
    if ($partial | length) > 1 {
      print $"Ambiguous project name '($name)'. Matches:"
      $partial | each { print $"  - ($in)" }
      exit 1
    }
    return ($partial | first)
  }

  print $"No test project found matching '($name)'"
  print "Available projects:"
  $dirs | each { print $"  - ($in)" }
  exit 1
}

def run-test [project: string, extra_args: list<string>]: nothing -> record {
  let csproj = ($test_dir | path join $project $"($project).csproj")
  let result = (dotnet test $csproj ...$extra_args | complete)
  {
    stdout: $result.stdout
    stderr: $result.stderr
    passed: ($result.exit_code == 0)
  }
}

def "main" [
  name: string
  ...args: string
] {
  let project = (find-project $name)
  print $"Running tests for ($project)"
  let result = (run-test $project $args)
  print $result.stdout
  if ($result.stderr | str trim | is-not-empty) {
    print $result.stderr
  }
  if (not $result.passed) { exit 1 }
}

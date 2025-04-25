#!/usr/bin/env nu

let root = $env.FILE_PWD | path dirname --num-levels 2
let artifacts = [ $root "artifacts" ] | path join

def main [splits?: int] {
  rm -rf $artifacts
  mkdir $artifacts

  let files = fd --type file -E "*.md" ".*" $root | lines | sort 

  let splits = if $splits == null { 5 } else { $splits }

  $files
    | chunks ((($files | length) / $splits) | into int)
    | enumerate
    | each { |x|
        let contents = $x.item
          | each { $"## ($in)\n\n```\n(open --raw $in | str trim)\n```" }
          | str join "\n\n"
        $"# OZDS ($x.index)/($splits)\n\n($contents)"
      }
    | enumerate
    | each { |x|
        $x.item
          | save -f $"($artifacts)/split-($x.index).md"
      }

  ls $artifacts
    | get name
    | where { $in | path basename | str starts-with "split-" }
    | where { $in | path basename | str ends-with ".md" }
    | each {
        let index = $in
          | path basename
          | parse "split-{index}.md"
          | get index
          | first
        let basename = $"split-($index).pdf"
        let output = [ ($in | path dirname) $basename ]
          | path join;
        (pandoc
          --from=markdown+rebase_relative_paths
          --to=pdf
          --standalone
          --table-of-contents
          $"--output=($output)"
          $in)
      }
}

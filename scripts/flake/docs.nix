{ pkgs, ... }:

{
  integrate.devShell.devShell =
    pkgs.mkShell {
      packages = with pkgs; [
        # Scripts
        just
        nushell

        # C#
        dotnet-sdk
        dotnet-runtime
        dotnet-aspnetcore

        # Documentation
        mdbook
        openjdk
        plantuml
        graphviz
        mdbook-plantuml
        pandoc-plantuml-filter
      ];
    };
}

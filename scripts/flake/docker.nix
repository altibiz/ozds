{ self, pkgs, ... }:

{
  integrate.package.package =
    pkgs.pkgs.dockerTools.buildImage {
      name = "altibiz/ozds";
      tag = "latest";
      created = "now";
      copyToRoot = pkgs.buildEnv {
        name = "image-root";
        paths = [ self.packages.${pkgs.system}.default ];
        pathsToLink = [ "/bin" ];
      };
      config = {
        Cmd = [ "ozds" ];
      };
    };
}

{ self, ... }:

{
  flake.lib.playwright.pkgs = pkgs: {
    node = pkgs.nodejs;
    browsers =
      if pkgs.hostPlatform.isLinux
      then
        pkgs.playwright-driver.browsers.override
          {
            withFirefox = false;
            withWebkit = false;
          }
      else pkgs.playwright-driver.browsers;
  };

  flake.lib.playwright.env = pkgs:
    let
      actual = self.lib.playwright.pkgs pkgs;
    in
    {
      PLAYWRIGHT_NODEJS_PATH = "${actual.node}/bin/node";
      PLAYWRIGHT_BROWSERS_PATH = "${actual.browsers}";
    };
}

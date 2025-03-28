{ self, perch, nixpkgsPlaywright, ... }:

{
  flake.packages =
    builtins.listToAttrs
      (builtins.map
        (system:
          let
            playwrightPkgs = import nixpkgsPlaywright {
              inherit system;
            };
          in
          {
            name = system;
            value = {
              playwrightNode = playwrightPkgs.nodejs;
              playwrightBrowsers =
                if playwrightPkgs.hostPlatform.isLinux
                then
                  playwrightPkgs.playwright-driver.browsers.override
                    {
                      withFirefox = false;
                      withWebkit = false;
                    }
                else playwrightPkgs.playwright-driver.browsers;
            };
          })
        perch.lib.defaults.systems);

  flake.lib.playwright.env = system: {
    PLAYWRIGHT_NODEJS_PATH =
      "${self.packages.${system}.playwrightNode}/bin/node";
    PLAYWRIGHT_BROWSERS_PATH =
      "${self.packages.${system}.playwrightBrowsers}";
  };
}

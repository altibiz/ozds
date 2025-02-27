{ self, pkgs, config, lib, ... }:

{
  seal.defaults.overlay = "ozds";
  seal.overlays.ozds = (final: prev: {
    dotnet-sdk = prev.dotnet-sdk_8;
    dotnet-runtime = prev.dotnet-runtime_8;
    dotnet-aspnetcore = prev.dotnet-aspnetcore_8;
  });

  seal.defaults.package = "ozds";
  integrate.package.package =
    let
      pname = "ozds";
    in
    pkgs.buildDotnetModule {
      pname = pname;
      version = "0.1.0";

      src = self;
      projectFile = "src/Ozds.Server/Ozds.Server.csproj";
      nugetDeps = ./deps.nix;
      executables = [ "Ozds.Server" ];
      makeWrapperArgs = [
        "--set DOTNET_CONTENTROOT ${placeholder "out"}/lib/${pname}"
      ] ++ lib.mapAttrsToList
        (name: value: "--set ${name} ${value}")
        (self.lib.playwright.env pkgs);
      buildInputs = builtins.attrValues (self.lib.playwright.pkgs pkgs);

      dotnet-sdk = pkgs.dotnet-sdk;
      dotnet-runtime = pkgs.dotnet-aspnetcore;

      meta = {
        description = "ozds";
        homepage = "https://github.com/altibiz/ozds";
        license = pkgs.lib.licenses.mit;
        mainProgram = "Ozds.Server";
      };
    };

  seal.defaults.nixosModule = "ozds";
  branch.nixosModule.nixosModule =
    let
      cfg = config.services.ozds;
    in
    {
      options.services.ozds = {
        enable = lib.mkEnableOption "ozds";

        package = lib.mkOption {
          type = lib.types.package;
          default = self.packages.${pkgs.system}.default;
          description = "Package to use for the ozds service.";
        };

        user = lib.mkOption {
          type = lib.types.str;
          description = "User under which the ozds server runs.";
          default = "ozds";
        };

        group = lib.mkOption {
          type = lib.types.str;
          description = "Group under which the ozds server runs.";
          default = "ozds";
        };

        stateDir = lib.mkOption {
          type = lib.types.str;
          default = "/var/lib/ozds";
          description = "State directory the ozds server will use.";
        };

        environment = lib.mkOption {
          type = lib.types.attrsOf (
            lib.types.oneOf [
              lib.types.bool
              lib.types.int
              lib.types.str
            ]
          );
          default = { };
          description = "Extra environment variables passed to ozds server";
        };

        environmentFile = lib.mkOption {
          type = lib.types.str;
          default = null;
          description = "Path to environment variables file to set for the ozds service.";
        };

        httpPort = lib.mkOption {
          type = lib.types.port;
          default = 5000;
          description = "HTTP port to listen on";
        };

        httpsPort = lib.mkOption {
          type = lib.types.port;
          default = 5001;
          description = "HTTPS port to listen on";
        };

        openFirewall = lib.mkOption {
          type = lib.types.bool;
          default = false;
          description = "Open ports needed for the ozds service.";
        };
      };

      config = lib.mkIf cfg.enable {
        environment.systemPackages = [
          self.packages.${pkgs.system}.default
        ];

        users.groups.ozds = {
          name = cfg.group;
        };
        users.users.ozds = {
          isSystemUser = true;
          name = cfg.user;
          group = cfg.group;
        };

        networking.firewall.allowedTCPPorts =
          lib.mkIf cfg.openFirewall [ cfg.httpPort cfg.httpsPort ];

        systemd.services.ozds = {
          description = "ozds";
          after = [ "network.target" ];
          wantedBy = [ "multi-user.target" ];
          environment =
            lib.mapAttrs
              (_: value:
                if lib.isBool value
                then lib.boolToString value
                else builtins.toString value)
              (cfg.environment // {
                ASPNETCORE_URLS =
                  "http://0.0.0.0:${builtins.toString cfg.httpPort}"
                    + ";https://0.0.0.0:${builtins.toString cfg.httpsPort}";
              });
          serviceConfig = {
            EnvironmentFile = cfg.environmentFile;
            ExecStart = lib.getExe cfg.package;
            Restart = "always";
            User = cfg.user;
            Group = cfg.group;
            AmbientCapabilities = lib.mkIf
              (cfg.httpPort < 1024 || cfg.httpsPort < 1024)
              [ "CAP_NET_BIND_SERVICE" ];
            StateDirectory = builtins.baseNameOf cfg.stateDir;
            WorkingDirectory = cfg.stateDir;
            UMask = "0077";
          } // lib.optionalAttrs
            (cfg.environmentFile != null)
            {
              EnvironmentFile = cfg.environmentFile;
            } // {
            ProtectSystem = "strict";
            ProtectHome = true;
            PrivateTmp = true;
            NoNewPrivileges = true;
            RestrictSUIDSGID = true;
            ProtectKernelTunables = true;
            ProtectKernelModules = true;
            ProtectControlGroups = true;
            ReadWritePaths = [ cfg.stateDir "/tmp" ];
            RestrictAddressFamilies = "AF_INET AF_INET6";
            PrivateDevices = true;
            LockPersonality = true;
            RestrictRealtime = true;
            CapabilityBoundingSet = [ "CAP_NET_BIND_SERVICE" ];
            ProtectClock = true;
            ProtectHostname = true;
          };
        };
      };
    };
}

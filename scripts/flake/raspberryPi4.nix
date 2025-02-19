{ self
, lib
, pkgs
, config
, nixos-hardware
, sops-nix
, ...
}:

let
  sops = self.lib.sops.raspberryPi4;
  secrets = sops.secrets;
in
{
  integrate.nixosConfiguration = {
    systems = [ "aarch64-linux" ];

    nixosConfiguration = {
      imports = [
        nixos-hardware.nixosModules.raspberry-pi-4
        sops-nix.nixosModules.default
        self.nixosModules.default
      ];

      # system 

      system.stateVersion = "24.11";

      sops.defaultSopsFile = lib.path.append ./. sops.filePrefix;
      sops.age.keyFile = sops.ageKeyFile;

      nix.extraOptions = "experimental-features = nix-command flakes";
      nix.gc.automatic = true;
      nix.gc.options = "--delete-older-than 30d";
      nix.settings.auto-optimise-store = true;
      nix.settings.trusted-users = [ "@wheel" ];
      nix.package = pkgs.nixVersions.stable;

      fileSystems."/firmware" = {
        device = "/dev/disk/by-label/FIRMWARE";
        fsType = "vfat";
      };
      fileSystems."/" = {
        device = "/dev/disk/by-label/NIXOS_SD";
        fsType = "ext4";
      };

      environment.systemPackages = with pkgs; [
        libraspberrypi
        raspberrypi-eeprom
        man-pages
        man-pages-posix
      ];

      services.fstrim.enable = true;

      # postgresql

      services.postgresql.enable = true;
      services.postgresql.package = pkgs.postgresql_16;
      services.postgresql.extensions = with config.services.postgresql.package.pkgs; [
        timescaledb
      ];
      services.postgresql.settings.shared_preload_libraries = "timescaledb";

      services.postgresql.authentication = pkgs.lib.mkOverride 10 ''
        # NOTE: do not remove local privileges because that breaks timescaledb
        # TYPE    DATABASE    USER        ADDRESS         METHOD        OPTIONS
        local     all         all                         trust
        host      all         all         samehost        trust
        hostssl   all         all         192.168.0.0/16  scram-sha-256
        hostssl   all         all         10.8.0.0/16     scram-sha-256
      '';
      services.postgresql.enableTCPIP = true;
      services.postgresql.settings.port = 5433;
      networking.firewall.allowedTCPPorts = [ 5433 ];

      services.postgresql.settings.ssl = "on";
      services.postgresql.settings.ssl_cert_file =
        config.sops.secrets.${secrets.postgresSslCertFile.key}.path;
      sops.secrets.${secrets.postgresSslCertFile.key} = {
        owner = config.systemd.services.postgresql.serviceConfig.User;
        group = config.systemd.services.postgresql.serviceConfig.Group;
      };
      services.postgresql.settings.ssl_key_file =
        config.sops.secrets.${secrets.postgresSslKeyFile.key}.path;
      sops.secrets.${secrets.postgresSslKeyFile.key} = {
        owner = config.systemd.services.postgresql.serviceConfig.User;
        group = config.systemd.services.postgresql.serviceConfig.Group;
      };
      services.postgresql.initialScript =
        config.sops.secrets.${secrets.postgresInitialScript.key}.path;
      sops.secrets.${secrets.postgresInitialScript.key} = {
        owner = config.systemd.services.postgresql.serviceConfig.User;
        group = config.systemd.services.postgresql.serviceConfig.Group;
      };

      services.postgresql.settings = {
        checkpoint_timeout = "30min";
        checkpoint_completion_target = 0.9;
        max_wal_size = "1GB";
      };

      # network

      networking.firewall.enable = true;
      networking.networkmanager.enable = true;
      networking.nameservers = [ "1.1.1.1" "1.0.0.1" ];

      networking.networkmanager.wifi.powersave = false;
      networking.networkmanager.ensureProfiles.profiles = {
        "wifi" = {
          connection = {
            id = "wifi";
            permissions = "";
            type = "wifi";
            interface-name = "wlan0";
          };
          ipv4 = {
            dns-search = "";
            method = "auto";
          };
          ipv6 = {
            addr-gen-mode = "stable-privacy";
            dns-search = "";
            method = "auto";
          };
          wifi = {
            mac-address-blacklist = "";
            mode = "infrastructure";
            ssid = "$WIFI_SSID";
          };
          wifi-security = {
            auth-alg = "open";
            key-mgmt = "wpa-psk";
            psk = "$WIFI_PASS";
          };
        };
      };

      networking.networkmanager.ensureProfiles.environmentFiles = [
        config.sops.secrets.${secrets.networkManagerEnvironmentFile.key}.path
      ];

      sops.secrets.${secrets.networkManagerEnvironmentFile.key} = { };

      # vpn

      services.nebula.networks.ozds-vpn = {
        enable = true;
        cert = config.sops.secrets.${secrets.nebulaCert.key}.path;
        key = config.sops.secrets.${secrets.nebulaKey.key}.path;
        ca = config.sops.secrets.${secrets.nebulaCa.key}.path;
        firewall.inbound = [
          {
            host = "any";
            port = "any";
            proto = "any";
          }
        ];
        firewall.outbound = [
          {
            host = "any";
            port = "any";
            proto = "any";
          }
        ];
        lighthouses = [ "10.8.0.1" ];
        staticHostMap = {
          "10.8.0.1" = [
            "ozds-vpn.altibiz.com:4242"
          ];
        };
        settings = {
          relay = {
            relays = [ "10.8.0.1" ];
          };
          punchy = {
            punch = true;
            delay = "1s";
            respond = true;
            respond_delay = "5s";
          };
          handshakes = {
            try_interval = "1s";
          };
          static_map = {
            cadence = "1m";
            lookup_timeout = "10s";
          };
          logging = {
            level = "debug";
          };
        };
      };

      sops.secrets.${secrets.nebulaCert.key} = {
        owner = config.systemd.services."nebula@ozds-vpn".serviceConfig.User;
        group = config.systemd.services."nebula@ozds-vpn".serviceConfig.Group;
      };

      sops.secrets.${secrets.nebulaKey.key} = {
        owner = config.systemd.services."nebula@ozds-vpn".serviceConfig.User;
        group = config.systemd.services."nebula@ozds-vpn".serviceConfig.Group;
      };

      sops.secrets.${secrets.nebulaCa.key} = {
        owner = config.systemd.services."nebula@ozds-vpn".serviceConfig.User;
        group = config.systemd.services."nebula@ozds-vpn".serviceConfig.Group;
      };

      # user

      services.openssh.enable = true;
      services.openssh.settings.PasswordAuthentication = false;

      programs.direnv.enable = true;
      programs.direnv.nix-direnv.enable = true;

      users.mutableUsers = false;
      users.users.altibiz = {
        hashedPasswordFile =
          config.sops.secrets.${secrets.userHashedPasswordFile.key}.path;
        extraGroups = [ "wheel" "dialout" ];
        packages = [
          pkgs.kitty
          pkgs.git
          pkgs.helix
          pkgs.yazi
          pkgs.lazygit
          pkgs.nushell
        ];
      };
      sops.secrets.${secrets.userHashedPasswordFile.key}.neededForUsers = true;

      sops.secrets.${secrets.userAuthorizedKeys.key} = {
        path = "${config.users.users.altibiz.home}/.ssh/authorized_keys";
        owner = config.users.users.altibiz.name;
        group = config.users.users.altibiz.group;
      };

      # service

      services.ozds.enable = true;
      services.ozds.environmentFile =
        config.sops.secrets.${secrets.ozdsEnv.key}.path;
      sops.secrets.${secrets.ozdsEnv.key} = { };
    };
  };
}

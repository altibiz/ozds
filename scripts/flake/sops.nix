{
  flake.lib.sops.raspberryPi4 = rec {
    secretsPrefix = "secrets";
    filePrefix = "scripts/flake/sops.yaml";
    ageKeyFile = "/root/.sops.age";

    secrets.postgresSslKeyFile.key = "postgres.crt";
    secrets.postgresSslKeyFile.from =
      generations.postgresSsl.args.priv;
    secrets.postgresSslCertFile.key = "postgres.crt.pub";
    secrets.postgresSslCertFile.from =
      generations.postgresSsl.args.pub;
    secrets.postgresInitialScript.key = "postgres.sql";
    secrets.postgresInitialScript.from =
      generations.postgresInitialScript.args.name;

    secrets.networkManagerEnvironmentFile.key = "wifi.env";
    secrets.networkManagerEnvironmentFile.from =
      generations.networkManagerEnvironmentFile.args.name;

    secrets.nebulaKey.key = "nebula.crt";
    secrets.nebulaKey.from =
      generations.nebulaSsl.args.priv;
    secrets.nebulaCert.key = "nebula.crt.pub";
    secrets.nebulaCert.from =
      generations.nebulaSsl.args.pub;
    secrets.nebulaCa.key = "nebula.ca.pub";
    secrets.nebulaCa.from =
      generations.nebulaSsl.args.ca;

    secrets.userHashedPasswordFile.key = "user.pass.pub";
    secrets.userHashedPasswordFile.from =
      generations.userHashedPasswordFile.args.pub;

    secrets.userAuthorizedKeys.key = "user.ssh.pub";
    secrets.userAuthorizedKeys.from =
      generations.userAuthorizedKeys.args.pub;

    secrets.ozdsEnv.key = "ozds.env";
    secrets.ozdsEnv.from = "ozdsEnv";

    generations.postgresSsl.generator = "openssl-cert";
    generations.postgresSsl.args = {
      ca = "ozds-postgres.ca.pub";
      priv = "postgres.crt";
      pub = "postgres.crt.pub";
    };
    generations.postgresInitialScript.generator = "ozds-postgresql";
    generations.postgresInitialScript.args = {
      name = "postgres.sql";
    };
    generations.networkManagerEnvironmentFile.generator = "env";
    generations.networkManagerEnvironmentFile.args = {
      name = "wifi.env";
      vars = {
        WIFI_SSID = "wifi.ssid";
        WIFI_PASS = "wifi.pass";
      };
    };
    generations.wifiSsid.generator = "id";
    generations.nebulaSsl.generator = "nebula-cert";
    generations.nebulaSsl.args = {
      ca = "ozds-nebula.ca.pub";
      priv = "nebula.crt";
      pub = "nebula.crt.pub";
    };
    generations.nebulaCa.value = "ozds-nebula.ca.pub";
    generations.nebulaCa.generator = "nebula-ca";
    generations.nebulaCa.args = {
      priv = "nebula.crt.pub";
      pub = "nebula.crt.pub";
    };
    generations.userHashedPasswordFile.generator = "mkpasswd";
    generations.userHashedPasswordFile.args = {
      pub = "user.pass.pub";
      priv = "user.pass";
    };
    generations.userAuthorizedKeys.generator = "ssh-keygen";
    generations.userAuthorizedKeys.args = {
      pub = "authorized_keys.pub";
      priv = "authorized_keys";
    };
    generations.ozdsEnv.generator = "ozds-env";
  };
}

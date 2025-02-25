let
  secrets = {
    filePrefix = "scripts/flake/secrets.yaml";
    ageKeyFile = "/root/.sops.age";
    postgresSslKeyFile = "postgres.crt";
    postgresSslCertFile = "postgres.crt.pub";
    postgresInitialScript = "postgres.sql";
    networkManagerEnvironmentFile = "wifi.env";
    nebulaKey = "nebula.crt";
    nebulaCert = "nebula.crt.pub";
    nebulaCa = "nebula.ca.pub";
    userHashedPasswordFile = "user.pass.pub";
    userAuthorizedKeys = "user.ssh.pub";
    ozdsEnv = "ozds.env";
  };

  rumor = {
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
in
{
  flake.lib.secrets."raspberryPi4-aarch64-linux" = secrets;

  flake.lib.rumor."raspberryPi4-aarch64-linux" = rumor;
}

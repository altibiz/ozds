{
  flake.lib.sops.raspberryPi4 = {
    secretsPrefix = "secrets";
    filePrefix = "scripts/flake/sops.yaml";
    ageKeyFile = "/root/.sops.age";

    secrets.postgresSslKeyFile.key = "postgres.crt";
    secrets.postgresSslKeyFile.generator = "openssl-cert";
    secrets.postgresSslKeyFile.args = {
      ca = "ozds-postgres.ca.pub";
    };

    secrets.postgresSslCertFile.key = "postgres.crt.pub";
    secrets.postgresSslCertFile.generator = "openssl-cert-pub";
    secrets.postgresSslCertFile.args = {
      ca = "ozds-postgres.ca.pub";
    };

    secrets.postgresInitialScript.key = "postgres.sql";
    secrets.postgresInitialScript.generator = "ozds-postgresql";

    secrets.networkManagerEnvironmentFile.key = "wifi.env";
    secrets.networkManagerEnvironmentFile.generator = "wifi-env";

    secrets.nebulaKey.key = "nebula.crt";
    secrets.nebulaKey.generator = "nebula-cert";
    secrets.nebulaKey.args = {
      ca = "ozds-nebula.ca.pub";
    };

    secrets.nebulaCert.key = "nebula.crt.pub";
    secrets.nebulaCert.generator = "nebula-cert-pub";
    secrets.nebulaCert.args = {
      ca = "ozds-nebula.ca.pub";
    };

    secrets.nebulaCa.key = "nebula.ca.pub";
    secrets.nebulaCa.value = "ozds-nebula.ca.pub";

    secrets.userHashedPasswordFile.key = "user.pass.pub";
    secrets.userHashedPasswordFile.generator = "mkpasswd";

    secrets.userAuthorizedKeys.key = "user.ssh.pub";
    secrets.userAuthorizedKeys.generator = "ssh-keygen";

    secrets.ozdsEnv.key = "ozds.env";
    secrets.ozdsEnv.generator = "ozds-env";
  };
}

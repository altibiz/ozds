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

  rumor.imports = [
    {
      importer = "vault";
      arguments.path = "kv/ozds/ozds/test";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "postgres-ca-priv";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "postgres-ca-pub";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "postgres-ca-srl";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "nebula-ca-priv";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "nebula-ca-pub";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/pidgeon/test";
      arguments.file = "wifi-ssid";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/pidgeon/test";
      arguments.file = "wifi-pass";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-email-address";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-email-password";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-email-host";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-email-port";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-email-username";
      arguments.allow_fail = true;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = "ozds-test-messaging-connection-string";
      arguments.allow_fail = true;
    }
  ];

  rumor.exports = [
    {
      exporter = "vault";
      arguments.path = "kv/ozds/ozds/test";
    }
  ];

  rumor.generations = [
    {
      generator = "openssl";
      arguments = {
        ca_private = "postgres-ca-priv";
        ca_public = "postgres-ca-pub";
        serial = "postgres-ca-srl";
        name = "ozds test rpi4 postgres";
        private = secrets.postgresSslCertFile;
        public = secrets.postgresSslKeyFile;
      };
    }
    {
      generator = "key";
      arguements = {
        name = "ozds-postgres-pass";
        length = 32;
      };
    }
    {
      generator = "key";
      arguements = {
        name = "altibiz-postgres-pass";
        length = 32;
      };
    }
    {
      generator = "key";
      arguements = {
        name = "postgres-pass";
        length = 32;
      };
    }
    {
      generator = "moustache";
      arguments = {
        name = secrets.postgresInitialScript;
        variables = {
          POSTGRES_PASS = "postgres-pass";
          OZDS_POSTGRES_PASS = "ozds-postgres-pass";
          ALTIBIZ_POSTGRES_PASS = "altibiz-postgres-fpass";
        };
        template = ''
          ALTER USER postgres WITH PASSWORD '{{POSTGRES_PASS}}';

          CREATE USER ozds PASSWORD '{{OZDS_POSTGRES_PASS}}';
          CREATE USER altibiz PASSWORD '{{ALTIBIZ_POSTGRES_PASS}}';

          CREATE DATABASE ozds;
          ALTER DATABASE ozds OWNER TO ozds;

          \c ozds

          GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO altibiz;
          GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO altibiz;
          GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO altibiz;
        '';
      };
    }
    {
      generator = "env";
      arguments = {
        name = secrets.networkManagerEnvironmentFile;
        variables = {
          WIFI_SSID = "wifi-ssid";
          WIFI_PASS = "wifi-pass";
        };
      };
    }
    {
      generator = "nebula";
      arguments = {
        ca_private = "nebula-ca-priv";
        ca_public = secrets.nebulaCa;
        name = "nebula";
        private = secrets.nebulaKey;
        public = secrets.nebulaCert;
      };
    }
    {
      generator = "mkpasswd";
      arguments = {
        public = secrets.userHashedPasswordFile;
        private = "altibiz-pass";
      };
    }
    {
      generator = "ssh-keygen";
      arguments = {
        public = secrets.userAuthorizedKeys;
        private = "altibiz-ssh-priv";
      };
    }
    {
      generator = "moustache";
      arguements = {
        name = "ozds-connection-string";
        variables = {
          OZDS_POSTGRES_PASS = "ozds-postgres-pass";
        };
        template = "Server=localhost;Port=5432;Database=ozds;User Id=ozds;Password={{OZDS_POSTGRES_PASS}};Ssl Mode=Disable;";
      };
    }
    {
      generator = "key";
      arguments = {
        name = "ozds-admin";
        length = 32;
      };
    }
    {
      generator = "env";
      arguments = {
        name = secrets.ozdsEnv;
        variables = {
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminEmail = "hrvoje@altibiz.com";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminPassword = "ozds-admin";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminUsername = "admin";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseConnectionString = "ozds-connection-string";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseProvider = "Postgres";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseTablePrefix = "";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__RecipeName = "ozds";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__ShellName = "Default";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__SiteName = "OZDS";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__SiteTimeZone = "Europe/Zagreb";
          Ozds__Data__ConnectionString = "ozds-connection-string";
          Ozds__Email__From__Address = "ozds-email-address";
          Ozds__Email__From__Name = "OZDS";
          Ozds__Email__Smtp__Host = "ozds-email-host";
          Ozds__Email__Smtp__Password = "ozds-email-password";
          Ozds__Email__Smtp__Port = "ozds-email-port";
          Ozds__Email__Smtp__Ssl = "false";
          Ozds__Email__Smtp__Username = "ozds-email-username";
          Ozds__Jobs__ConnectionString = "ozds-connection-string";
          Ozds__Messaging__PersistenceConnectionString = "ozds-connection-string";
          Ozds__Messaging__ConnectionString = "ozds-test-messaging-connection-string";
          Ozds__Messaging__Endpoints__AcknowledgeNetworkUserInvoice = "queue:altibiz-network-user-invoice-state-test";
          Ozds__Messaging__Sagas__NetworkUserInvoiceState = "ozds-network-user-invoice-state-test";
        };
      };
    }
  ];
in
{
  flake.lib.secrets."raspberryPi4-aarch64-linux" = secrets;

  flake.lib.rumor."raspberryPi4-aarch64-linux" = rumor;
}

let
  secrets = {
    filePrefix = "scripts/flake/raspberryPi4.yaml";
    ageKeyFile = "/root/.sops.age";
    hostName = "ozds-test";
    ip = "10.8.250.1";
  };

  secrets.keys = {
    postgresSslKeyFile = "postgres-ssl-priv";
    postgresSslCertFile = "postgres-ssl-pub";
    postgresInitialScript = "postgres-sql";
    networkManagerEnvironmentFile = "wifi-env";
    nebulaKey = "nebula-ssl-priv";
    nebulaCert = "nebula-ssl-pub";
    nebulaCa = "nebula-ca-pub";
    userHashedPasswordFile = "user-pass-pub";
    userAuthorizedKeys = "user-ssh-pub";
    ozdsEnv = "ozds-env";
  };

  files = {
    # shared
    postgresCaPrivate = "postgres-ca-priv";
    postgresCaPublic = "postgres-ca-pub";
    postgresCaSerial = "postgres-ca-srl";
    nebulaCaPrivate = "nebula-ca-priv";
    nebulaCaPublic = "nebula-ca-pub";
    wifiSsid = "wifi-ssid";
    wifiPassword = "wifi-pass";
    emailHost = "ozds-test-email-host";
    emailPort = "ozds-test-email-port";
    emailAddress = "ozds-test-email-address";
    emailUsername = "ozds-test-email-username";
    emailPassword = "ozds-test-email-password";
    messagingConnectionString = "ozds-test-messaging-connection-string";

    # instance
    postgresSslPrivate = "postgres-ssl-priv";
    postgresSslPublic = "postgres-ssl-pub";
    postgresOzdsPassword = "postgres-ozds-pass";
    postgresUserPassword = "postgres-user-pass";
    postgresPassword = "postgres-pass";
    postgresSql = "postgres-sql";
    nebulaSslPrivate = "nebula-ssl-priv";
    nebulaSslPublic = "nebula-ssl-pub";
    userPasswordPrivate = "user-pass-priv";
    userPasswordPublic = "user-pass-pub";
    userSshPrivate = "user-ssh-priv";
    userSshPublic = "user-ssh-pub";
    orchardAdminPasswordPrefix = "orchard-admin-pass-prefix";
    orchardAdminPassword = "orchard-admin-pass";
    connectionString = "ozds-connection-string";
    wifiEnv = "wifi-env";
    ozdsEnv = "ozds-env";
    agePublic = "age-pub";
    agePrivate = "age-priv";
    secretsPublic = "secrets-pub";
    secretsPrivate = "secrets-priv";
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
      arguments.file = files.postgresCaPrivate;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = files.postgresCaPublic;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = files.postgresCaSerial;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = files.nebulaCaPrivate;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = files.nebulaCaPublic;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/pidgeon/test";
      arguments.file = files.wifiSsid;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/pidgeon/test";
      arguments.file = files.wifiPassword;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/email/test";
      arguments.file = files.emailHost;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/email/test";
      arguments.file = files.emailPort;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/email/test";
      arguments.file = files.emailAddress;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/email/test";
      arguments.file = files.emailUsername;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/email/test";
      arguments.file = files.emailPassword;
    }
    {
      importer = "vault-file";
      arguments.path = "kv/ozds/bus/test";
      arguments.file = files.messagingConnectionString;
    }
  ];

  rumor.exports = [
    {
      exporter = "vault-file";
      arguments.path = "kv/ozds/shared";
      arguments.file = files.postgresCaSerial;
    }
    {
      exporter = "vault";
      arguments.path = "kv/ozds/ozds/test";
    }
    {
      exporter = "copy";
      arguments.from = files.secretsPublic;
      arguments.to = "../${secrets.filePrefix}";
    }
  ];

  rumor.generations = [
    {
      generator = "openssl";
      arguments = {
        ca_private = files.postgresCaPrivate;
        ca_public = files.postgresCaPublic;
        serial = files.postgresCaSerial;
        name = secrets.hostName;
        private = files.postgresSslPrivate;
        public = files.postgresSslPublic;
      };
    }
    {
      generator = "key";
      arguments = {
        name = files.postgresOzdsPassword;
        length = 32;
      };
    }
    {
      generator = "key";
      arguments = {
        name = files.postgresUserPassword;
        length = 32;
      };
    }
    {
      generator = "key";
      arguments = {
        name = files.postgresPassword;
        length = 32;
      };
    }
    {
      generator = "moustache";
      arguments = {
        name = files.postgresSql;
        variables = {
          POSTGRES_PASS = files.postgresPassword;
          OZDS_POSTGRES_PASS = files.postgresOzdsPassword;
          USER_POSTGRES_PASS = files.postgresUserPassword;
        };
        template = ''
          ALTER USER postgres WITH PASSWORD '{{POSTGRES_PASS}}';

          CREATE USER ozds PASSWORD '{{OZDS_POSTGRES_PASS}}';
          CREATE USER altibiz PASSWORD '{{USER_POSTGRES_PASS}}';

          CREATE DATABASE ozds;
          ALTER DATABASE ozds OWNER TO ozds;

          \c ozds

          GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO altibiz;
          GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO altibiz;
          GRANT ALL PRIVILEGES ON ALL FUNCTIONS IN SCHEMA public TO altibiz;
        '';
        renew = true;
      };
    }
    {
      generator = "env";
      arguments = {
        name = files.wifiEnv;
        variables = {
          WIFI_SSID = files.wifiSsid;
          WIFI_PASS = files.wifiPassword;
        };
        renew = true;
      };
    }
    {
      generator = "nebula";
      arguments = {
        ca_private = files.nebulaCaPrivate;
        ca_public = files.nebulaCaPublic;
        name = secrets.hostName;
        ip = "${secrets.ip}/16";
        private = files.nebulaSslPrivate;
        public = files.nebulaSslPublic;
      };
    }
    {
      generator = "mkpasswd";
      arguments = {
        public = files.userPasswordPublic;
        private = files.userPasswordPrivate;
      };
    }
    {
      generator = "ssh-keygen";
      arguments = {
        name = secrets.hostName;
        public = files.userSshPublic;
        private = files.userSshPrivate;
      };
    }
    {
      generator = "moustache";
      arguments = {
        name = files.connectionString;
        variables = {
          OZDS_POSTGRES_PASS = files.postgresOzdsPassword;
        };
        template = "Server=localhost"
          + ";Port=5432"
          + ";Database=ozds"
          + ";User Id=ozds"
          + ";Password={{OZDS_POSTGRES_PASS}}"
          + ";Ssl Mode=Disable";
        renew = true;
      };
    }
    {
      generator = "key";
      arguments = {
        name = files.orchardAdminPasswordPrefix;
        length = 31;
      };
    }
    {
      generator = "moustache";
      arguments = {
        name = files.orchardAdminPassword;
        variables = {
          ORCHARD_ADMIN_PASSWORD_PREFIX = files.orchardAdminPasswordPrefix;
        };
        template = "{{ORCHARD_ADMIN_PASSWORD_PREFIX}}!";
      };
    }
    {
      generator = "env";
      arguments = {
        name = files.ozdsEnv;
        variables = {
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminEmail = "hrvoje@altibiz.com";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminPassword = files.orchardAdminPassword;
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__AdminUsername = "admin";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseConnectionString = files.connectionString;
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseProvider = "Postgres";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__DatabaseTablePrefix = "";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__RecipeName = "ozds";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__ShellName = "Default";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__SiteName = "OZDS";
          OrchardCore__OrchardCore_AutoSetup__Tenants__0__SiteTimeZone = "Europe/Zagreb";
          Ozds__Data__ConnectionString = files.connectionString;
          Ozds__Email__From__Address = files.emailAddress;
          Ozds__Email__From__Name = "OZDS";
          Ozds__Email__Smtp__Host = files.emailHost;
          Ozds__Email__Smtp__Password = files.emailPassword;
          Ozds__Email__Smtp__Port = files.emailPort;
          Ozds__Email__Smtp__Ssl = "false";
          Ozds__Email__Smtp__Username = files.emailUsername;
          Ozds__Jobs__ConnectionString = files.connectionString;
          Ozds__Messaging__PersistenceConnectionString = files.connectionString;
          Ozds__Messaging__ConnectionString = files.messagingConnectionString;
          Ozds__Messaging__Endpoints__AcknowledgeNetworkUserInvoice = "queue:altibiz-network-user-invoice-state-test";
          Ozds__Messaging__Sagas__NetworkUserInvoiceState = "ozds-network-user-invoice-state-test";
        };
        renew = true;
      };
    }
    {
      generator = "age";
      arguments = {
        private = files.agePrivate;
        public = files.agePublic;
      };
    }
    {
      generator = "sops";
      arguments = {
        age = files.agePublic;
        private = files.secretsPrivate;
        public = files.secretsPublic;
        secrets = {
          ${secrets.keys.postgresSslKeyFile} = files.postgresSslPrivate;
          ${secrets.keys.postgresSslCertFile} = files.postgresSslPublic;
          ${secrets.keys.postgresInitialScript} = files.postgresSql;
          ${secrets.keys.networkManagerEnvironmentFile} = files.wifiEnv;
          ${secrets.keys.nebulaKey} = files.nebulaSslPrivate;
          ${secrets.keys.nebulaCert} = files.nebulaSslPublic;
          ${secrets.keys.nebulaCa} = files.nebulaCaPublic;
          ${secrets.keys.userHashedPasswordFile} = files.userPasswordPublic;
          ${secrets.keys.userAuthorizedKeys} = files.userSshPublic;
          ${secrets.keys.ozdsEnv} = files.ozdsEnv;
        };
        renew = true;
      };
    }
  ];
in
{
  flake.lib.secrets."raspberryPi4-aarch64-linux" = secrets;

  flake.lib.rumor."raspberryPi4-aarch64-linux" = rumor;
}

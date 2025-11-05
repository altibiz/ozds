using Ozds.Business.Models;
using Ozds.Business.Models.Joins;
using Ozds.Business.Reflection;
using Ozds.Server.Test.Containers;

namespace Ozds.Server.Test.Fixtures;

public class TestApiKeyFixture(
  ServiceComposition composition
)
{
  public async Task<ApiKeyWithApiKeyScope> CreateForUserAndScope(
    TestUser testUser,
    ScopeModel scope,
    CancellationToken cancellationToken,
    Action<Configurator>? configure = null
  )
  {
    var configurator = new Configurator();
    if (configure is not null)
    {
      configure(configurator);
    }

    var trackableFixture = new TestTrackableFixture(composition);

    var modelReflector = composition.Ozds.Services
      .GetRequiredService<ModelReflector>();

    var apiKey = await trackableFixture.Create<ApiKeyModel>(
      cancellationToken,
      apiKey =>
      {
        apiKey.PrincipalModelType = modelReflector
          .ResolveModelName(typeof(RepresentativeModel));
        apiKey.PrincipalModelId = testUser.Id;

        configurator.ConfigureApiKey(apiKey);
      });

    var auditableFixture = new TestAuditableFixture(composition);

    var apiKeyScope = await auditableFixture.Create<ApiKeyScopeModel>(
      cancellationToken,
      apiKeyScope =>
      {
        apiKeyScope.ScopeId = scope.Id;
        apiKeyScope.ApiKeyId = apiKey.Id;

        configurator.ConfigureApiKeyScope(apiKeyScope);
      });

    return new ApiKeyWithApiKeyScope(
      apiKey,
      apiKeyScope
    );
  }

  public class Configurator
  {
    public Action<ApiKeyModel> ConfigureApiKey { get; private set; } =
      _ => { };

    public Action<ApiKeyScopeModel> ConfigureApiKeyScope { get; private set; } =
      _ => { };

    public Configurator WithApiKey(
      Action<ApiKeyModel> configure)
    {
      var prior = ConfigureApiKey;
      ConfigureApiKey = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }

    public Configurator WithApiKeyScope(
      Action<ApiKeyScopeModel> configure)
    {
      var prior = ConfigureApiKeyScope;
      ConfigureApiKeyScope = x =>
      {
        prior(x);
        configure(x);
      };
      return this;
    }
  }
}

public record ApiKeyWithApiKeyScope(
  ApiKeyModel ApiKey,
  ApiKeyScopeModel ApiKeyScope
);

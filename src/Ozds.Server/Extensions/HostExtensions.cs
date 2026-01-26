using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Ozds.Server.Middleware;

namespace Ozds.Server.Extensions;

public static class HostExtensions
{
  public static IHostApplicationBuilder AddOzdsServer(
    this IHostApplicationBuilder builder
  )
  {
    if (builder is WebApplicationBuilder webBuilder)
    {
      webBuilder.WebHost.ConfigureKestrel(serverOptions =>
      {
        serverOptions.Limits.MinRequestBodyDataRate = null;
      });
    }

    builder.Services.AddRazorPages();
    builder.Services.AddControllersWithViews();
    builder.Services.AddServerSideBlazor();

    builder.AddVersioning();
    builder.AddOpenApi();

    return builder;
  }

  public static WebApplication UseOzdsServer(
    this WebApplication app
  )
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
      app.UseHttpsRedirection();
    }

    app.UseMiddleware<OzdsExceptionMiddleware>();

    app.UseStaticFiles();
    app.MapStaticAssets();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseOpenApi();

    app.MapBlazorHub("/app/{culture}/_blazor");

    app.MapControllers();

    return app;
  }

  private static IHostApplicationBuilder AddVersioning(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services
      .AddApiVersioning(options =>
      {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
      })
      .AddApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
      });

    return builder;
  }

  private static IHostApplicationBuilder AddOpenApi(
    this IHostApplicationBuilder builder
  )
  {
    builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc(
        "v1", new OpenApiInfo
        {
          Title = "OZDS API",
          Version = "v1"
        });
      options.DocInclusionPredicate((docName, apiDesc) =>
      {
        var routeTemplate = apiDesc.RelativePath;
        return routeTemplate?.StartsWith("api/") ?? false;
      });
      options.TagActionsBy(api =>
      {
        var controllerName = api.ActionDescriptor.RouteValues["controller"];
        return new[] { controllerName?.Replace("ApiV1", "") ?? "Unknown" };
      });
      options.AddSecurityDefinition(
        "Bearer", new OpenApiSecurityScheme
        {
          Description =
            "API Key Authorization header using the Bearer scheme."
            + " Example: \"Bearer {apiKey}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
          Scheme = "Bearer"
        });
      options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              }
            },
            Array.Empty<string>()
          }
        });
    });

    return builder;
  }

  private static WebApplication UseOpenApi(
    this WebApplication app
  )
  {
    if (!app.Environment.IsDevelopment())
    {
      app.UseMiddleware<OzdsOpenApiAuthorizationMiddleware>();
    }

    app.UseSwagger(c =>
    {
      c.RouteTemplate = "api/{documentName}/openapi.json";
    });
    app.UseSwaggerUI(c =>
    {
      c.SwaggerEndpoint("/api/v1/openapi.json", "V1");
      c.RoutePrefix = "api/v1/openapi";

      if (app.Environment.IsDevelopment())
      {
        var apiKey = app.Configuration.GetValue<string>("Ozds:Sdk:ApiKey");
        c.HeadContent = @"
          <script>
            console.log('Auth script loaded!');
            window.addEventListener('load', function() {
              console.log('Window loaded, starting auth check...');
              let attempts = 0;
              const maxAttempts = 10;

              const interval = setInterval(() => {
                console.log('Attempt', attempts, 'window.ui:', window.ui);
                if (window.ui) {
                  clearInterval(interval);
                  console.log('Authorizing with key...');
                  window.ui.preauthorizeApiKey('Bearer', 'API_KEY_HERE');
                } else if (++attempts >= maxAttempts) {
                  clearInterval(interval);
                  console.warn('Swagger UI did not load in time');
                }
              }, 1000);
            });
          </script>".Replace("API_KEY_HERE", apiKey);
      }
    });

    return app;
  }
}

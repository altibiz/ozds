using OrchardCore.Recipes;
using OrchardCore.ResourceManagement.TagHelpers;
using Ozds.Data.Extensions;
using Ozds.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);
var rootServices = builder.Services;
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services
  .AddOrchardCore()
  .AddCommands()
  .AddSecurity()
  .AddMvc()
  .AddIdGeneration()
  .AddEmailAddressValidator()
  .AddSetupFeatures("OrchardCore.AutoSetup")
  .AddDataAccess()
  .AddDataStorage()
  .AddBackgroundService()
  .AddScripting()
  .AddTheming()
  .AddCaching()
  .ConfigureServices((tenantedServices, rootServices) =>
  {
    tenantedServices.AddResourceManagement();
    tenantedServices
      .AddTagHelpers<LinkTagHelper>()
      .AddTagHelpers<MetaTagHelper>()
      .AddTagHelpers<ResourcesTagHelper>()
      .AddTagHelpers<ScriptTagHelper>()
      .AddTagHelpers<StyleTagHelper>();

    tenantedServices.AddOzdsDbClient();
  });

rootServices.AddOzdsClient(isDevelopment);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.Use(async (context, next) =>
{
  if (context.Request.Path == "/")
  {
    context.Response.Redirect("/client/pages/dashboard", permanent: false);
  }
  else
  {
    await next();
  }
});

app.UseOrchardCore();
app.MapBlazorHub("/client/_blazor");

await app.RunAsync();

using MudBlazor.Services;
using OrchardCore.Recipes;
using OrchardCore.ResourceManagement.TagHelpers;
using Ozds.Data;

var builder = WebApplication.CreateBuilder(args);

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
  .ConfigureServices(s => s
    .AddResourceManagement()
    .AddTagHelpers<LinkTagHelper>()
    .AddTagHelpers<MetaTagHelper>()
    .AddTagHelpers<ResourcesTagHelper>()
    .AddTagHelpers<ScriptTagHelper>()
    .AddTagHelpers<StyleTagHelper>()
  );

builder.Services
  .AddRazorComponents()
  .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddScoped<OzdsDbContext>();
builder.Services.AddScoped<OzdsDbClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  _ = app.UseExceptionHandler("/Error");
  _ = app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOrchardCore();

app.MapRazorComponents<Ozds.Client.App>();
app.MapBlazorHub("/app/_blazor");

await using (var scope = app.Services.CreateAsyncScope())
{
  var dbClient = scope.ServiceProvider.GetRequiredService<OzdsDbClient>();
  await dbClient.MigrateAsync();
}

await app.RunAsync();

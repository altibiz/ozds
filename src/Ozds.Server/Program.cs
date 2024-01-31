using MudBlazor.Services;
using OrchardCore.Recipes;
using OrchardCore.ResourceManagement.TagHelpers;
using Ozds.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOrchardCore()
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
  .ConfigureServices(services => services.AddRecipes())
  .ConfigureServices(s =>
  {
    s.AddResourceManagement();

    s.AddTagHelpers<LinkTagHelper>();
    s.AddTagHelpers<MetaTagHelper>();
    s.AddTagHelpers<ResourcesTagHelper>();
    s.AddTagHelpers<ScriptTagHelper>();
    s.AddTagHelpers<StyleTagHelper>();
  });

builder.Services.AddRazorComponents()
  .AddInteractiveServerComponents();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddScoped<OzdsDbContext>();
builder.Services.AddScoped<OzdsDbClient>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseOrchardCore();
app.MapBlazorHub("/app/_blazor");

await using (var scope = app.Services.CreateAsyncScope())
{
  var dbClient = scope.ServiceProvider.GetRequiredService<OzdsDbClient>();
  await dbClient.MigrateAsync();
}

await app.RunAsync();

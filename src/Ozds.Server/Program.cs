using MudBlazor.Services;
using OrchardCore.ResourceManagement.TagHelpers;
using Ozds.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<OzdsDbContext>();
builder.Services.AddScoped<OzdsDbClient>();

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

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
  var client = scope.ServiceProvider.GetRequiredService<OzdsDbClient>();
  await client.MigrateAsync();
}

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

await app.RunAsync();

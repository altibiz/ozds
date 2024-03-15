using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OrchardCore.DisplayManagement;
using Ozds.Data;

namespace Ozds.Server.Extensions;

public static class IEndpointRouteBuilderExtensions
{
  public static void MapOzdsClient(
    this IEndpointRouteBuilder endpoints,
    string pattern
  )
  {
    endpoints
      .Map(pattern, async context =>
      {
        var actionContextAccessor = context.RequestServices
          .GetRequiredService<IActionContextAccessor>();
        var actionContext = actionContextAccessor.ActionContext;
        if (actionContext is null)
        {
          var routeData = new RouteData();
          routeData.Routers.Add(new RouteCollection());

          actionContext =
            new ActionContext(context, routeData, new ActionDescriptor());
          var filters =
            context.RequestServices.GetServices<IAsyncViewActionFilter>();

          foreach (var filter in filters)
            await filter.OnActionExecutionAsync(actionContext);

          actionContextAccessor.ActionContext = actionContext;
        }

        await using var writer = new StreamWriter(context.Response.Body);
        var viewContext = new ViewContext(
          actionContext,
          new EmptyView(),
          new ViewDataDictionary(
            new EmptyModelMetadataProvider(),
            new ModelStateDictionary()
          )
          {
            Model = null
          },
          new TempDataDictionary(
            actionContext.HttpContext,
            context.RequestServices.GetRequiredService<ITempDataProvider>()
          ),
          writer,
          new HtmlHelperOptions()
        );

        var htmlHelper = context.RequestServices
          .GetRequiredService<IHtmlHelper>();
        if (htmlHelper is IViewContextAware viewContextAware)
        {
          viewContextAware.Contextualize(viewContext);
        }

        var ozdsClient = context.RequestServices
          .GetRequiredService<OzdsDbClient>();

        await htmlHelper.RenderPartialAsync("Client");
      });
  }
}

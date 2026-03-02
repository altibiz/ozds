using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.JSInterop;
using Ozds.Client.Extensions;

namespace Ozds.Client.Components.Base;

public abstract partial class OzdsComponentBase : DisposableComponentBase
{
  [Inject]
  private NavigationManager NavigationManager { get; set; } = default!;

  [Inject]
  private TemplateBinderFactory TemplateBinderFactory { get; set; } = default!;

  [Inject]
  private IJSRuntime JS { get; set; } = default!;

  protected string HostHref
  {
    get
    {
      var uri = new Uri(NavigationManager.Uri);
      return $"{uri.Scheme}://{uri.Host}:{uri.Port}";
    }
  }

  protected string Href
  {
    get { return NavigationManager.Uri; }
  }

  protected string BaseHref
  {
    get { return NavigationManager.BaseUri; }
  }

  protected string PathHref
  {
    get { return new Uri(NavigationManager.Uri).AbsolutePath; }
  }

  protected string BasePathHref
  {
    get { return new Uri(NavigationManager.BaseUri).AbsolutePath; }
  }

  protected string LoginHref
  {
    get
    {
      return "/app/auth/login?returnUrl=" + Uri.EscapeDataString(PathHref);
    }
  }

  protected string LogoutHref
  {
    get
    {
      return "/app/auth/logout?returnUrl=" + Uri.EscapeDataString(PathHref);
    }
  }

  protected string IndexHref
  {
    get { return BasedHref("/"); }
  }

  protected string ApiV1Href
  {
    get { return "/api/v1/openapi"; }
  }

  protected string PageHref<T>(
    object? parameters = null,
    object? queryParameters = null
  )
  {
    return PageHref(typeof(T), parameters, queryParameters);
  }

  protected string PageHref(
    Type type,
    object? parameters = null,
    object? queryParameters = null
  )
  {
    return NavigationManager.PageHref(
      TemplateBinderFactory,
      type,
      parameters,
      queryParameters
    );
  }

  protected void NavigateBack()
  {
#pragma warning disable CA2012 // Use ValueTasks correctly
    JS.InvokeVoidAsync("history.back");
#pragma warning restore CA2012 // Use ValueTasks correctly
  }

  protected void NavigateHere()
  {
#pragma warning disable CA2012 // Use ValueTasks correctly
    JS.InvokeVoidAsync("location.reload");
#pragma warning restore CA2012 // Use ValueTasks correctly
  }

  protected void NavigateToLogin()
  {
    NavigationManager.NavigateTo(LoginHref);
  }

  protected void NavigateToLogout()
  {
    NavigationManager.NavigateTo(LogoutHref);
  }

  protected void NavigateToIndex()
  {
    NavigationManager.NavigateTo(IndexHref);
  }

  protected void NavigateToApiV1()
  {
    NavigationManager.NavigateTo(ApiV1Href);
  }

  protected void NavigateToPage<T>(
    object? parameters = null,
    object? queryParameters = null
  )
  {
    NavigationManager.NavigateTo(PageHref<T>(parameters, queryParameters));
  }

  protected void NavigateToPage(
    Type type,
    object? parameters = null,
    object? queryParameters = null
  )
  {
    NavigationManager.NavigateTo(PageHref(type, parameters, queryParameters));
  }

  private string BasedHref(string uri)
  {
    return NavigationManager.BasedHref(uri);
  }
}

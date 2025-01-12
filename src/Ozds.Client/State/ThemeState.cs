using MudBlazor;

namespace Ozds.Client.State;

public record ThemeState(
  MudTheme Theme,
  bool IsDarkMode,
  Func<MudTheme, Task> SetTheme,
  Func<bool, Task> SetDarkMode
)
{
  public static MudTheme DefaultTheme()
  {
    return new()
    {
      Palette = new PaletteLight
      {
        Primary = "#0072bc",
        AppbarBackground = "#0072bc",
      },
      PaletteDark = new PaletteDark
      {
        Primary = "#23a8ff",
        AppbarBackground = "#0072bc",
      },
      LayoutProperties = new LayoutProperties
      {
        DefaultBorderRadius = "0px"
      },
      Typography = new Typography
      {
        Default = new Default
        {
          FontFamily = ["Roboto"],
        },
        H1 = new H1
        {
          FontFamily = ["Roboto"],
        },
        H2 = new H2
        {
          FontFamily = ["Roboto"],
        },
        H3 = new H3
        {
          FontFamily = ["Roboto"],
        },
        H4 = new H4
        {
          FontFamily = ["Roboto"],
        },
        H5 = new H5
        {
          FontFamily = ["Roboto"],
        },
        H6 = new H6
        {
          FontFamily = ["Roboto"],
        },
        Button = new Button
        {
          FontFamily = ["Roboto"],
        },
        Body1 = new Body1
        {
          FontFamily = ["Roboto"],
        },
        Body2 = new Body2
        {
          FontFamily = ["Roboto"],
        },
        Caption = new Caption
        {
          FontFamily = ["Roboto"],
        },
        Subtitle2 = new Subtitle2
        {
          FontFamily = ["Roboto"],
        }
      },
    };
  }
}

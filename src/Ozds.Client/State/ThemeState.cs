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
      PaletteLight = new PaletteLight
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
        Default = new DefaultTypography
        {
          FontFamily = ["Roboto"],
        },
        H1 = new H1Typography
        {
          FontFamily = ["Roboto"],
        },
        H2 = new H2Typography
        {
          FontFamily = ["Roboto"],
        },
        H3 = new H3Typography
        {
          FontFamily = ["Roboto"],
        },
        H4 = new H4Typography
        {
          FontFamily = ["Roboto"],
        },
        H5 = new H5Typography
        {
          FontFamily = ["Roboto"],
        },
        H6 = new H6Typography
        {
          FontFamily = ["Roboto"],
        },
        Button = new ButtonTypography
        {
          FontFamily = ["Roboto"],
        },
        Body1 = new Body1Typography
        {
          FontFamily = ["Roboto"],
        },
        Body2 = new Body2Typography
        {
          FontFamily = ["Roboto"],
        },
        Caption = new CaptionTypography
        {
          FontFamily = ["Roboto"],
        },
        Subtitle2 = new Subtitle2Typography
        {
          FontFamily = ["Roboto"],
        }
      },
    };
  }
}

using MudBlazor;

namespace Ozds.Client.State;

public record ThemeState(
  MudTheme Theme,
  bool IsDarkMode,
  Action<MudTheme> SetTheme,
  Action<bool> SetDarkMode
)
{
  public static MudTheme Default()
  {
    return new()
    {
      Palette = new()
      {
        Primary = new("#176fc1"),
        Secondary = new("#338ed2"),
        Tertiary = new("#a7d6f2"),
        GrayDefault = "#e3e3e3",
      },
      PaletteDark = new()
      {
        Primary = new("#176fc1"),
        Secondary = new("#338ed2"),
        Tertiary = new("#a7d6f2"),
        GrayDefault = "#e3e3e3",
      },
      LayoutProperties = new()
      {
        DefaultBorderRadius = "0px"
      },
      Shadows = new(),
      ZIndex = new(),
      Typography = new()
      {
        Default = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = ".875rem",
          FontWeight = 400,
          LineHeight = 1.43,
          LetterSpacing = ".01071em"
        },
        H1 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "6rem",
          FontWeight = 300,
          LineHeight = 1.167,
          LetterSpacing = "-.01562em"
        },
        H2 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "3.75rem",
          FontWeight = 300,
          LineHeight = 1.2,
          LetterSpacing = "-.00833em"
        },
        H3 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "3rem",
          FontWeight = 400,
          LineHeight = 1.167,
          LetterSpacing = "0"
        },
        H4 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "2.125rem",
          FontWeight = 400,
          LineHeight = 1.235,
          LetterSpacing = ".00735em"
        },
        H5 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "1.5rem",
          FontWeight = 400,
          LineHeight = 1.334,
          LetterSpacing = "0"
        },
        H6 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "1.25rem",
          FontWeight = 400,
          LineHeight = 1.6,
          LetterSpacing = ".0075em"
        },
        Button = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = ".875rem",
          FontWeight = 500,
          LineHeight = 1.75,
          LetterSpacing = ".02857em"
        },
        Body1 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = "1rem",
          FontWeight = 400,
          LineHeight = 1.5,
          LetterSpacing = ".00938em"
        },
        Body2 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = ".875rem",
          FontWeight = 400,
          LineHeight = 1.43,
          LetterSpacing = ".01071em"
        },
        Caption = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = ".75rem",
          FontWeight = 400,
          LineHeight = 1.66,
          LetterSpacing = ".03333em"
        },
        Subtitle2 = new()
        {
          FontFamily = ["Roboto", "Helvetica", "Arial", "sans-serif"],
          FontSize = ".875rem",
          FontWeight = 500,
          LineHeight = 1.57,
          LetterSpacing = ".00714em"
        }
      }
    };
  }
}

namespace Ozds.Business.Extensions;

public static class IntExtensions
{
  private static readonly string[][] Colors =
  [
    [
      "#fbd500",
      "#e91e25",
      "#20f9bc"
    ],
    [
      "#d5fb00",
      "#e9541e",
      "#20f5f9"
    ],
    [
      "#8afb00",
      "#e9911e",
      "#20b4f9"
    ],
    [
      "#3ffb00",
      "#e9ce1e",
      "#2073f9"
    ],
    [
      "#00fb0d",
      "#c7e91e",
      "#2032f9"
    ],
    [
      "#00fb58",
      "#8ae91e",
      "#4f20f9"
    ],
    [
      "#00fba3",
      "#4de91e",
      "#9020f9"
    ],
    [
      "#00fbee",
      "#1ee92c",
      "#d120f9"
    ],
    [
      "#00bcfb",
      "#1ee969",
      "#f920e0"
    ],
    [
      "#0071fb",
      "#1ee9a5",
      "#f9209f"
    ]
  ];

  public static string ToColor(this int i)
  {
    return Colors[i / 3 % Colors.Length][i % 3];
  }

  public static string[] ToPhaseColors(this int i)
  {
    return Colors[i % Colors.Length];
  }
#pragma warning disable S125 // Sections of code should not be commented out
  // NOTE: generate { |$i|
  //         if $i <= 10 {
  //           { out: (["#FB8C00", "#E91E63", "#20F97B"] |
  //                   each { |x| pastel rotate ($i * 10) $x |
  //                              pastel format hex }),
  //             next: ($i + 1) }
  //         }
  //       } 1 | to json
#pragma warning restore S125 // Sections of code should not be commented out
}

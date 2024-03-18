namespace Ozds.Business.Models.Common;

public record CumulativeMeasure(
  PhasicMeasure Import,
  PhasicMeasure Export
);

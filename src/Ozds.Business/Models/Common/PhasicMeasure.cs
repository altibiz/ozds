namespace Ozds.Business.Models.Common;

public record PhasicMeasure();

public record SinglePhasicMeasure(float Value) : PhasicMeasure;

public record TriPhasicMeasure(float ValueL1, float ValueL2, float ValueL3) : PhasicMeasure;

public record NullPhasicMeasure() : PhasicMeasure;

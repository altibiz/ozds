using Ozds.Business.Models.Abstractions;

namespace Ozds.Business.Models;

public record AbbB2xMeterModel(
  string Id,
  string? NetworkUserMeasurementLocationId,
  string? LocationMeasurementLocationId,
  float MinVoltage_V,
  float MaxVoltage_V,
  float MinCurrent_A,
  float MaxCurrent_A,
  float MinActivePower_W,
  float MaxActivePower_W,
  float MinReactivePower_VAR,
  float MaxReactivePower_VAR,
  float ConnectionPower_W,
  List<PhaseModel> Phases
) : MeterModel(
  Id,
  NetworkUserMeasurementLocationId,
  LocationMeasurementLocationId
);

namespace Ozds.Business.Analysis;

public record Load(
  DateTimeOffset Timestamp,
  decimal ActivePower_kW,
  decimal ReactivePower_kVAR,
  decimal ApparentPower_kVA
);

namespace Ozds.Business.Pushing.Requests;

public readonly record struct MeterPushRequest(
  List<MeterRegister> Registers,
  List<MeterRange> Ranges
);

public readonly record struct MeterRegister(
  string Name,
  string Type,
  decimal Did
);

public readonly record struct MeterRange(
  string Ts,
  decimal Delta,
  List<List<string>> Rows
);

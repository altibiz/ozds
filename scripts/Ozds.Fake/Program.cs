var client = new HttpClient();
var random = new Random();
var now = DateTimeOffset.UtcNow;

var schneiderPushRequest = new
{
  voltageL1_V = random.NextDouble() * 1000,
  voltageL2_V = random.NextDouble() * 1000,
  voltageL3_V = random.NextDouble() * 1000,
  currentL1_A = random.NextDouble() * 1000,
  currentL2_A = random.NextDouble() * 1000,
  currentL3_A = random.NextDouble() * 1000,
  activePowerL1_W = random.NextDouble() * 1000,
  activePowerL2_W = random.NextDouble() * 1000,
  activePowerL3_W = random.NextDouble() * 1000,
  reactivePowerTotal_VAR = random.NextDouble() * 1000,
  apparentPowerTotal_VA = random.NextDouble() * 1000,
  activeEnergyImportL1_Wh = random.NextDouble() * 1000,
  activeEnergyImportL2_Wh = random.NextDouble() * 1000,
  activeEnergyImportL3_Wh = random.NextDouble() * 1000,
  activeEnergyImportTotal_Wh = random.NextDouble() * 1000,
  activeEnergyExportTotal_Wh = random.NextDouble() * 1000,
  reactiveEnergyImportTotal_VARh = random.NextDouble() * 1000,
  reactiveEnergyExportTotal_VARh = random.NextDouble() * 1000
};

var abbPushRequest = new
{
  voltageL1_V = random.NextDouble() * 1000,
  voltageL2_V = random.NextDouble() * 1000,
  voltageL3_V = random.NextDouble() * 1000,
  currentL1_A = random.NextDouble() * 1000,
  currentL2_A = random.NextDouble() * 1000,
  currentL3_A = random.NextDouble() * 1000,
  activePowerL1_W = random.NextDouble() * 1000,
  activePowerL2_W = random.NextDouble() * 1000,
  activePowerL3_W = random.NextDouble() * 1000,
  reactivePowerL1_VAR = random.NextDouble() * 1000,
  reactivePowerL2_VAR = random.NextDouble() * 1000,
  reactivePowerL3_VAR = random.NextDouble() * 1000,
  activePowerImportL1_Wh = random.NextDouble() * 1000,
  activePowerImportL2_Wh = random.NextDouble() * 1000,
  activePowerImportL3_Wh = random.NextDouble() * 1000,
  activePowerExportL1_Wh = random.NextDouble() * 1000,
  activePowerExportL2_Wh = random.NextDouble() * 1000,
  activePowerExportL3_Wh = random.NextDouble() * 1000,
  reactivePowerImportL1_VARh = random.NextDouble() * 1000,
  reactivePowerImportL2_VARh = random.NextDouble() * 1000,
  reactivePowerImportL3_VARh = random.NextDouble() * 1000,
  reactivePowerExportL1_VARh = random.NextDouble() * 1000,
  reactivePowerExportL2_VARh = random.NextDouble() * 1000,
  reactivePowerExportL3_VARh = random.NextDouble() * 1000,
  activeEnergyImportTotal_Wh = random.NextDouble() * 1000,
  activeEnergyExportTotal_Wh = random.NextDouble() * 1000,
  reactiveEnergyImportTotal_VARh = random.NextDouble() * 1000,
  reactiveEnergyExportTotal_VARh = random.NextDouble() * 1000
};

var pidgeonPushRequest = new
{
  timestamp = now,
  measurements = new[]
  {
    new
    {
      deviceId = "abb",
      timestamp = now,
      data = abbPushRequest as dynamic
    },
    new
    {
      deviceId = "schneider",
      timestamp = now,
      data = schneiderPushRequest as dynamic
    }
  }
};

await client.PostAsJsonAsync("http://localhost:5000/push/schneider",
  pidgeonPushRequest);

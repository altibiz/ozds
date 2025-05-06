namespace Ozds.Report.Entities;

public class AbbB2xAggregateEntity : AggregateEntity
{
  public decimal ActiveEnergyTotalImportT0_Wh { get; set; }

  public decimal ActiveEnergyTotalImportT1_Wh { get; set; }

  public decimal ActiveEnergyTotalImportT2_Wh { get; set; }

  public decimal ReactiveEnergyTotalImportT0_VARh { get; set; }

  public decimal ReactiveEnergyTotalExportT0_VARh { get; set; }

  public decimal MaxActivePowerTotalNetT1_W { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Ozds.Business.Math;
using Ozds.Business.Models.Base;

namespace Ozds.Business.Models.Complex;

// TODO: shortcodes to english

public abstract class
  ActiveEnergyTotalImportCalculationItemModel : CalculationItemModel
{
  [Required]
  public required decimal Min_kWh { get; set; }

  [Required]
  public required decimal Max_kWh { get; set; }

  [Required]
  public required decimal Amount_kWh { get; set; }

  [Required]
  public required decimal Price_EUR { get; set; }

  [Required]
  public required decimal Total_EUR { get; set; }

  public override decimal Total
  {
    get { return Total_EUR; }
  }
}

public abstract class ActiveEnergyTotalImportT0CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_kWh),
            PhasicMeasure<decimal>.Null
          )
        ),
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_kWh),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public abstract class ActiveEnergyTotalImportT1CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_kWh),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        ),
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_kWh),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}

public abstract class ActiveEnergyTotalImportT2CalculationItemModel
  : ActiveEnergyTotalImportCalculationItemModel
{
  public override SpanningMeasure<decimal> Amount
  {
    get
    {
      return new MinMaxSpanningMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Min_kWh),
            PhasicMeasure<decimal>.Null
          )
        ),
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Max_kWh),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public class UsageActiveEnergyTotalImportT0CalculationItemModel
  : ActiveEnergyTotalImportT0CalculationItemModel
{
  public override string Kind
  {
    get { return "MJT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public class UsageActiveEnergyTotalImportT1CalculationItemModel
  : ActiveEnergyTotalImportT1CalculationItemModel
{
  public override string Kind
  {
    get { return "MVT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}

public class UsageActiveEnergyTotalImportT2CalculationItemModel
  : ActiveEnergyTotalImportT2CalculationItemModel
{
  public override string Kind
  {
    get { return "MNT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new UsageExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public class SupplyActiveEnergyTotalImportT1CalculationItemModel
  : ActiveEnergyTotalImportT2CalculationItemModel
{
  public override string Kind
  {
    get { return "RVT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          ),
          DuplexMeasure<decimal>.Null
        )
      );
    }
  }
}

public class SupplyActiveEnergyTotalImportT2CalculationItemModel
  : ActiveEnergyTotalImportT2CalculationItemModel
{
  public override string Kind
  {
    get { return "RNT"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new BinaryTariffMeasure<decimal>(
          DuplexMeasure<decimal>.Null,
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public class
  SupplyBusinessUsageCalculationItemModel :
  ActiveEnergyTotalImportT0CalculationItemModel
{
  public override string Kind
  {
    get { return "TRP"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

public class
  SupplyRenewableEnergyCalculationItemModel :
  ActiveEnergyTotalImportT0CalculationItemModel
{
  public override string Kind
  {
    get { return "OIE"; }
  }

  public override ExpenditureMeasure<decimal> Price
  {
    get
    {
      return new SupplyExpenditureMeasure<decimal>(
        new UnaryTariffMeasure<decimal>(
          new ImportExportDuplexMeasure<decimal>(
            new SinglePhasicMeasure<decimal>(Price_EUR),
            PhasicMeasure<decimal>.Null
          )
        )
      );
    }
  }
}

using Ozds.Business.Finance.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;

// TODO: tax rate from catalogue

namespace Ozds.Business.Finance;

public class NetworkUserInvoiceIssuer
{
  private readonly IServiceProvider _serviceProvider;

  public NetworkUserInvoiceIssuer(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public CalculatedNetworkUserInvoiceModel Issue(
    NetworkUserInvoiceIssuingBasisModel basis
  )
  {
    var calculators = _serviceProvider
      .GetServices<INetworkUserCalculationCalculator>();
    var calculations = basis.NetworkUserCalculationBases.Select(
      basis => calculators
                 .FirstOrDefault(calculator => calculator
                   .CanCalculate(basis))
                 ?.Calculate(basis)
               ?? throw new InvalidOperationException(
                 $"No calculator found for {basis.GetType().Name}"
               )
    );

    var total = calculations
      .Sum(calculation => calculation.Total_EUR.TariffSum.DuplexSum.PhaseSum);
    var tax = total * 0.13M;
    var totalWithTax = total + tax;

    var initial = new NetworkUserInvoiceModel
    {
      Id = default!,
      Title = "",
      IssuedById = "",
      IssuedOn = DateTimeOffset.Now,
      NetworkUserId = basis.NetworkUser.Id,
      FromDate = basis.FromDate,
      ToDate = basis.ToDate,
      ArchivedNetworkUser = basis.NetworkUser,
      ArchivedLocation = basis.Location,
      Total_EUR = total,
      Tax_EUR = tax,
      TotalWithTax_EUR = totalWithTax
    };

    return new CalculatedNetworkUserInvoiceModel(
      Invoice: initial,
      NetworkUserCalculations: calculations.ToList()
    );
  }
}

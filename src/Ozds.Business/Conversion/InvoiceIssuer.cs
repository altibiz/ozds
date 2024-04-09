using Ozds.Business.Conversion.Abstractions;
using Ozds.Business.Models;
using Ozds.Business.Models.Composite;

// TODO: tax rate from catalogue

namespace Ozds.Business.Conversion;

public class InvoiceIssuer : IInvoiceIssuer
{
  private readonly IServiceProvider _serviceProvider;

  public InvoiceIssuer(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public bool CanIssueForNetworkUser(NetworkUserInvoiceIssuingBasisModel basis)
  {
    return true;
  }

  public bool CanIssueForLocation(LocationInvoiceIssuingBasisModel basis)
  {
    return true;
  }

  public CalculatedNetworkUserInvoiceModel IssueForNetworkUser(
    NetworkUserInvoiceIssuingBasisModel basis
  )
  {
    var calculators = _serviceProvider
      .GetServices<ICalculationCalculator>();
    var calculations = basis.CalculationBases.Select(
      basis => calculators
        .FirstOrDefault(calculator => calculator
          .CanCalculateForNetworkUser(basis))
        ?.CalculateForNetworkUser(basis)
          ?? throw new InvalidOperationException(
            $"No calculator found for {basis.GetType().Name}"
          )
    );

    var total = calculations
      .Sum(calculation => calculation.Total_EUR.TariffSum.DuplexSum.PhaseSum);
    var tax = total * 0.13M;
    var totalWithTax = total + tax;

    var initial = new NetworkUserInvoiceModel()
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
      Calculations: calculations.ToList()
    );
  }

  public CalculatedLoactionInvoiceModel IssueForLocation(
    LocationInvoiceIssuingBasisModel basis
  )
  {
    throw new NotImplementedException();
  }
}

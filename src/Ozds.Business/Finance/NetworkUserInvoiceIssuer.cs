using Ozds.Business.Finance.Agnostic;
using Ozds.Business.Models;
using Ozds.Business.Models.Base;
using Ozds.Business.Models.Composite;

namespace Ozds.Business.Finance;

public class NetworkUserInvoiceIssuer
{
  private readonly AgnosticNetworkUserCalculationCalculator
    _calculationCalculator;

  private readonly IHttpContextAccessor _httpContextAccessor;

  public NetworkUserInvoiceIssuer(
    AgnosticNetworkUserCalculationCalculator calculationCalculator,
    IHttpContextAccessor httpContextAccessor
  )
  {
    _calculationCalculator = calculationCalculator;
    _httpContextAccessor = httpContextAccessor;
  }

  public CalculatedNetworkUserInvoiceModel Issue(
    NetworkUserInvoiceIssuingBasisModel basis
  )
  {
    var now = DateTimeOffset.UtcNow;
    var issuer = GetRepresentativeId();

    var calculations = basis.NetworkUserCalculationBases
      .Select(_calculationCalculator.Calculate)
      .OfType<NetworkUserCalculationModel>()
      .ToList();

    var total = calculations
      .Sum(calculation => calculation
        .Total_EUR
        .ExpenditureSum
        .TariffSum
        .DuplexSum
        .PhaseSum
      );
    var tax = total * basis.RegulatoryCatalogue.TaxRate_Percent / 100M;
    var totalWithTax = total + tax;

    var initial = new NetworkUserInvoiceModel
    {
      Id = default!,
      Title = issuer is null
        ? $"Invoice for {basis.NetworkUser.Title} at {basis.Location.Title}"
        : $"Invoice for {basis.NetworkUser.Title} at {basis.Location.Title} issued by {issuer}",
      IssuedById = issuer,
      IssuedOn = now,
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
      NetworkUserCalculations: calculations
    );
  }

  private string? GetRepresentativeId()
  {
    if (_httpContextAccessor.HttpContext is not { } httpContextAccessor)
    {
      return null;
    }

    return null;
    // return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
  }
}

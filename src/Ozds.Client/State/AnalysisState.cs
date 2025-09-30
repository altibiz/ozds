using Ozds.Business.Models.Composite;

namespace Ozds.Client.State;

public record AnalysisState(
  Action Reset,
  Lazy<List<AnalysisBasisModel>> AnalysisBases
);

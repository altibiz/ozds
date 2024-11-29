using Ozds.Business.Models.Composite;

namespace Ozds.Client.State;

public record AnalysisState(
  Lazy<List<AnalysisBasisModel>> AnalysisBases
);

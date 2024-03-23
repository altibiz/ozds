namespace Ozds.Business.Models.Abstractions;

public abstract record SoftDeletableModel(string Id, bool IsDeleted) : IdModel(Id);

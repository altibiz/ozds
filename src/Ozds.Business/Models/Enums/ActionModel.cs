namespace Ozds.Business.Models.Enums;

public enum ActionModel
{
  Read,
  List,
  Create,
  Update,
  Delete,
  Restore,
  Forget,
}

public static class ActionModelExtensions
{
  public static string ToTitle(this ActionModel action)
  {
    return action switch
    {
      ActionModel.Read => "Read",
      ActionModel.List => "List",
      ActionModel.Create => "Create",
      ActionModel.Update => "Update",
      ActionModel.Delete => "Delete",
      ActionModel.Restore => "Restore",
      ActionModel.Forget => "Forget",
      _ => throw new ArgumentOutOfRangeException(nameof(action)),
    };
  }
}

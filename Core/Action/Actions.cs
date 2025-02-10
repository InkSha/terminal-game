namespace Core;
public class Actions
{
  public delegate void ActionCallback(string keyword, string[] args);
  public ActionCallback? ActionCallbackHandler { get; set; } = null;


  public bool Execute(string keyword, string[] args)
  {
    if (ActionCallbackHandler is null)
    {
      return false;
    }
    try
    {
      ActionCallbackHandler.Invoke(keyword, args);
    }
    catch (Exception e)
    {
      return false;
    }

    return true;
  }
}

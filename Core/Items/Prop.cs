namespace Core;

public interface IProp
{
  public bool CanUse { get; set; }
  public bool UnlimitedUse { get; set; }
  public int UseCount { get; set; }
  public int MaxUseCount { get; set; }
  public int EffectiveTime { get; set; }
  public int Effective { get; set; }
  public int CDTime { get; set; }
  public int CD { get; set; }
}

public class Props(string name) : Items(name), IProp
{
  public bool CanUse { get; set; } = false;
  public bool UnlimitedUse { get; set; } = false;
  public int UseCount { get; set; } = 0;
  public int MaxUseCount { get; set; } = 0;
  public int EffectiveTime { get; set; } = 0;
  public int Effective { get; set; } = 0;
  public int CDTime { get; set; } = 0;
  public int CD { get; set; } = 0;
}

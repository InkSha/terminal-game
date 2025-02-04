namespace Core;

public enum BuildStatus
{
  /// <summary>
  /// Build not create
  /// </summary>
  Uncreate,
  /// <summary>
  /// Begin build
  /// </summary>
  Build,
  /// <summary>
  /// Build complete and used
  /// </summary>
  Active,
  /// <summary>
  /// Build complete but not used
  /// </summary>
  Deactive,
  /// <summary>
  /// Build damaged
  /// </summary>
  Damaged,
  /// <summary>
  /// Build repairing
  /// </summary>
  Repair,
  /// <summary>
  /// Build discard
  /// </summary>
  Discard,
  /// <summary>
  /// Build destroyed
  /// </summary>
  Destroyed
}

public interface IBuild : IIdObject, IGUIItem
{
  public int HP { get; set; }
  public bool CanDestroy { get; set; }
  public int Restore { get; set; }
  public int RestoreTime { get; set; }
  public BuildStatus Status { get; set; }
  public List<string> Tags { get; set; }
}

public class Build : IdObject, IBuild
{
  public const string BUILD_LABEL = "建筑";
  public string Data { get; set; } = "";
  public int HP { get; set; } = -1;
  public bool CanDestroy { get; set; } = false;
  public int Restore { get; set; } = 0;
  public int RestoreTime { get; set; } = 0;
  public BuildStatus Status { get; set; } = BuildStatus.Uncreate;
  public List<string> Tags { get; set; } = [];

  public Build(string name) : base(BUILD_LABEL)
  {
    Data = name;
  }
}

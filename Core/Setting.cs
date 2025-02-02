namespace Core;

using Utils;

public class Setting : Json<Setting>
{
  public const string SETTING_SAVE_PATH = "tmp/setting.json";
  public const string DATA_SAVE_PATH = "tmp/data.json";

  public string DataSavePath { get; set; } = DATA_SAVE_PATH;
}

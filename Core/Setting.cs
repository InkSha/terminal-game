namespace Core;

using Utils;

public class Setting : Json<Setting>
{
  public const string BASE_DIR = "tmp";
  public const string SETTING_SAVE_PATH = "setting.json";
  public const string DATA_SAVE_PATH = "data.json";

  public static string DataSavePath { get; set; } = Path.Join(BASE_DIR, DATA_SAVE_PATH);
  public static string SettingSavePath { get; set; } = Path.Join(BASE_DIR, SETTING_SAVE_PATH);

  public Setting()
  {
    if (!Directory.Exists(BASE_DIR))
    {
      Directory.CreateDirectory(BASE_DIR);
    }
  }
}

using Utils;

namespace Core;

public class Save : Json<Save>
{
  public string? Time { get; set; }
  public string? Date { get; set; }

  public static Save LoadData(string path)
  {
    if (File.Exists(path))
    {
      return FromFile(path, new Save())!;
    }

    return new Save();
  }

  public void SaveData(string path)
  {
    ToFile(path);
  }
}

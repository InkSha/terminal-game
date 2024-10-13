using System.Text.Json;

namespace Utils;

public class Json<T> where T : Json<T>
{
  public string ToJson()
  {
    return JsonSerializer.Serialize(this);
  }

  public static T? FromFile(string filename)
  {
    try
    {
      string json = File.ReadAllText(filename);

      if (!string.IsNullOrEmpty(json))
      {
        return JsonSerializer.Deserialize<T>(json);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
    }

    return null;
  }

  public void ToFile(string name)
  {
    try
    {
      string content = ToJson();
      Console.WriteLine(content);
      File.WriteAllText($"{name}.json", content);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error writing file: {ex.Message}");
    }
  }

}

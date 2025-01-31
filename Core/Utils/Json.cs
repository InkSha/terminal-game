using System.Text.Json;

namespace Utils;

public class Json<T> where T : Json<T>
{
  public string ToJson()
  {
    return JsonSerializer.Serialize(this);
  }

  public static T? FromFile(string filename, T? defaultValue = null)
  {
    try
    {
      string json = File.ReadAllText(filename, System.Text.Encoding.UTF8);

      if (!string.IsNullOrEmpty(json))
      {
        return JsonSerializer.Deserialize<T>(json);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
    }

    return defaultValue;
  }

  public void ToFile(string name)
  {
    try
    {
      string content = ToJson();
      Console.WriteLine(content);
      File.WriteAllText($"{name}.json", content, System.Text.Encoding.UTF8);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error writing file: {ex.Message}");
    }
  }

}

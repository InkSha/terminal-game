using System.Text.Json;

namespace Utils;

public class Json<T> where T : Json<T>
{
  private static readonly JsonSerializerOptions serializerOptions = new()
  {
    IncludeFields = true
  };

  public string ToJson(JsonSerializerOptions? options = null)
  {
    options ??= serializerOptions;
    string content = JsonSerializer.Serialize((T)this, options);
    return content;
  }

  public static T? FromFile(string filename, T? defaultValue = null)
  {
    try
    {
      string json = File.ReadAllText(filename, System.Text.Encoding.UTF8);

      if (!string.IsNullOrEmpty(json))
      {
        return JsonSerializer.Deserialize<T>(json, serializerOptions);
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
      File.WriteAllText(name, content, System.Text.Encoding.UTF8);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error writing file: {ex.Message}");
    }
  }

}

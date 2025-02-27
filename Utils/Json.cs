using System.Text.Json;
using System.Text.Json.Serialization;

namespace Utils;

public class Json<T> where T : Json<T>
{
  private static readonly JsonSerializerOptions serializerOptions = new()
  {
    IncludeFields = true,
    ReferenceHandler = ReferenceHandler.Preserve,
  };

  private string FromJsonFilePath = "";

  public string ToJson(JsonSerializerOptions? options = null)
  {
    options ??= serializerOptions;
    string content = JsonSerializer.Serialize(this, GetType(), options);
    return content;
  }

  public static R? FromJson<R>(string json, R? defaultValue = default) where R : T
  {
    try
    {
      if (!string.IsNullOrEmpty(json))
      {
        return JsonSerializer.Deserialize<R>(json, serializerOptions);
      }
    }
    catch (Exception ex)
    {
      Print.PrintText($"Error reading or deserializing file: {ex.Message}");
    }

    return defaultValue;
  }

  public static T? FromJson(string json, T? defaultValue = default)
  {
    return FromJson<T>(json, defaultValue);
  }

  public static R? FromFile<R>(string filename, R? defaultValue = default) where R : T
  {
    if (File.Exists(filename))
    {
      string content = File.ReadAllText(filename, System.Text.Encoding.UTF8);
      return FromJson(content, defaultValue);
    }
    return defaultValue;
  }

  public static T? FromFile(string filename, T? defaultValue = default)
  {
    return FromFile<T>(filename, defaultValue);
  }

  public void ToFile()
  {
    if (FromJsonFilePath == "")
    {
      throw new Exception("FromJsonFilePath is empty");
    }
    ToFile(FromJsonFilePath);
  }

  public void ToFile(string name)
  {
    try
    {
      string content = ToJson();
      File.WriteAllText(name, content, System.Text.Encoding.UTF8);
    }
    catch (Exception ex)
    {
      Print.PrintText($"Error writing file: {ex.Message}");
    }
  }

}

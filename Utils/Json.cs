using System.Text.Json;

namespace Utils;

public class Json<T> where T : Json<T>
{
  private static readonly JsonSerializerOptions serializerOptions = new()
  {
    IncludeFields = true
  };

  private string FromJsonFilePath = "";

  public string ToJson(JsonSerializerOptions? options = null)
  {
    options ??= serializerOptions;
    string content = JsonSerializer.Serialize(this, GetType(), options);
    return content;
  }

  public static R? FromFile<R>(string filename, R? defaultValue = default) where R : T
  {
    try
    {
      string json = File.ReadAllText(filename, System.Text.Encoding.UTF8);

      if (!string.IsNullOrEmpty(json))
      {
        var obj = JsonSerializer.Deserialize<R>(json, serializerOptions);
        // Using try-catch block
        // If obj is null, it will throw an exception
        // and print the error message in the catch block
        // Finally, return the default value not obj
        obj!.FromJsonFilePath = filename;
        return obj;
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error reading or deserializing file: {ex.Message}");
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
      // Console.WriteLine(content);
      File.WriteAllText(name, content, System.Text.Encoding.UTF8);
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error writing file: {ex.Message}");
    }
  }

}

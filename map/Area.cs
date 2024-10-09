using System.Text.Json;

namespace map
{
  public class Area(string name)
  {
    protected string name = name;
    protected Area[] child = [];

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    public Area[] Child
    {
      get { return child; }
      set { child = value; }
    }

    protected void Create(string parent)
    {
      string path = Path.Join(parent, Name);
      Directory.CreateDirectory(path);

      for (int i = 0; i < child.Length; i++)
      {
        child[i].Create(path);
      }
    }

    public string ToJson()
    {
      return JsonSerializer.Serialize<Area>(this);
    }

    public static Area FromFile(string filename)
    {
      string json = File.ReadAllText(filename);

      if (!string.IsNullOrEmpty(json))
      {
        Area? area = JsonSerializer.Deserialize<Area>(json);

        if (area is not null) return area;
      }

      return new("");
    }

    public void ToFile(string? path = null)
    {
      string content = ToJson();
      string filename = Name + ".json";

      if (!string.IsNullOrEmpty(path))
      {
        filename = path;
      }
      File.WriteAllText(filename, content);
    }

    public void Push(Area[] areas)
    {
      child = child.Concat(areas).ToArray();
    }

    public void Init()
    {
      if (!string.IsNullOrEmpty(Name))
      {
        Create("");
      }
    }
  }
}

using System.Reflection.Metadata;
using Utils;

namespace Core.Map;

public struct AreaData(string name)
{
  public string Name = name;
  public AreaData[] Child = [];
}

public class Area(string name) : Json<Area>
{
  public string Name { get; set; } = name;

  public Area[] Child { get; set; } = [];

  protected void Create(string parent)
  {
    string path = Path.Join(parent, Name);
    Directory.CreateDirectory(path);

    for (int i = 0; i < Child.Length; i++)
    {
      Child[i].Create(path);
    }
  }

  public void Push(params Area[] areas)
  {
    Child = [.. Child, .. areas];
  }

  public void Init()
  {
    if (!string.IsNullOrEmpty(Name))
    {
      Create("");
    }
  }

  public static Area LoadData(AreaData data)
  {
    Area root = new(data.Name);
    foreach (AreaData item in data.Child)
    {
      root.Push(LoadData(item));
    }

    return root;
  }
}

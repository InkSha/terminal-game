using System.Numerics;

namespace Core.GUI;

public struct GUIItem(string label, string data)
{
  public string label = label;
  public string data = data;
}

public class GUI(List<GUIItem> list)
{
  private readonly List<GUIItem> items = list;

  public void Add(GUIItem item)
  {
    items.Add(item);
  }

  public void PrintUI()
  {
    items.ForEach(item =>
    {
      Console.WriteLine($"[{item.label}]: {item.data}");
    });
  }
}

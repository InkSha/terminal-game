namespace Core;

public interface IGUIItem
{
  public string Label { get; }
  public string Data { get; }
}

public class GUIItem(string label, string data) : IGUIItem
{
  public string Label { get; set; } = label;
  public string Data { get; set; } = data;
}

public class GUI(List<IGUIItem> list)
{
  private readonly List<IGUIItem> items = list;

  public void Add(IGUIItem item)
  {
    items.Add(item);
  }

  public void Add(List<IGUIItem> list)
  {
    items.AddRange(list);
  }

  public void RemoveItem(string label)
  {
    items.RemoveAll(item => item.Label == label);
  }

  public void Clear()
  {
    items.Clear();
  }

  public void ToggleGUI(List<IGUIItem> list)
  {
    items.Clear();
    items.AddRange(list);
  }

  public void PrintUI()
  {
    Console.Clear();
    items.ForEach(item =>
    {
      Console.WriteLine($"[{item.Label}]: {item.Data}");
    });
  }
}

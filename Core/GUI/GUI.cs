namespace Core.GUI;

public class GUIItem(string label, string data)
{
  public string Label { get; set; } = label;
  public string Data { get; set; } = data;

}

public class GUI(List<GUIItem> list)
{
  private readonly List<GUIItem> items = list;

  public void Add(GUIItem item)
  {
    items.Add(item);
  }

  public void RemoveItem(string label)
  {
    items.RemoveAll(item => item.Label == label);
  }

  public void Clear()
  {
    items.Clear();
  }

  public void ToggleGUI(List<GUIItem> list)
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

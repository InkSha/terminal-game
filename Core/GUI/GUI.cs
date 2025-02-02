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

  public bool Contains(string label)
  {
    return items.Exists(item => item.Label == label);
  }

  public bool UseLabelChangeItem(string label, string data)
  {
    var item = new GUIItem(label, data);

    return ChangeItem(item);
  }

  public bool ChangeItem(IGUIItem item)
  {
    int index = items.FindIndex(i => i.Label == item.Label);
    if (index != -1)
    {
      items[index] = item;

      return true;
    }

    return false;
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

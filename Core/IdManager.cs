using Utils;

namespace Core;

public static class IdManager
{
  public static List<string> Labels { get; } = [];

  public static Dictionary<string, int> IdRecord = [];

  public static void AddLabel(string label)
  {
    if (!Labels.Contains(label))
    {
      Labels.Add(label);
    }

    IdRecord.TryAdd(label, 0);
  }

  public static void RemoveLabel(string label)
  {
    Labels.Remove(label);
    IdRecord.Remove(label);
  }

  public static int GetId(string label)
  {
    AddLabel(label);
    return IdRecord[label]++;
  }

  public static void SetID(string label, int id)
  {
    if (!IdRecord.ContainsKey(label))
    {
      return;
    }

    IdRecord[label] = id;
  }

  public static void ResetID(string label)
  {
    if (!IdRecord.ContainsKey(label))
    {
      return;
    }
    IdRecord[label] = 0;
  }
}

public interface IIdObject
{
  public string Label { get; }
  public int Id { get; }
}

public class IdObject(string label) : Json<IdObject>, IIdObject
{
  public string Label { get; } = label;
  public int Id { get; } = IdManager.GetId(label);
}

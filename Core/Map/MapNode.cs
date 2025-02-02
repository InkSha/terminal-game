namespace Core;

public class MapNode : IdObject, IGUIItem
{
  public const string MAP_NODE_LABEL = "地点";
  private MapNode? Parent { get; set; } = null;
  public List<MapNode> Areas { get; } = [];
  public string Data { get; set; }

  public MapNode(string Data, List<MapNode>? Areas = null) : base(MAP_NODE_LABEL)
  {
    this.Data = Data;
    if (Areas is not null)
    {
      AddAreas(Areas);
    }
  }

  public void AddArea(MapNode area)
  {
    area.Parent = this;
    Areas.Add(area);
  }

  public void AddAreas(List<MapNode> areas)
  {
    foreach (var area in areas)
    {
      AddArea(area);
    }
  }

  public void RemoveArea(string areaName)
  {
    Areas.RemoveAll(area =>
    {
      if (area.Data != areaName)
      {
        area.RemoveArea(areaName);
        return false;
      }

      area.Parent = null;

      return true;
    });
  }

  public List<string> ListAreas()
  {
    List<string> areas = [];

    foreach (var area in Areas)
    {
      areas.AddRange(area.ListAreas());
    }

    areas.Add(ToString());
    return areas;
  }

  public List<string> ToRoot()
  {
    List<string> areas = [Data];

    if (Parent is not null)
    {
      areas.AddRange(Parent.ToRoot());
    }
    return areas;
  }

  public override string ToString()
  {

    if (Parent is not null)
    {
      return $"{Parent}->{Data}";
    }

    return Data;
  }
}

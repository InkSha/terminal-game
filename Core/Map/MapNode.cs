using System.Text.Json.Serialization;

namespace Core;

public class MapNode : IdObject, IGUIItem
{
  public const string MAP_NODE_LABEL = "地点";
  [JsonIgnore]
  private MapNode? Parent { get; set; } = null;
  public List<MapNode> Areas { get; } = [];
  public List<Build> Builds { get; set; } = [];
  public List<Items> Items { get; set; } = [];
  public string Data { get; set; }

  public MapNode(string Data, List<MapNode>? Areas = null) : base(MAP_NODE_LABEL)
  {
    this.Data = Data;
    if (Areas is not null)
    {
      AddAreas(Areas);
    }
  }

  public MapNode? GetParent()
  {
    return Parent;
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

  public void AddBuild(Build build)
  {
    Builds.Add(build);
  }

  public void AddBuilds(List<Build> builds)
  {
    Builds.AddRange(builds);
  }

  public void RemoveBuild(Build build)
  {
    Builds.Remove(build);
  }

  public void AddItem(Items item)
  {
    Items.Add(item);
  }

  public void AddItems(List<Items> items)
  {
    Items.AddRange(items);
  }

  public void RemoveItem(Items item)
  {
    Items.Remove(item);
  }

  public bool CreateMap(string? baseDir = null)
  {
    string parent = string.Join('/', ToRoot());
    string path = baseDir is not null
      ? string.Join('/', [baseDir, parent])
      : parent;

    if (!Directory.Exists(path))
    {
      Directory.CreateDirectory(path);
    }
    foreach (var build in Builds)
    {
      build.CreateBuild(path);
    }

    foreach (var item in Items)
    {
      item.CreateItem(path);
    }

    foreach (var area in Areas)
    {
      area.CreateMap(baseDir);
    }

    return Directory.Exists(path);
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

  public List<string> ListBuilds()
  {
    List<string> builds = [];

    foreach (var build in Builds)
    {
      builds.Add(build.Data);
    }

    return builds;
  }

  public List<string> ListItems()
  {
    List<string> items = [];

    foreach (var item in Items)
    {
      items.Add(item.Data);
    }

    return items;
  }

  public List<string> ToRoot()
  {
    List<string> areas = [];

    if (Parent is not null)
    {
      areas.AddRange(Parent.ToRoot());
    }

    areas.Add(Data);
    return areas;
  }

  public MapNode? Find(string areaName)
  {
    if (Data == areaName)
    {
      return this;
    }

    foreach (var area in Areas)
    {
      var result = area.Find(areaName);
      if (result is not null)
      {
        return result;
      }
    }
    return null;
  }

  public MapNode? Find(int id)
  {
    if (Id == id)
    {
      return this;
    }

    foreach (var area in Areas)
    {
      var result = area.Find(id);
      if (result is not null)
      {
        return result;
      }
    }

    return null;
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

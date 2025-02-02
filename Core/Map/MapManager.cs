namespace Core;

public class MapManager : IGUIItem
{
  public MapNode Root { get; set; }
  public string Label => "位面";
  public string Data => Root?.Data ?? "Unknown World";
  public MapNode? CurrentNode { get; set; }

  public MapManager(MapNode? root = null)
  {
    Root = root ?? new MapNode("root");
    CurrentNode = Root;
  }

  private void LoadMapNode(MapNode root)
  {
    Root = root;
    CurrentNode = Root;
  }

  public void LoadMap(MapNode root)
  {
    LoadMapNode(root);
  }

  public void LoadMap(string json)
  {
    var root = MapNode.FromJson<MapNode>(json, new(""));
    if (root is not null)
    {
      LoadMapNode(root);
    }
  }
  public string SaveMap()
  {
    if (Root is not null)
    {
      return Root.ToJson();
    }
    return "{}";
  }

  public bool GoTo(string mapName)
  {
    var node = Root.Find(mapName);
    if (node is null)
    {
      return false;
    }
    CurrentNode = node;
    return true;
  }

  public bool GoTo(int id)
  {
    var node = Root.Find(id);
    if (node is null)
    {
      return false;
    }
    CurrentNode = node;
    return true;
  }

  public bool GoTo(MapNode node)
  {
    CurrentNode = node;
    return true;
  }
}

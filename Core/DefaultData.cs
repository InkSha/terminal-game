namespace Core;

public static class DefaultData
{
  public static MapNode MapNode { get; } = new MapNode("Main World", [
    new("North Continent", [
      new("Black Forest", [
        new("Edge Town", [
        ])
      ]),
      new("Mountains", [
        new("Mount Doom")
      ])
    ]),
  ]);
}

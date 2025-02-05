namespace Core;

public static class DefaultData
{
  public static MapNode MapNode { get; } = new MapNode("Main World", [
    new("North Continent", [
      new("Black Forest", [
        new("Edge Town", [
        ]){
          Builds = [
            new("Town Center"){
              Items = [
                new("Wooden Sword"),
                new("Wooden Shield"),
                new("Wooden Axe"),
              ]
            },
            new("Blacksmith"),
          ],
        }
      ]),
      new("Mountains", [
        new("Mount Doom"){
          Items = [
            new("Fire Sword"),
          ]
        }
      ])
    ]),
  ]);
}

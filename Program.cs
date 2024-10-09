using map;

public class Program
{
  public static void Main()
  {
    Area main = new("main world");
    Area land = new("land");
    Area sea = new("sea");
    Area forest = new("forest");
    Area snow = new("snow");
    Area island = new("island");
    Area town = new("town");

    sea.Push([island]);
    land.Push([forest, snow, town]);
    main.Push([land, sea]);

    main.Init();
    main.ToFile();
  }
}

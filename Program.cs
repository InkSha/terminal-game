using Core;
public class Program
{
  public static void Main()
  {
    Game game = new();
    game.Start();
  }

  public static void Start()
  {
    CreateWorld();
  }

  public static void CreateWorld()
  {
    Area main = Area.LoadData(new("Main World")
    {
      Child = [
        new("land"){
          Child = [new("forest"), new("snow"), new("town"),]
        },
        new("sea"){ Child = [new("island")] }
      ]
    });
    main.Init();
    main.ToFile(main.Name);
  }
}

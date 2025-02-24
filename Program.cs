using Core;
public class Program
{
  const string DEBUG_FLAG = "debug";

  public static void Main(string[] args)
  {
    bool debug = args.Contains(DEBUG_FLAG, StringComparer.OrdinalIgnoreCase)
              || args.Contains($"--{DEBUG_FLAG}", StringComparer.OrdinalIgnoreCase);

    Print.Debug = debug;

    Game game = new();
    game.Start();
  }
}

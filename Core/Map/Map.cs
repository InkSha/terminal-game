namespace Core.Map;

public class Map(int width, int height)
{
  public int Height { get; set; } = height;
  public int Width { get; set; } = width;
  public static readonly char Wall = '#';
  public static readonly char Space = ' ';
  public static readonly char Character = 'q';
  public static readonly char Monster = '&';
  public static readonly char Boss = '@';
  public static readonly char Chest = 'm';
  public static readonly char Item = '*';

  public char[,] GenerateMap()
  {
    int head = 0;
    int foot = Height - 1;
    int tail = Width - 1;

    char[,] map = new char[Height, Width];

    Random random = new();

    for (int row = 0; row < Height; row++)
    {
      for (int col = 0; col < Width; col++)
      {
        char c = Space;
        if (
          // 天花板和地板
          row == head
          || row == foot
          || col == head
          || col == tail
        )
        {
          c = Wall;
        }
        else
        {
          int num = random.Next(0, 100);

          if (num > 70)
          {
            if (num > 99)
            {
              c = Boss;
            }
            else if (num > 95)
            {
              c = Chest;
            }
            else if (num > 90)
            {
              c = Item;
            }
            else if (num > 85)
            {
              c = Monster;
            }
          }
        }
        map[row, col] = c;
      }
    }
    return map;
  }

  public void Print()
  {
    char[,] map = GenerateMap();

    for (int row = 0; row < Height; row++)
    {
      for (int col = 0; col < Width; col++)
      {
        Console.Write(map[row, col]);
      }
      Console.Write('\n');
    }

  }
}

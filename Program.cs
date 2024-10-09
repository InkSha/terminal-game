using entity;
using map;

public class Program
{
  public static void Main()
  {
    CreateWorld();
    BaseEntity elf = new("elf");
    Random random = new();

    bool loop = true;

    while (loop)
    {
      Console.WriteLine("你行走在大地上");
      Console.WriteLine("怎么做?");
      Console.WriteLine("[walk] 接着走\n[find] 搜寻周围\n[end] 结束这一切");
      Console.WriteLine(new string('-', 30));

      string? input = Console.ReadLine();

      switch (input)
      {
        case "find":
          if (random.Next(0, 100) > 50)
          {
            BaseEntity monster = new("monster");
            bool war = true;
            Console.WriteLine("你遭遇了怪物!");

            while (war)
            {
              Console.WriteLine("怎么做?");
              Console.WriteLine("[attack] 攻击\n[defense] 防御\n[run] 逃跑");
              Console.WriteLine(new string('-', 30));
              string? select = Console.ReadLine();

              int damage;

              switch (select)
              {
                case "attack":
                  Console.WriteLine("攻击!");

                  damage = elf.Damage(monster);

                  Console.WriteLine($"你对 {monster.Name} 造成了 {damage} 伤害!");

                  if (monster.IsDeath())
                  {
                    Console.WriteLine($"你击败了 {monster.Name} !");
                    war = false;
                    loop = false;
                  }

                  break;
                case "defense":
                  Console.WriteLine("防御!");

                  damage = monster.Damage(elf);

                  Console.WriteLine($"{monster.Name} 对你造成了 {damage} 伤害!");
                  break;
                case "run":
                  Console.WriteLine("逃跑了!");
                  war = false;
                  loop = false;
                  break;
              }
            }
          }
          else
          {
            Console.WriteLine("一无所获");
          }
          break;
        case "end":
          loop = false;
          break;
      }
    }

  }

  public static void CreateWorld()
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

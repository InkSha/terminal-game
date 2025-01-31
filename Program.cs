using Core.Map;
using Core.GUI;
using Core.Time;

public class Program
{
  public static void Main()
  {
    Time time = new("23:58:56");
    Date date = new();

    GUIItem timeItem = new("时间", time.ToString());
    GUIItem dateItem = new("日期", date.ToString());

    time.TimeChange += (Time time) =>
    {
      timeItem.Data = time.ToString();
    };

    time.DayAdd += date.Increment;

    date.DateChange += (Date date) =>
    {
      dateItem.Data = date.ToString();
    };

    GUI gui = new([
      timeItem,
      dateItem,
      new("天气", "晴朗"),
      new("任务", "无"),
      new("位面", "主世界"),
      new("阵营", "无"),
      new("地区", "华夏未央市市区北部"),
      new("势力", "华夏"),
      new("场景", "市区广场"),
      new("人物", "张三、李四、王五、赵六"),
      new("物品", "无"),
      new("行为", "对话、查看"),
    ]);
    bool loop = true;

    int tickSec = 1;

    while (loop)
    {
      time.Tick(tickSec);
      gui.PrintUI();
      Console.WriteLine("怎么做?");
      string input = string.Format($"{Console.ReadLine()}");
      if (input.ToLower().StartsWith('q'))
      {
        loop = false;
      }
      else if (int.TryParse(input, out int num))
      {
        tickSec = num;
      }
    }
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

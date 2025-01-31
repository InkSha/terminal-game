using Core.Map;
using Core.GUI;

public class Program
{
  public static void Main()
  {
    GUI gui = new([
      new("时间", "12:00:00"),
      new("日期", "3000/01/01"),
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
    gui.PrintUI();
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

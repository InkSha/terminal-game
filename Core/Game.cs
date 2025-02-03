using Utils;

namespace Core;

public enum GameState
{
  Playing,
  End,
  Pause,
}

public class Game
{
  public readonly Setting setting = Setting.FromFile(Setting.SETTING_SAVE_PATH, new())!;
  public readonly Save save;
  public readonly MapManager mapManager = new();
  public readonly Time time = new();
  public readonly Date date = new();
  public readonly GUI gui = new([]);
  public GameState State { get; set; } = GameState.End;

  public Game()
  {
    save = LoadData();
    time.DayAdd = date.Increment;

    gui.Add([
      time,
      date,
      new GUIItem("天气", "晴朗"),
      new GUIItem("任务", "无"),
      // new GUIItem("位面", "主世界"),
      mapManager,
      new GUIItem("阵营", "无"),
      // new GUIItem("地区", "华夏未央市市区北部"),
      mapManager.Position,
      new GUIItem("势力", "华夏"),
      new GUIItem("场景", "市区广场"),
      new GUIItem("人物", "张三、李四、王五、赵六"),
      new GUIItem("物品", "无"),
      new GUIItem("行为", "对话、查看"),
    ]);
  }

  public void Start()
  {
    State = GameState.Playing;

    while (State != GameState.End)
    {
      if (State != GameState.Pause)
      {
        time.Tick();
        gui.PrintUI();
        Console.WriteLine("怎么做?");
      }
      string input = string.Format($"{Console.ReadLine()}");
      if (input.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
      {
        End();
      }
      else if (input.Equals("list", StringComparison.CurrentCultureIgnoreCase))
      {
        Console.WriteLine(string.Join("\n", mapManager.Root.ListAreas()));
        State = GameState.Pause;
      }
      else if (input.StartsWith("goto", StringComparison.CurrentCultureIgnoreCase))
      {
        string[] args = input.Split("goto ");
        if (args.Length > 1)
        {
          string position = args[1];
          if (mapManager.GoTo(position))
          {
            gui.ChangeItem(mapManager.CurrentNode!);
          }
        }
        State = GameState.Playing;
      }
      else if (input.Equals("start", StringComparison.CurrentCultureIgnoreCase))
      {
        State = GameState.Playing;
      }
    }
  }

  public void End()
  {
    State = GameState.End;
    SaveData();
    Console.WriteLine("游戏结束");
  }

  public Save LoadData()
  {
    var save = Save.LoadData(setting.DataSavePath);

    // load time and date
    if (save.Time is not null) time.ChangeTime(save.Time);
    if (save.Date is not null) date.ChangeDate(save.Date);

    // load map
    if (save.Map is not null) mapManager.LoadMap(save.Map);
    else mapManager.LoadMap(DefaultData.MapNode);

    // load current position
    if (save.CurrentPosition is not null)
    {
      mapManager.GoTo(save.CurrentPosition.Data);
    }

    save.Map = mapManager.Root;


    return save;
  }

  public void SaveData()
  {
    setting.ToFile(Setting.SETTING_SAVE_PATH);
    save.Time = time.ToString();
    save.Date = date.ToString();
    save.CurrentPosition = mapManager.CurrentNode;
    save.SaveData(setting.DataSavePath);
  }
}

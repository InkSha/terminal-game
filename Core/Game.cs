namespace Core;

public enum GameState
{
  Playing,
  End,
  // Pause,
}

public class Game
{
  public readonly Setting setting = Setting.FromFile(Setting.SETTING_SAVE_PATH, new())!;
  public readonly Save save;
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
      new GUIItem("位面", "主世界"),
      new GUIItem("阵营", "无"),
      new GUIItem("地区", "华夏未央市市区北部"),
      new GUIItem("势力", "华夏"),
      new GUIItem("场景", "市区广场"),
      new GUIItem("人物", "张三、李四、王五、赵六"),
      new GUIItem("物品", "无"),
      new GUIItem("行为", "对话、查看"),
    ]);
  }

  public Save LoadData()
  {
    var save = Save.LoadData(setting.DataSavePath);

    if (save.Time is not null) time.ChangeTime(save.Time);
    if (save.Date is not null) date.ChangeDate(save.Date);

    return save;
  }

  public void Start()
  {
    State = GameState.Playing;

    while (State != GameState.End)
    {
      time.Tick();
      gui.PrintUI();
      Console.WriteLine("怎么做?");
      string input = string.Format($"{Console.ReadLine()}");
      if (input.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
      {
        End();
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

  public void SaveData()
  {
    setting.ToFile(Setting.SETTING_SAVE_PATH);
    save.Time = time.ToString();
    save.Date = date.ToString();
    save.SaveData(setting.DataSavePath);
  }
}

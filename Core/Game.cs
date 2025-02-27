using System.Reflection.Metadata;

namespace Core;

public enum GameState
{
  Playing,
  End,
  Pause,
}

// TODO
// first. apply build and item to map
// second. add player and life

public partial class Game
{
  public readonly Setting setting = Setting.FromFile(Setting.SettingSavePath, new())!;
  public readonly Save save;
  public readonly Command command = new();
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
      mapManager.Position,
      new GUIItem("势力", "华夏"),
      new GUIItem("场景", "市区广场"),
      new GUIItem("人物", "张三、李四、王五、赵六"),
      new GUIItem("物品", "无"),
      new GUIItem("行为", "对话、查看"),
    ]);

    command.RegisterCommands([
      new CommandItem("quit", "退出游戏", new(){
        ActionCallbackHandler = (keyrowd, args) => End()
      }, "q"),
      new CommandItem("list", "查看地点列表", new(){
        ActionCallbackHandler = (keywrod, args) =>
        {
          Print.PrintText(mapManager.Root.ListAreas());
          State = GameState.Pause;
        }
      }, "ls"),
      new CommandItem("goto", "前往地点", new(){
        ActionCallbackHandler = (keywrod, args) =>
        {
          if (args.Length > 1)
          {
            string position = string.Join(" ", args[1..args.Length]);
            if (mapManager.GoTo(position))
            {
              gui.ChangeItem(mapManager.CurrentNode!);
            }
          }
          State = GameState.Playing;
        }
      }),
      new CommandItem("createword", "创建世界", new(){
        ActionCallbackHandler = (keywrod, args) =>
        {
          mapManager.Root.CreateMap("tmp");
        }
      }, "cw", "cworld"),
      new CommandItem("start", "开始游戏", new(){
        ActionCallbackHandler = (keywrod, args) =>
        {
          State = GameState.Playing;
        }
      }),
      new CommandItem("look", "查看周围", new() {
        ActionCallbackHandler = (keywrod, args) => {
          var areaFlag = "area";
          var buildFlag = "build";
          var itemFlag = "item";
          if (args.Length == 0) {
            args = [areaFlag, buildFlag, itemFlag];
          }
          if (args.Contains(areaFlag)) {
            var areas = mapManager.CurrentNode?.Areas;
            var parent = mapManager.CurrentNode?.GetParent();
            areas ??= [];

            if (parent is not null)
            {
              areas.Add(parent);
            }

            if (areas.Count == 0)
            {
              Print.PrintText("附近没有地点");
            }
            else
            {
              Print.PrintText("附近有以下地点:");
              foreach (var area in areas) {
                Print.PrintText($"  {area.Data}");
              }
            }
          }

          if (args.Contains(buildFlag)) {
            var builds = mapManager.CurrentNode!.ListBuilds();
            if (builds.Count == 0)
            {
              Print.PrintText("附近没有建筑");
            }
            else
            {
              Print.PrintText("附近有以下建筑:");
              foreach (var build in builds) {
                Print.PrintText($"  {build}");
              }
            }
          }

          if (args.Contains(itemFlag)) {
            var items = mapManager.CurrentNode!.ListItems();
            if (items.Count == 0)
            {
              Print.PrintText("附近没有物品");
            }
            else
            {
              Print.PrintText("附近有以下物品:") ;
              foreach (var item in items) {
                Print.PrintText($"  {item}");
              }
            }
          }
          State = GameState.Pause;
        }
      })
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
        Print.PrintText("怎么做?");
      }
      command.ExecuteCommand();
    }
  }

  public void End()
  {
    State = GameState.End;
    SaveData();
    Print.PrintText("游戏结束");
  }

  public Save LoadData()
  {
    var save = Save.LoadData(Setting.DataSavePath);

    // load time and date
    if (save.Time is not null) time.ChangeTime(save.Time);
    if (save.Date is not null) date.ChangeDate(save.Date);

    // load map
    if (save.Map is not null) mapManager.LoadMap(save.Map);
    else mapManager.LoadMap(DefaultData.MapNode);

    // load current position
    if (save.CurrentPosition is not null)
    {
      Print.PrintText($"当前位置: {save.CurrentPosition.Data}");
      mapManager.GoTo(save.CurrentPosition.Data);
    }

    save.Map = mapManager.Root;


    return save;
  }

  public void SaveData()
  {
    setting.ToFile(Setting.SettingSavePath);
    save.Time = time.ToString();
    save.Date = date.ToString();
    save.CurrentPosition = mapManager.CurrentNode;
    save.SaveData(Setting.DataSavePath);
  }
}

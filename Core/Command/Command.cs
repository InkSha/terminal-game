namespace Core;

public readonly struct CommandItem
{
  public string Keyword { get; }
  public string Description { get; }
  public Actions Callback { get; }
  public string[] Alias { get; }
  public CommandItem(string keyword, string description, Actions action)
  {
    Keyword = keyword;
    Description = description;
    Callback = action;
    Alias = [];
  }

  public CommandItem(string keyword, string description, Actions action, params string[] alias)
  {
    Keyword = keyword;
    Description = description;
    Callback = action;
    Alias = alias;
  }
}

public class Command
{
  public List<string> History { get; set; } = [];
  public List<CommandItem> Commands { get; } = [];

  public bool HasCommand(string keyword)
  {
    return Commands.Any(x => x.Keyword == keyword);
  }

  public bool HasAlias(string alias)
  {
    return Commands.Any(x => x.Alias.Any(x => x == alias));
  }

  public bool HasAlias(string[] alias)
  {
    return Commands.Any(command => command.Alias.Any(x => alias.Contains(x)));
  }

  public bool RegisterCommand(string keyword, string description, Actions action, params string[] alias)
  {
    if (HasCommand(keyword) || HasAlias(alias))
    {
      return false;
    }
    Commands.Add(new(keyword, description, action, alias));

    return HasCommand(keyword);
  }

  public bool RegisterCommand(CommandItem item)
  {
    if (HasCommand(item.Keyword) || HasAlias(item.Alias))
    {
      return false;
    }
    Commands.Add(item);
    return HasCommand(item.Keyword);
  }

  public List<bool> RegisterCommands(List<CommandItem> items)
  {
    var results = new List<bool>();
    foreach (var item in items)
    {
      results.Add(RegisterCommand(item));
    }
    return results;
  }

  public void Help()
  {
    foreach (var item in Commands)
    {
      int terminalWidth = Console.WindowWidth / 2;
      string space = new(' ', terminalWidth - item.Keyword.Length - item.Description.Length);
      Console.WriteLine($"{item.Keyword}{space}{item.Description}");

      if (item.Alias.Length > 0)
      {
        Console.WriteLine($"\talias:  {string.Join("、", item.Alias)}");
      }
    }
  }

  public object? ExecuteCommand()
  {
    string input = ReadInput();
    string[] args = input.Split(' ');
    string keyword = args[0];

    if (keyword.StartsWith("help", StringComparison.OrdinalIgnoreCase) || keyword == "?")
    {
      Help();
      input = ReadInput();
      args = input.Split(' ');
      keyword = args[0];
    }

    CommandItem? command = Commands.FirstOrDefault(command =>
    {
      return command.Keyword == keyword || command.Alias.Any(alias => alias == keyword);
    });

    if (command is null || command?.Callback == null)
    {
      return default;
    }
    else
    {
      return command?.Callback.Execute(keyword, args);
    }
  }

  public void PrintCommandList()
  {
    foreach (var item in Commands)
    {
      Console.WriteLine($"{item.Keyword}: {item.Description}");
    }
  }

  public string ReadInput()
  {
    string input = string.Format($"{Console.ReadLine()}");
    History.Add(input);
    return input;
  }
}

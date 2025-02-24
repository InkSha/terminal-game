public class Print
{
  public static bool Debug { get; set; } = false;

  public static void PrintText(string text)
  {
    Console.WriteLine(text);
  }

  public static void PrintText(params string[] text)
  {
    Console.WriteLine(string.Join("\n", text));
  }

  public static void PrintText(List<string> text)
  {
    Console.WriteLine(string.Join("\n", text));
  }

  public static void Clear()
  {
    if (!Debug) Console.Clear();
  }
}

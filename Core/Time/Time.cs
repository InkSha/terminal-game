namespace Core;

public class Time : IGUIItem
{

  public delegate void TimeChangeHandler(Time time);
  public delegate void DayAddHandler(int day);
  public TimeChangeHandler? TimeChange { get; set; }
  public DayAddHandler? DayAdd { get; set; }
  public int hour = 0;
  public int min = 0;
  public int sec = 0;
  public int SecRadix { get; set; } = 60;
  public int MinRadix { get; set; } = 60;
  public int HourRadix { get; set; } = 24;
  public int Day { get; set; } = 1;
  public char Separator { get; set; } = ':';
  public string Label { get; set; } = "时间";
  public string Hour { get => AddZero(hour); }
  public string Min { get => AddZero(min); }
  public string Sec { get => AddZero(sec); }

  public string Data
  {
    get => ToString();
    set => ChangeTime(value);
  }

  public Time(int hour, int min, int sec)
  {
    this.hour = hour;
    this.min = min;
    this.sec = sec;
    JudgeTime();
  }

  public Time(string time = "00:00:00")
  {
    ChangeTime(time);
  }

  private void JudgeTime()
  {
    if (hour >= HourRadix) hour = 0;
    if (min >= MinRadix) min = 0;
    if (sec >= SecRadix) sec = 0;

    Day = 1;
  }

  public void ChangeTime(string time = "00:00:00")
  {
    if (time.Equals(""))
    {
      return;
    }
    string hour = "";
    string min = "";
    string sec = "";
    int pointer = 0;
    foreach (char c in time)
    {
      if (int.TryParse(c.ToString(), out int num))
      {
        if (pointer == 0)
        {
          hour += c;
        }
        else if (pointer == 1)
        {
          min += c;
        }
        else if (pointer == 2)
        {
          sec += c;
        }
        else
        {
          break;
        }
      }
      else
      {
        pointer++;
      }
    }

    this.hour = int.Parse(hour);
    this.min = int.Parse(min);
    this.sec = int.Parse(sec);
    JudgeTime();
    TimeChange?.Invoke(this);
  }

  public static string AddZero(int time)
  {
    if (time < 10)
    {
      return $"0{time}";
    }
    return time.ToString();
  }

  public void Tick(int second = 1)
  {
    sec += second;
    while (sec >= SecRadix)
    {
      min++;
      sec -= SecRadix;
    }

    while (min >= MinRadix)
    {
      hour++;
      min -= MinRadix;
    }
    int currentDay = Day;
    while (hour >= HourRadix)
    {
      hour -= HourRadix;
      Day++;
    }

    TimeChange?.Invoke(this);

    if (currentDay != Day)
    {
      DayAdd?.Invoke(Day - currentDay);
    }

  }

  public override string ToString()
  {
    return $"{Hour}{Separator}{Min}{Separator}{Sec}";
  }
}

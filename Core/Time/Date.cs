using Core.GUI;

namespace Core.Time;

public class Date
{
  public delegate void DateChangeHandler(Date date);
  public DateChangeHandler? DateChange { get; set; }
  public int year;
  public int month;
  public int day;
  public int MonthRadix { get; set; } = 12;
  public int DayRadix { get; set; } = 30;
  public char Separator { get; set; } = '/';
  public string Label { get; set; } = "日期";

  public string Year { get => AddZero(year); }
  public string Month { get => AddZero(month); }
  public string Day { get => AddZero(day); }

  public Date(int year, int month, int day)
  {
    this.year = year;
    this.month = month;
    this.day = day;
  }

  public Date(string date = "2200/01/01")
  {
    string year = "";
    string month = "";
    string day = "";

    int pointer = 0;

    foreach (char c in date)
    {
      if (int.TryParse(c.ToString(), out int num))
      {
        if (pointer == 0)
        {
          year += c;
        }
        else if (pointer == 1)
        {
          month += c;
        }
        else if (pointer == 2)
        {
          day += c;
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

    this.year = int.Parse(year);
    this.month = int.Parse(month);
    this.day = int.Parse(day);
  }

  public static string AddZero(int date)
  {
    if (date < 10)
    {
      return $"0{date}";
    }
    else
    {
      return date.ToString();
    }
  }

  public void Increment(int date = 1)
  {
    day += date;
    while (day > DayRadix)
    {
      day -= DayRadix;
      month++;
    }

    while (month > MonthRadix)
    {
      month -= MonthRadix;
      year++;
    }

    DateChange?.Invoke(this);
  }

  public override string ToString()
  {
    return $"{Year}{Separator}{Month}{Separator}{Day}";
  }

  public GUIItem ToGUIItem()
  {
    GUIItem item = new(Label, ToString());

    DateChange += (date) =>
    {
      item.Data = date.ToString();
    };

    return item;
  }
}

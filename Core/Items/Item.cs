namespace Core;

public enum ItemType
{
  Other = 0b0000_0000_0000,
  Food = 0b0000_0000_0001,
  Weapon = 0b0000_0000_0010,
  Armor = 0b0000_0000_0100,
  Tool = 0b0000_0000_1000,
  Material = 0b0000_0001_0000,
  Consumable = 0b0000_0010_0000,
  Task = 0b0000_0100_0000,
  Magic = 0b0000_1000_0000,
}
public enum ItemEffect
{
  None,
}

public enum ItemQuality
{
  Poor,
  Common,
  Rare,
  Epic,
  Legendary,
  Mythic,
}

public interface IItems : IIdObject, IGUIItem
{
  public string Name { get; set; }
  public string Description { get; set; }
  public string Icon { get; set; }
  public int Weight { get; set; }
  public int Quantity { get; set; }
  public int Price { get; set; }
  public ItemQuality Quality { get; set; }
  public ItemType Type { get; set; }
  public List<ItemEffect> Effects { get; set; }
  public List<string> Tags { get; set; }
  public List<IItemAttribute> Attributes { get; set; }
  public List<string> TriggerEvents { get; set; }
}

public class ItemAttribute(string Name, int? Value, string? Data, string? Description) : IItemAttribute, IGUIItem
{
  public string Name { get; set; } = Name;
  public int Value { get; set; } = Value ?? 0;
  public string Data { get; set; } = Data ?? "";
  public string Label { get; set; } = Name;
  public string? Description { get; set; } = Description;
}

public class Items(string name) : IdObject(ITEM_LABEL), IItems
{
  public const string ITEM_LABEL = "物品";
  public const string ITEM_FILE_EXTEND = ".item.data.json";
  public string Name { get; set; } = name;
  public string Data => Name;
  public string Description { get; set; } = "";
  public string Icon { get; set; } = "";
  public int Weight { get; set; } = 0;
  public int Quantity { get; set; } = 1;
  public int Price { get; set; } = 0;
  public ItemQuality Quality { get; set; } = ItemQuality.Common;
  public ItemType Type { get; set; } = ItemType.Other;
  public List<ItemEffect> Effects { get; set; } = [];
  public List<string> Tags { get; set; } = [];
  public List<IItemAttribute> Attributes { get; set; } = [];
  public List<string> TriggerEvents { get; set; } = [];

  public bool CheckType(ItemType type)
  {
    return (Type & type) == type;
  }

  public bool CreateItem(string path = ".")
  {
    var file = $"{path}/{Data}{ITEM_FILE_EXTEND}";

    if (!File.Exists(file))
    {
      ToFile(file);
    }

    return File.Exists(file);
  }
}

using System.Text.Json;

namespace entity;

public class BaseEntity(string name)
{
  public string Name { get; set; } = name;
  protected int Level { get; set; } = 1;
  protected int Health { get; set; } = 100;
  protected int Attack { get; set; } = 10;
  protected int Defense { get; set; } = 5;
  protected int MissRate { get; set; } = 10;
  protected int HitRate { get; set; } = 0;
  protected bool Life { get; set; } = true;

  public string ToJson()
  {
    return JsonSerializer.Serialize(this);
  }

  public static BaseEntity FromFile(string filename)
  {
    string json = File.ReadAllText(filename);

    if (!string.IsNullOrEmpty(json))
    {
      BaseEntity? entity = JsonSerializer.Deserialize<BaseEntity>(json);

      if (entity is not null) return entity;
    }

    return new("");
  }

  public void ToFile(string? path = null)
  {
    string content = ToJson();
    string filename = Name + ".json";

    if (!string.IsNullOrEmpty(path))
    {
      filename = path;
    }
    File.WriteAllText(filename, content);
  }

  public bool IsDeath()
  {
    return !Life;
  }

  public int Damage(BaseEntity entity)
  {
    int damage = Attack - (entity.Defense / 2);

    entity.Health -= damage;

    if (entity.Health <= 0)
    {
      entity.Life = false;
    }

    return damage;
  }
}

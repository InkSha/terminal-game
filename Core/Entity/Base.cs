using Utils;

namespace Core.Entity;

public class BaseEntity(string name) : Json<BaseEntity>
{
  public string Name { get; set; } = name;
  protected int Level { get; set; } = 1;
  protected int Health { get; set; } = 100;
  protected int Attack { get; set; } = 10;
  protected int Defense { get; set; } = 5;
  protected int MissRate { get; set; } = 10;
  protected int HitRate { get; set; } = 0;
  protected bool Life { get; set; } = true;

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

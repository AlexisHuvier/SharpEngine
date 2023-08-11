using SharpEngine.Data.Save;

namespace SE_BasicWindow;

public class WeaponData
{
    public string Name { get; set; }
    public string Texture { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    public int Life { get; set; }
    public int Speed { get; set; }
    public int Attack { get; set; }
    public string ClassName { get; set; }
    public float Scale { get; set; }
    public bool IsActiveWeapon { get; set; }
    
    public WeaponData() {}

    public WeaponData(string name, string texture, string description, int level, int life, int speed, int attack,
        string className, float scale, bool isActiveWeapon)
    {
        Name = name;
        Texture = texture;
        Description = description;
        Level = level;
        Life = life;
        Speed = speed;
        Attack = attack;
        ClassName = className;
        Scale = scale;
        IsActiveWeapon = isActiveWeapon;
    }

    public void ToSave(ISave save, string prefix)
    {
        save.SetObject($"{prefix}_name", Name);
        save.SetObject($"{prefix}_icon", Texture);
        save.SetObject($"{prefix}_description", Description);
        save.SetObject($"{prefix}_level", Level);
        save.SetObject($"{prefix}_life", Life);
        save.SetObject($"{prefix}_speed", Speed);
        save.SetObject($"{prefix}_attack", Attack);
        save.SetObject($"{prefix}_class", ClassName);
        save.SetObject($"{prefix}_scale", Scale);
        save.SetObject($"{prefix}_active", IsActiveWeapon);
    }

    public static WeaponData FromSave(ISave save, string prefix)
    {
        return new WeaponData(
            save.GetObjectAs($"{prefix}_name", ""),
            save.GetObjectAs($"{prefix}_texture", ""),
            save.GetObjectAs($"{prefix}_description", ""),
            save.GetObjectAs($"{prefix}_level", 1),
            save.GetObjectAs($"{prefix}_life", 0),
            save.GetObjectAs($"{prefix}_speed", 0),
            save.GetObjectAs($"{prefix}_attack", 0),
            save.GetObjectAs($"{prefix}_class", ""),
            save.GetObjectAs($"{prefix}_scale", 1f),
            save.GetObjectAs($"{prefix}_active", true)
        );
    }
}
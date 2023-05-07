namespace Manamon.Data;

public struct Player
{
    public List<Monster> Team = new() { new Monster("Iolena") };
    public List<Monster> Others = new();
    public bool[] Manadex = { false, false, false };

    public Player()
    {
    }
}
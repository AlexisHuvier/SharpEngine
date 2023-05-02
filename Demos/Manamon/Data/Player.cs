namespace Manamon.Data;

public struct Player
{
    public List<Monster> Team = new();
    public List<Monster> Others = new();
    public bool[] Manadex = { false, false, false };

    public Player()
    {
    }
}
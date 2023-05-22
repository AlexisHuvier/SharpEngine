using System.Collections.Generic;

namespace SharpEngine.Utils;

public readonly struct Animation
{
    public readonly string Name;
    public readonly List<uint> Indices;
    public readonly float Timer;

    public Animation(string name, List<uint> indices, float timer)
    {
        Name = name;
        Indices = indices;
        Timer = timer;
    }
}
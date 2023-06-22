using System.Collections.Generic;

namespace SharpEngine.Utils;

/// <summary>
/// Struct which represents animation used by SpriteSheetComponent
/// </summary>
public readonly struct Animation
{
    /// <summary>
    /// Name of Animation
    /// </summary>
    public readonly string Name;
    
    /// <summary>
    /// Indices of frames
    /// </summary>
    public readonly List<uint> Indices;
    
    /// <summary>
    /// Timer between frames
    /// </summary>
    public readonly float Timer;

    /// <summary>
    /// Create Animation
    /// </summary>
    /// <param name="name">Animation</param>
    /// <param name="indices">Frame Indices</param>
    /// <param name="timer">Frame Timer</param>
    public Animation(string name, List<uint> indices, float timer)
    {
        Name = name;
        Indices = indices;
        Timer = timer;
    }
}
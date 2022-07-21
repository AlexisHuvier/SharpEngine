using System;

namespace SharpEngine.Utils;

/// <summary>
/// Temps du jeu
/// </summary>
public class GameTime
{
    public TimeSpan TotalGameTime { get; set; }
    public TimeSpan ElapsedGameTime { get; set; }
    public bool IsRunningSlowly { get; set; }

    public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool isRunningSlowly)
    {
        TotalGameTime = totalGameTime;
        ElapsedGameTime = elapsedGameTime;
        IsRunningSlowly = isRunningSlowly;
    }

    public static GameTime FromMonogameGameTime(Microsoft.Xna.Framework.GameTime gameTime) => new(gameTime.TotalGameTime, gameTime.ElapsedGameTime, gameTime.IsRunningSlowly);
}

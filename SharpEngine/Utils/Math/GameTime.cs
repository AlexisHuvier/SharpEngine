using System;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Temps du jeu
/// </summary>
public struct GameTime
{
    public TimeSpan TotalGameTime;
    public TimeSpan ElapsedGameTime;
    public bool IsRunningSlowly;

    public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool isRunningSlowly)
    {
        TotalGameTime = totalGameTime;
        ElapsedGameTime = elapsedGameTime;
        IsRunningSlowly = isRunningSlowly;
    }

    public static GameTime FromMonogameGameTime(Microsoft.Xna.Framework.GameTime gameTime) => new(gameTime.TotalGameTime, gameTime.ElapsedGameTime, gameTime.IsRunningSlowly);
}

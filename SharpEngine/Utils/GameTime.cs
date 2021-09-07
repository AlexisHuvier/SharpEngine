using System;

namespace SharpEngine
{
    public class GameTime
    {
        public TimeSpan totalGameTime { get; set; }
        public TimeSpan elapsedGameTime { get; set; }
        public bool isRunningSlowly { get; set; }

        public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool isRunningSlowly)
        {
            this.totalGameTime = totalGameTime;
            this.elapsedGameTime = elapsedGameTime;
            this.isRunningSlowly = isRunningSlowly;
        }

        internal static GameTime FromMonogameGameTime(Microsoft.Xna.Framework.GameTime gameTime)
        {
            return new GameTime(gameTime.TotalGameTime, gameTime.ElapsedGameTime, gameTime.IsRunningSlowly);
        }
    }
}

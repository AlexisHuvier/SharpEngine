using System;

namespace SharpEngine
{
    public class DebugManager
    {
        private static int frameRate = 0;
        private static int frameCounter = 0;
        private static TimeSpan elapsedTime = TimeSpan.Zero;

        public static int GetFPS()
        {
            return frameRate;
        }

        public static long GetGCMemory()
        {
            return GC.GetTotalMemory(false);
        }

        internal static void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.elapsedGameTime;

            if(elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        internal static void Draw()
        {
            frameCounter++;
        }
    }
}

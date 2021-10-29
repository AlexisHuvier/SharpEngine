namespace SharpEngine
{
    /// <summary>
    /// Fonctions et constantes mathématiques
    /// </summary>
    class Math
    {
        public const float E = 2.71828175F;
        public const float LOG10E = 0.4342945F;
        public const float LOG2E = 1.442695F;
        public const float PI = 3.14159274F;
        public const float PIOVER2 = 1.57079637F;
        public const float PIOVER4 = 0.7853982F;
        public const float TAU = 6.28318548F;
        public const float TWOPI = 6.28318548F;

        public static float ToDegrees(float radians)
        {
            return radians * 180 / PI;
        }
        public static float ToRadians(float degrees)
        {
            return degrees * PI / 180;
        }

        public static float Distance(float value1, float value2)
        {
            return System.Math.Abs(value2 - value1);
        }

        public static int Distance(int value1, int value2)
        {
            return System.Math.Abs(value2 - value1);
        }

        public static float Clamp(float value, float mini = float.MinValue, float maxi = float.MaxValue)
        {
            if (value < mini)
                return mini;
            else if (value > maxi)
                return maxi;
            else
                return value;
        }

        public static int Clamp(int value, int mini = int.MinValue, int maxi = int.MaxValue)
        {
            if (value < mini)
                return mini;
            else if (value > maxi)
                return maxi;
            else
                return value;
        }

        public static bool IntersectCircleRect(float cx, float cy, float radius, float rx, float ry, float rw, float rh)
        {

            // temporary variables to set edges for testing
            float testX = cx;
            float testY = cy;

            // which edge is closest?
            if (cx < rx) testX = rx;      // test left edge
            else if (cx > rx + rw) testX = rx + rw;   // right edge
            if (cy < ry) testY = ry;      // top edge
            else if (cy > ry + rh) testY = ry + rh;   // bottom edge

            // get distance from closest edges
            float distX = cx - testX;
            float distY = cy - testY;
            double distance = System.Math.Sqrt((distX * distX) + (distY * distY));

            // if the distance is less than the radius, collision!
            if (distance <= radius)
                return true;
            return false;
        }
    }
}

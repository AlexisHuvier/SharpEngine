namespace SharpEngine
{
    /// <summary>
    /// Fonctions et constantes mathématiques
    /// </summary>
    public class Math
    {
        public const float E = 2.71828175F;
        public const float LOG10E = 0.4342945F;
        public const float LOG2E = 1.442695F;
        public const float PI = 3.14159274F;
        public const float PIOVER2 = 1.57079637F;
        public const float PIOVER4 = 0.7853982F;
        public const float TAU = 6.28318548F;
        public const float TWOPI = 6.28318548F;

        public static float ToDegrees(float radians) => radians * 180 / PI;
        public static float ToRadians(float degrees) => degrees * PI / 180;

        public static float Distance(float value1, float value2) => System.Math.Abs(value2 - value1);
        public static int Distance(int value1, int value2) => System.Math.Abs(value2 - value1);

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
    }
}

using System;

namespace SharpEngine.Utils.Math;

/// <summary>
/// Fonctions et constantes mathématiques
/// </summary>
public static class MathUtils
{
    public const float E = 2.71828175F;
    public const float Log10E = 0.4342945F;
    public const float Log2E = 1.442695F;
    public const float Pi = 3.14159274F;
    public const float PiOver2 = 1.57079637F;
    public const float PiOver4 = 0.7853982F;
    public const float Tau = 6.28318548F;
    public const float TwoPi = 6.28318548F;

    private static Random _rand = new();

    public static float RandomBetween(float min, float max)
    {
        double range = max - min;
        var sample = _rand.NextDouble();
        return (float)(sample * range + min);
    }

    public static int RandomBetween(int min, int max) => _rand.Next(min, max + 1);

    public static float ToDegrees(float radians) => radians * 180 / Pi;
    public static float ToRadians(float degrees) => degrees * Pi / 180;

    public static float Distance(float value1, float value2) => System.Math.Abs(value2 - value1);
    public static int Distance(int value1, int value2) => System.Math.Abs(value2 - value1);

    public static float Clamp(float value, float mini = float.MinValue, float maxi = float.MaxValue)
    {
        if (value < mini)
            return mini;
        return value > maxi ? maxi : value;
    }

    public static int Clamp(int value, int mini = int.MinValue, int maxi = int.MaxValue)
    {
        if (value < mini)
            return mini;
        return value > maxi ? maxi : value;
    }
}

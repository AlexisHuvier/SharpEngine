using System;

namespace SharpEngine.Utils;

public class Rand
{
    private static readonly Random Random = new();

    public static int GetRand(int min, int max) => Random.Next(min, max);
    public static int GetRand(int max) => Random.Next(max);
    public static int GetRand() => Random.Next();

    public static float GetRandF(float min, float max) => Random.NextSingle() * (max - min) + min;
    public static float GetRandF(float max) => Random.NextSingle() * max;
    public static float GetRandF() => Random.NextSingle();
}
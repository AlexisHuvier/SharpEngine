using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des bruitages
/// </summary>
public static class SoundManager
{
    private static readonly Dictionary<string, SoundEffect> SoundEffects = new();

    internal static void Unload()
    {
        foreach (var soundEffect in SoundEffects)
            soundEffect.Value.Dispose();
        SoundEffects.Clear();
    }

    public static int Volume
    {
        get => (int)(SoundEffect.MasterVolume * 100f);
        set => SoundEffect.MasterVolume = value / 100f;
    }

    public static void AddSound(string name, string file)
    {
        if (!SoundEffects.ContainsKey(name))
            SoundEffects.Add(name, SoundEffect.FromFile(file));
    }

    public static void Play(string name, float volume = 1f, float pitch = 0f, float pan = 0f)
    {
        if (SoundEffects.TryGetValue(name, out var effect))
            effect.Play(volume, pitch, pan);
    }
}

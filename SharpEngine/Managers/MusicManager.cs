using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des musiques
/// </summary>
public static class MusicManager
{
    private static readonly Dictionary<string, Song> Songs = new();

    internal static void Unload()
    {
        foreach (var song in Songs)
            song.Value.Dispose();
        Songs.Clear();
    }

    public static bool Repeating
    {
        get => MediaPlayer.IsRepeating;
        set => MediaPlayer.IsRepeating = value;
    }

    public static bool Muted
    {
        get => MediaPlayer.IsMuted;
        set => MediaPlayer.IsMuted = value;
    }

    public static int Volume
    {
        get => (int)(MediaPlayer.Volume * 100f);
        set => MediaPlayer.Volume = value / 100f;
    }

    public static void AddSong(string name, Uri file)
    {
        if (!Songs.ContainsKey(name))
            Songs.Add(name, Song.FromUri(name, file));
    }

    public static void Play(string name)
    {
        if (Songs.TryGetValue(name, out var song))
            MediaPlayer.Play(song);
    }

    public static void Stop() => MediaPlayer.Stop();
}

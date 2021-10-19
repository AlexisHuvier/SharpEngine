using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

namespace SharpEngine
{
    /// <summary>
    /// Gestion des musiques
    /// </summary>
    public class MusicManager
    {
        private static Dictionary<string, Song> songs = new Dictionary<string, Song>();

        internal static void Unload()
        {
            foreach (KeyValuePair<string, Song> song in songs)
                song.Value.Dispose();
            songs.Clear();
        }

        public static void AddSong(string name, Uri file)
        {
            if (!songs.ContainsKey(name))
                songs.Add(name, Song.FromUri(name, file));
        }

        public static void Play(string name)
        {
            if (songs.ContainsKey(name))
                MediaPlayer.Play(songs[name]);
        }

        public static void Stop()
        {
            MediaPlayer.Stop();
        }

        public static void SetRepeating(bool repeat)
        {
            MediaPlayer.IsRepeating = repeat;
        }

        public static void SetVolume(int volume)
        {
            MediaPlayer.Volume = volume / 100f;
        }
    }
}

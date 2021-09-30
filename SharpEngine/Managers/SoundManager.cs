using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace SharpEngine
{
    public class SoundManager
    {
        private static Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        internal static void Unload()
        {
            foreach (KeyValuePair<string, SoundEffect> soundEffect in soundEffects)
                soundEffect.Value.Dispose();
            soundEffects.Clear();
        }

        public static void AddSound(string name, string file)
        {
            if (!soundEffects.ContainsKey(name))
                soundEffects.Add(name, SoundEffect.FromFile(file));
        }

        public static void Play(string name, float volume = 1f, float pitch = 0f, float pan = 0f)
        {
            if (soundEffects.ContainsKey(name))
                soundEffects[name].Play(volume, pitch, pan);
        }
    }
}

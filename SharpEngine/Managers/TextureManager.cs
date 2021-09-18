using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SharpEngine
{
    class TextureManager
    {
        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static void AddTexture(string name, Texture2D texture)
        {
            if (!textures.ContainsKey(name))
                textures.Add(name, texture);
        }

        public static Texture2D GetTexture(string name)
        {
            return textures.GetValueOrDefault(name, null);
        }

        public static void Unload(string name)
        {
            if (GetTexture(name) is Texture2D texture)
                texture.Dispose();
        }

        public static void Unload()
        {
            foreach (var texture in textures.Values)
                texture.Dispose();
            textures.Clear();
        }
    }
}

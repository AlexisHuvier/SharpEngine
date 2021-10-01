using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SharpEngine
{
    public class TextureManager
    {
        private Window window;
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, string> texturesWillBeCreated = new Dictionary<string, string>();

        internal TextureManager(Window window)
        {
            this.window = window;
        }

        public void AddTexture(string name, Texture2D texture)
        {
            if (!textures.ContainsKey(name))
            {
                textures.Add(name, texture);
            }
        }

        public void AddTexture(string name, string file)
        {
            if (!textures.ContainsKey(name))
            {
                if (window.internalGame.GraphicsDevice != null)
                    textures.Add(name, Texture2D.FromFile(window.internalGame.GraphicsDevice, file));
                else
                    texturesWillBeCreated.Add(name, file);
            }
        }

        internal void Load()
        {
            foreach (KeyValuePair<string, string> texture in texturesWillBeCreated)
                textures.Add(texture.Key, Texture2D.FromFile(window.internalGame.GraphicsDevice, texture.Value));
        }

        public Texture2D GetTexture(string name)
        {
            return textures.GetValueOrDefault(name, null);
        }

        internal void Unload(string name)
        {
            if (GetTexture(name) is Texture2D texture)
                texture.Dispose();
        }

        internal void Unload()
        {
            foreach (var texture in textures.Values)
                texture.Dispose();
            textures.Clear();
        }
    }
}

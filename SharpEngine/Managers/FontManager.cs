using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.IO;

namespace SharpEngine
{
    /// <summary>
    /// Gestion des polices
    /// </summary>
    public class FontManager
    {
        private Window window;
        private Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
        private Dictionary<string, KeyValuePair<string, int>> fontsWillBeCreated = new Dictionary<string, KeyValuePair<string, int>>();

        internal FontManager(Window window)
        {
            this.window = window;
        }

        public void AddFont(string name, string file, int fontSize = 25)
        {
            if (!fonts.ContainsKey(name))
            {
                if (window.internalGame.GraphicsDevice != null)
                {
                    var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(file),
                        fontSize,
                        1024,
                        1024,
                        new[]
                        {
                            CharacterRange.BasicLatin,
                            CharacterRange.Latin1Supplement,
                            CharacterRange.LatinExtendedA,
                            CharacterRange.Cyrillic
                        }
                    );

                    fonts.Add(name, fontBakeResult.CreateSpriteFont(window.internalGame.GraphicsDevice));
                }
                else
                    fontsWillBeCreated.Add(name, KeyValuePair.Create(file, fontSize));
            }
        }

        internal void Load()
        {
            foreach (KeyValuePair<string, KeyValuePair<string, int>> font in fontsWillBeCreated)
            {
                var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(font.Value.Key),
                        font.Value.Value,
                        1024,
                        1024,
                        new[]
                        {
                            CharacterRange.BasicLatin,
                            CharacterRange.Latin1Supplement,
                            CharacterRange.LatinExtendedA,
                            CharacterRange.Cyrillic
                        }
                    );

                fonts.Add(font.Key, fontBakeResult.CreateSpriteFont(window.internalGame.GraphicsDevice));
            }
        }

        public SpriteFont GetFont(string name)
        {
            if (fonts.ContainsKey(name))
                return fonts[name];
            else
                throw new System.Exception($"Font not founded : {name}");
        }
    }
}

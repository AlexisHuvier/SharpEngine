using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace SharpEngine.Managers;

/// <summary>
/// Gestion des polices
/// </summary>
public class FontManager
{
    private readonly Window _window;
    private readonly Dictionary<string, SpriteFont> _fonts = new();
    private readonly Dictionary<string, KeyValuePair<string, int>> _fontsWillBeCreated = new();

    internal FontManager(Window window)
    {
        _window = window;
    }

    public void AddFont(string name, string file, int fontSize = 25)
    {
        if (_fonts.ContainsKey(name)) return;
        
        if (_window.InternalGame.GraphicsDevice != null)
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

            _fonts.Add(name, fontBakeResult.CreateSpriteFont(_window.InternalGame.GraphicsDevice));
        }
        else
            _fontsWillBeCreated.Add(name, KeyValuePair.Create(file, fontSize));
    }

    internal void Load()
    {
        foreach (var font in _fontsWillBeCreated)
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

            _fonts.Add(font.Key, fontBakeResult.CreateSpriteFont(_window.InternalGame.GraphicsDevice));
        }
    }

    public SpriteFont GetFont(string name)
    {
        if (_fonts.TryGetValue(name, out var font))
            return font;
        throw new System.Exception($"Font not founded : {name}");
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant l'affichage d'un texte
/// </summary>
public class TextComponent: Component
{
    public string Text;
    public string Font;
    public Color Color;
    public bool Displayed;
    public Vec2 Offset;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="color">Couleur du texte (Color.BLACK)</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du texte (Vec2(0))</param>
    public TextComponent(string text = "", string font = "", Color color = null, bool displayed = true, Vec2 offset = null)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Displayed = displayed;
        Offset = offset ?? new Vec2(0);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed || Text.Length <= 0 ||
            Font.Length <= 0) return;
        
        var spriteFont = Entity.Scene.Window.FontManager.GetFont(Font);
        Renderer.RenderText(Entity.Scene.Window, spriteFont, Text, tc.Position + Offset - CameraManager.Position, Color, MathHelper.ToRadians(tc.Rotation), spriteFont.MeasureString(Text) / 2, tc.Scale, SpriteEffects.None, 1);
    }

    public override string ToString() => $"TextComponent(text={Text}, font={Font}, color={Color}, displayed={Displayed}, offset={Offset})";
}

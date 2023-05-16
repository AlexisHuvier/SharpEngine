using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant l'affichage d'un texte
/// </summary>
public class TextComponent: Component
{
    public string Text { get; set; }
    public string Font { get; set; }
    public Color Color { get; set; }
    public bool Displayed { get; set; }
    public Vec2 Offset { get; set; }

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="color">Couleur du texte (Color.BLACK)</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du texte (Vec2(0))</param>
    public TextComponent(string text = "", string font = "", Color color = null, bool displayed = true, Vec2? offset = null)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed || Text.Length <= 0 ||
            Font.Length <= 0) return;
        
        var spriteFont = Entity.Scene.Window.FontManager.GetFont(Font);
        var position = new Vec2(tc.Position.X + Offset.X - CameraManager.Position.X,
            tc.Position.Y + Offset.Y - CameraManager.Position.Y);
        Renderer.RenderText(Entity.Scene.Window, spriteFont, Text, position, Color, MathHelper.ToRadians(tc.Rotation), spriteFont.MeasureString(Text) / 2, tc.Scale, SpriteEffects.None, tc.LayerDepth);
    }

    public override string ToString() => $"TextComponent(text={Text}, font={Font}, color={Color}, displayed={Displayed}, offset={Offset})";
}

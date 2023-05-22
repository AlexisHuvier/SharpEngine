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
    public string Text;
    public string Font;
    public Color Color;
    public bool Displayed;
    public Vec2 Offset;

    private TransformComponent _transformComponent;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="text">Texte</param>
    /// <param name="font">Nom de la police</param>
    /// <param name="color">Couleur du texte (Color.BLACK)</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du texte (Vec2(0))</param>
    public TextComponent(string text = "", string font = "", Color? color = null, bool displayed = true, Vec2? offset = null)
    {
        Text = text;
        Font = font;
        Color = color ?? Color.Black;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
    }

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (_transformComponent == null || !Displayed || Text.Length <= 0 || Font.Length <= 0) return;
        
        var spriteFont = Entity.Scene.Window.FontManager.GetFont(Font);
        var position = new Vec2(
            _transformComponent.Position.X + Offset.X - CameraManager.Position.X,
            _transformComponent.Position.Y + Offset.Y - CameraManager.Position.Y
            );
        Renderer.RenderText(Entity.Scene.Window, spriteFont, Text, position, Color,
            MathHelper.ToRadians(_transformComponent.Rotation), spriteFont.MeasureString(Text) / 2,
            _transformComponent.Scale, SpriteEffects.None, _transformComponent.LayerDepth);
    }

    public override string ToString() => $"TextComponent(text={Text}, font={Font}, color={Color}, displayed={Displayed}, offset={Offset})";
}

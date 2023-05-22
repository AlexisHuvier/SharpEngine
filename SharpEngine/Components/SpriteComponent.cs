using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant l'affichage d'un sprite
/// </summary>
public class SpriteComponent: Component
{
    public string Sprite;
    public bool Displayed;
    public Vec2 Offset;
    public bool FlipX;
    public bool FlipY;

    private TransformComponent _transformComponent;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="sprite">Nom de la texture</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du Sprite (Vec2(0))</param>
    /// <param name="flipX">Si le sprite est retourné en X</param>
    /// <param name="flipY">Si le sprite est retourné en Y</param>
    public SpriteComponent(string sprite, bool displayed = true, Vec2? offset = null, bool flipX = false, bool flipY = false)
    {
        Sprite = sprite;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;
    }

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (_transformComponent == null || !Displayed || Sprite.Length <= 0) return;

        var effects = SpriteEffects.None;
        if (FlipX)
            effects |= SpriteEffects.FlipHorizontally;
        if (FlipY)
            effects |= SpriteEffects.FlipVertically;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture(Sprite);
        var position = new Vec2(
            _transformComponent.Position.X + Offset.X - CameraManager.Position.X,
            _transformComponent.Position.Y + Offset.Y - CameraManager.Position.Y
            );
        Renderer.RenderTexture(Entity.Scene.Window, texture, position, null, Color.White,
            MathHelper.ToRadians(_transformComponent.Rotation), new Vec2(texture.Width / 2f, texture.Height / 2f),
            _transformComponent.Scale, effects, _transformComponent.LayerDepth);
    }

    public override string ToString() => $"SpriteComponent(sprite={Sprite}, displayed={Displayed}, offset={Offset})";
}

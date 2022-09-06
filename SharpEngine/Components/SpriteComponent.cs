﻿using Microsoft.Xna.Framework;
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

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="sprite">Nom de la texture</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du Sprite (Vec2(0))</param>
    /// <param name="flipX">Si le sprite est retourné en X</param>
    /// <param name="flipY">Si le sprite est retourné en Y</param>
    public SpriteComponent(string sprite, bool displayed = true, Vec2 offset = null, bool flipX = false, bool flipY = false)
    {
        Sprite = sprite;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed || Sprite.Length <= 0) return;

        var effects = SpriteEffects.None;
        if (FlipX)
            effects |= SpriteEffects.FlipHorizontally;
        if (FlipY)
            effects |= SpriteEffects.FlipVertically;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture(Sprite);
        Renderer.RenderTexture(Entity.Scene.Window, texture, tc.Position + Offset - CameraManager.Position, null, Color.White, MathHelper.ToRadians(tc.Rotation), new Vec2(texture.Width / 2f, texture.Height / 2f), tc.Scale, effects, 1);
    }

    public override string ToString() => $"SpriteComponent(sprite={Sprite}, displayed={Displayed}, offset={Offset})";
}

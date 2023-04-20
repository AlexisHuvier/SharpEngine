using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant l'affichage d'animations à partir d'un SpriteSheet
/// </summary>
public class AnimSpriteSheetComponent : Component
{
    public string Sprite { get; set; }
    public Vec2 SpriteSize { get; set; }
    public List<Animation> Animations { get; set; }
    public bool Displayed { get; set; }
    public Vec2 Offset { get; set; }
    public bool FlipX { get; set; }
    public bool FlipY { get; set; }
    public string Anim
    {
        get => _currentAnim;
        set
        {
            _currentAnim = value;
            _currentImage = 0;
            _internalTimer = GetAnimation(_currentAnim)?.Timer ?? 0;
        }
    }

    private string _currentAnim;
    private int _currentImage;
    private float _internalTimer;

    /// <summary>
    /// Initialise le Componsant.
    /// </summary>
    /// <param name="sprite">Nom de la texture</param>
    /// <param name="spriteSize">Taille d'un sprite</param>
    /// <param name="animations">Dictionnaire des animations (nom -> liste id)</param>
    /// <param name="currentAnim">Animation actuelle </param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du Sprite (Vec2(0))</param>
    /// <param name="flipX">Si le sprite est retourné en X</param>
    /// <param name="flipY">Si le sprite est retourné en Y</param>
    public AnimSpriteSheetComponent(string sprite, Vec2 spriteSize, List<Animation> animations, 
        string currentAnim = "", bool displayed = true, Vec2? offset = null, bool flipX = false,
        bool flipY = false)
    {
        Sprite = sprite;
        SpriteSize = spriteSize;
        Animations = animations;
        _currentAnim = currentAnim;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;

        _currentImage = 0;
        _internalTimer = GetAnimation(_currentAnim)?.Timer ?? 0;
    }

    public Animation? GetAnimation(string name)
    {
        foreach(var animation in Animations)
        {
            if (animation.Name == name)
                return animation;
        }

        return null;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        var anim = GetAnimation(_currentAnim);

        if (anim == null) return;
        
        if (_internalTimer <= 0)
        {
            if (_currentImage >= anim.Value.Indices.Count - 1)
                _currentImage = 0;
            else
                _currentImage++;
            _internalTimer = anim.Value.Timer;
        }
        _internalTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        var anim = GetAnimation(_currentAnim);
        
        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed || Sprite.Length <= 0 ||
            _currentAnim.Length <= 0 || SpriteSize == new Vec2(0) || anim == null) return;
        
        var effects = SpriteEffects.None;
        if (FlipX)
            effects |= SpriteEffects.FlipHorizontally;
        if (FlipY)
            effects |= SpriteEffects.FlipVertically;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture(Sprite);
        
        // ReSharper disable once PossibleLossOfFraction
        var positionSource = new Vec2(SpriteSize.X * (anim.Value.Indices[_currentImage] % (texture.Width / SpriteSize.X)), SpriteSize.Y * (anim.Value.Indices[_currentImage] / (int)(texture.Width / SpriteSize.X))); 
        Renderer.RenderTexture(Entity.Scene.Window, texture, tc.Position + Offset - CameraManager.Position, new Rect(positionSource, SpriteSize), Color.White, MathHelper.ToRadians(tc.Rotation), SpriteSize / 2, tc.Scale, effects, 1);
    }

    public override string ToString() => $"SpriteSheetComponent(sprite={Sprite}, spriteSize={SpriteSize}, nbAnimations={Animations.Count}, currentAnim={_currentAnim})";
}

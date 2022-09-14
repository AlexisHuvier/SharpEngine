﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

/// <summary>
/// Composant ajoutant l'affichage d'animations à partir d'un SpriteSheet
/// </summary>
public class AnimSpriteSheetComponent : Component
{
    public string Sprite;
    public Vec2 SpriteSize;
    public Dictionary<string, List<int>> Animations;
    public bool Displayed;
    public float Timer;
    public Vec2 Offset;
    public bool FlipX;
    public bool FlipY;
    
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
    /// <param name="timer">Temps en ms entre chaque frame</param>
    /// <param name="displayed">Est affiché</param>
    /// <param name="offset">Décalage de la position du Sprite (Vec2(0))</param>
    /// <param name="flipX">Si le sprite est retourné en X</param>
    /// <param name="flipY">Si le sprite est retourné en Y</param>
    public AnimSpriteSheetComponent(string sprite, Vec2 spriteSize, Dictionary<string, List<int>> animations, 
        string currentAnim = "", float timer = 250, bool displayed = true, Vec2 offset = null, bool flipX = false,
        bool flipY = false)
    {
        Sprite = sprite;
        SpriteSize = spriteSize;
        Animations = animations;
        _currentAnim = currentAnim;
        Timer = timer;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
        FlipX = flipX;
        FlipY = flipY;

        _currentImage = 0;
        _internalTimer = timer;
    }

    public void SetAnim(string anim)
    {
        _currentAnim = anim;
        _currentImage = 0;
        _internalTimer = Timer;
    }

    public string GetAnim() => _currentAnim;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!Animations.ContainsKey(_currentAnim)) return;
        
        if (_internalTimer <= 0)
        {
            if (_currentImage >= Animations[_currentAnim].Count - 1)
                _currentImage = 0;
            else
                _currentImage++;
            _internalTimer = Timer;
        }
        _internalTimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed || Sprite.Length <= 0 ||
            _currentAnim.Length <= 0 || SpriteSize == new Vec2(0) || !Animations.ContainsKey(_currentAnim)) return;
        
        var effects = SpriteEffects.None;
        if (FlipX)
            effects |= SpriteEffects.FlipHorizontally;
        if (FlipY)
            effects |= SpriteEffects.FlipVertically;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture(Sprite);
        
        // ReSharper disable once PossibleLossOfFraction
        var positionSource = new Vec2(SpriteSize.X * (Animations[_currentAnim][_currentImage] % (texture.Width / SpriteSize.X)), SpriteSize.Y * (Animations[_currentAnim][_currentImage] / (int)(texture.Width / SpriteSize.X))); 
        Renderer.RenderTexture(Entity.Scene.Window, texture, tc.Position + Offset - CameraManager.Position, new Rect(positionSource, SpriteSize), Color.White, MathHelper.ToRadians(tc.Rotation), SpriteSize / 2, tc.Scale, effects, 1);
    }

    public override string ToString() => $"SpriteSheetComponent(sprite={Sprite}, spriteSize={SpriteSize}, nbAnimations={Animations.Count}, currentAnim={_currentAnim})";
}
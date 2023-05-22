using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Core;
using SharpEngine.Managers;
using SharpEngine.Utils.Math;
using Color = SharpEngine.Utils.Color;
using GameTime = SharpEngine.Utils.Math.GameTime;

namespace SharpEngine.Components;

public class RectComponent: Component
{
    public Color Color;
    public Vec2 Size;
    public bool Displayed;
    public Vec2 Offset;

    private TransformComponent _transformComponent;
    
    public RectComponent(Color color, Vec2 size, bool displayed = true, Vec2? offset = null)
    {
        Color = color;
        Size = size;
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
        
        if (_transformComponent == null || !Displayed) return;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture("blank");
        var size = Size * _transformComponent.Scale;
        Renderer.RenderTexture(Entity.Scene.Window, texture,
            new Rect(_transformComponent.Position.X + Offset.X - CameraManager.Position.X - size.X / 2,
                _transformComponent.Position.Y + Offset.Y - CameraManager.Position.Y - size.Y / 2, size), Color,
            MathHelper.ToRadians(_transformComponent.Rotation), Vec2.Zero, SpriteEffects.None,
            _transformComponent.LayerDepth);
    }
}
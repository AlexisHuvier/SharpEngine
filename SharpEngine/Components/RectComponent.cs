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
    public Color Color { get; set; }
    public Vec2 Size { get; set; }
    public bool Displayed { get; set; }
    public Vec2 Offset { get; set; }
    
    public RectComponent(Color color, Vec2 size, bool displayed = true, Vec2? offset = null)
    {
        Color = color;
        Size = size;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        if (Entity.GetComponent<TransformComponent>() is not { } tc || !Displayed) return;
        
        var texture = Entity.Scene.Window.TextureManager.GetTexture("blank");
        var position = new Vec2(tc.Position.X + Offset.X - CameraManager.Position.X,
            tc.Position.Y + Offset.Y - CameraManager.Position.Y);
        var size = Size * tc.Scale;
        Console.WriteLine(new Rect(position, size));
        Renderer.RenderTexture(Entity.Scene.Window, texture, new Rect(position - size / 2, size), Color, MathHelper.ToRadians(tc.Rotation), Vec2.Zero, SpriteEffects.None, tc.LayerDepth);
    }
}
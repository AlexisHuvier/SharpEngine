﻿using SharpEngine.Core;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Widgets;

public class Frame: Widget
{
    public Color Color;
    public Vec2 Size;
    public Vec2 BorderSize;

    /// <summary>
    /// Initialise le Widget.
    /// </summary>
    /// <param name="position">Position (Vec2(0))</param>
    /// <param name="size">Taille du rectangle (Vec2(5))</param>
    /// <param name="borderSize">Taille de la bordure (Vec2(3))</param>
    /// <param name="color">Couleur du rectangle</param>
    public Frame(Vec2 position = null, Vec2 size = null, Vec2 borderSize = null, Color color = null) : base(position)
    {
        Color = color ?? Color.Black;
        Size = size ?? new Vec2(5);
        BorderSize = borderSize ?? new Vec2(3);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        
        if (!Displayed || Scene == null) return;

        var texture = Scene.Window.TextureManager.GetTexture("blank");
        var realPosition = Parent != null ? Position + Parent.Position : Position;
        
        Renderer.RenderTexture(Scene.Window, texture, new Rect(realPosition - Size / 2, Size.X, BorderSize.Y), Color);
        Renderer.RenderTexture(Scene.Window, texture, new Rect(realPosition - Size / 2, BorderSize.X, Size.Y), Color);
        Renderer.RenderTexture(Scene.Window, texture,
            new Rect(realPosition.X - Size.X / 2, realPosition.Y + (Size.Y - BorderSize.Y) - Size.Y / 2, Size.X,
                BorderSize.Y), Color);
        Renderer.RenderTexture(Scene.Window, texture,
            new Rect(realPosition.X + (Size.X - BorderSize.X) - Size.X / 2, realPosition.Y - Size.Y / 2, BorderSize.X,
                Size.Y), Color);
    }
}
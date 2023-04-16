#nullable enable
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Core;

public static class Renderer
{
    public static void RenderTexture(Window window, Texture2D texture, Vec2 position, Rect? sourceRectangle, Color color, float rotation, Vec2 origin, Vec2 scale, SpriteEffects effets, float layerDepth)
    {
        var sizeX = sourceRectangle != null ? sourceRectangle.Value.Size.X * scale.X : texture.Width * scale.X;
        var sizeY = sourceRectangle != null ? sourceRectangle.Value.Size.Y * scale.Y : texture.Height * scale.Y;
        var windowSize = window.ScreenSize;

        if (position.X - origin.X - sizeX * scale.X >= windowSize.X || position.X - origin.X + sizeX * scale.X <= 0 ||
            position.Y - origin.Y - sizeY * scale.Y >= windowSize.Y || position.Y - origin.Y + sizeY * scale.Y <= 0) return;
        
        if (sourceRectangle is null)
            window.InternalGame.SpriteBatch.Draw(texture, position.ToMg(), null, color.ToMg(), rotation,
                origin.ToMg(), scale.ToMg(), effets, layerDepth);
        else
            window.InternalGame.SpriteBatch.Draw(texture, position.ToMg(), sourceRectangle.Value.ToMg(), color.ToMg(),
                rotation, origin.ToMg(), scale.ToMg(), effets, layerDepth);
    }

    public static void RenderTexture(Window window, Texture2D texture, Rect destinationRectangle, Color color)
    {
        var position = destinationRectangle.Position;
        var size = destinationRectangle.Size;
        var windowSize = window.ScreenSize;

        if (position.X >= windowSize.X || position.X + size.X <= 0 || position.Y >= windowSize.Y || position.Y + size.Y <= 0) return;
        
        window.InternalGame.SpriteBatch.Draw(texture, destinationRectangle.ToMg(), color.ToMg());
    }

    public static void RenderText(Window window, SpriteFont font, string text, Vec2 position, Color color, float rotation, Vec2 origin, Vec2 scale, SpriteEffects effects, float layerDepth)
    {
        var size = font.MeasureString(text) * scale;
        var windowSize = window.ScreenSize;

        if (position.X - origin.X - size.X * scale.X >= windowSize.X || position.X - origin.X + size.X * scale.X <= 0 ||
            position.Y - origin.Y - size.Y * scale.Y >= windowSize.Y || position.Y - origin.Y + size.Y * scale.Y <= 0) return;
        
        window.InternalGame.SpriteBatch.DrawString(font, text, position.ToMg(), color.ToMg(), rotation, origin.ToMg(), scale.ToMg(), effects, layerDepth);
    }

    public static void RenderText(Window window, SpriteFont font, string text, Vec2 position, Color color)
    {
        var size = font.MeasureString(text);
        var windowSize = window.ScreenSize;

        if (position.X >= windowSize.X || position.X + size.X <= 0 || position.Y >= windowSize.Y || position.Y + size.Y <= 0) return;
        window.InternalGame.SpriteBatch.DrawString(font, text, position.ToMg(), color.ToMg());
    }
}
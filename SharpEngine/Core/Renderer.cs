#nullable enable
using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngine.Core;

public static class Renderer
{
    public static void RenderTexture(Window window, Texture2D texture, Vec2 position, Rect? sourceRectangle, Color color, float rotation, Vec2 origin, Vec2 scale, SpriteEffects effets, float layerDepth)
    {
        var positionTemp = position - origin;
        var size = sourceRectangle != null ? sourceRectangle.Size * scale : new Vec2(texture.Width, texture.Height) * scale;
        var windowSize = window.ScreenSize;

        if (positionTemp.X >= windowSize.X || positionTemp.X + size.X <= 0 || positionTemp.Y >= windowSize.Y || positionTemp.Y + size.Y <= 0) return;
        
        if (sourceRectangle is null)
            window.InternalGame.SpriteBatch.Draw(texture, position.ToMg(), null, color.ToMg(), rotation,
                origin.ToMg(),
                scale.ToMg(), effets, layerDepth);
        else
            window.InternalGame.SpriteBatch.Draw(texture, position.ToMg(), sourceRectangle.ToMg(), color.ToMg(),
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
        var positionTemp = position - origin;
        var size = font.MeasureString(text) * scale;
        var windowSize = window.ScreenSize;

        if (positionTemp.X >= windowSize.X || positionTemp.X + size.X <= 0 || positionTemp.Y >= windowSize.Y || positionTemp.Y + size.Y <= 0) return;
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
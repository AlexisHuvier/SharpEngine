using Raylib_cs;
using SharpEngine.Math;
using Color = SharpEngine.Utils.Color;

namespace SharpEngine.Component;

/// <summary>
/// Component which display sprite
/// </summary>
public class SpriteComponent: Component
{
    /// <summary>
    /// Name of Texture which be displayed
    /// </summary>
    public string Texture;
    
    /// <summary>
    /// Define if Sprite is displayed
    /// </summary>
    public bool Displayed;
    
    /// <summary>
    /// Offset of Sprite
    /// </summary>
    public Vec2 Offset;

    private TransformComponent? _transformComponent;

    /// <summary>
    /// Create SpriteComponent
    /// </summary>
    /// <param name="texture">Name Texture Displayed</param>
    /// <param name="displayed">If Texture is Displayed (true)</param>
    /// <param name="offset">Offset (Vec2(0))</param>
    public SpriteComponent(string texture, bool displayed = true, Vec2? offset = null)
    {
        Texture = texture;
        Displayed = displayed;
        Offset = offset ?? Vec2.Zero;
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transformComponent = Entity?.GetComponentAs<TransformComponent>();
    }

    /// <inheritdoc />
    public override void Draw()
    {
        base.Draw();

        var window = Entity?.Scene?.Window;
        
        if(_transformComponent == null || !Displayed || Texture.Length <= 0 || window == null) return;

        var texture = window.TextureManager.GetTexture(Texture);
        var position = _transformComponent.GetTransformedPosition(Offset);
        Raylib.DrawTexture(texture, (int)(position.X - texture.width / 2f), (int)(position.Y - texture.height / 2f),
            Color.White);
    }
}
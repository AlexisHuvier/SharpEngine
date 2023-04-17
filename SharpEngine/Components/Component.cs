﻿using Microsoft.Xna.Framework.Graphics;
using SharpEngine.Entities;
using SharpEngine.Utils.Math;
using SharpEngine.Utils.Control;

namespace SharpEngine.Components;

/// <summary>
/// Composant basique
/// </summary>
public class Component
{
    internal Entity Entity;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    protected Component()
    {
        Entity = null;
    }

    public virtual void SetEntity(Entity entity) => Entity = entity;
    public Entity GetEntity() => Entity;
    public SpriteBatch GetSpriteBatch() => Entity?.Scene?.Window.InternalGame.SpriteBatch;
    public Window GetWindow() => Entity?.Scene?.Window;

    public virtual void Initialize() {}
    public virtual void LoadContent() {}
    public virtual void UnloadContent() {}
    public virtual void TextInput(object sender, Key key, char character) {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(GameTime gameTime) {}
}

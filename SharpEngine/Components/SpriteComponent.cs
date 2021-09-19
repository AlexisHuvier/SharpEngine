using Microsoft.Xna.Framework;

namespace SharpEngine.Components
{
    public class SpriteComponent: Component
    {
        public string sprite;
        public bool displayed;

        public SpriteComponent(params object[] parameters): base(parameters)
        {
            sprite = "";
            displayed = true;

            if (parameters.Length >= 1 && parameters[0] is string spr)
                sprite = spr;
            if (parameters.Length >= 2 && parameters[1] is bool disp)
                displayed = disp;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && sprite.Length > 0) {
                var texture = entity.scene.window.textureManager.GetTexture(sprite);
                entity.scene.window.internalGame.spriteBatch.Draw(texture, tc.position.ToMonoGameVector(), null, Color.WHITE.ToMonoGameColor(), MathHelper.ToRadians(tc.rotation), new Vector2(texture.Width, texture.Height) / 2, tc.scale.ToMonoGameVector(), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1);
            }
        }

        public override string ToString()
        {
            return $"SpriteComponent(sprite={sprite}, displayed={displayed})";
        }
    }
}

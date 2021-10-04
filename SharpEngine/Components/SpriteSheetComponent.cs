using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SharpEngine.Components
{
    public class SpriteSheetComponent : Component
    {
        public string sprite;
        public Vec2 spriteSize;
        public Dictionary<string, List<int>> anims;
        private string currentAnim;
        private int currentImage;
        public float timer;
        private float internalTimer;
        public bool displayed;

        public SpriteSheetComponent(params object[] parameters) : base(parameters)
        {
            sprite = "";
            spriteSize = new Vec2(0);
            anims = new Dictionary<string, List<int>>();
            currentAnim = "";
            timer = 250;
            displayed = true;

            if (parameters.Length >= 1 && parameters[0] is string spr)
                sprite = spr;
            if (parameters.Length >= 2 && parameters[1] is Vec2 sSize)
                spriteSize = sSize;
            if (parameters.Length >= 3 && parameters[2] is Dictionary<string, List<int>> animations)
                anims = animations;
            if (parameters.Length >= 4 && parameters[3] is string currentAnimation)
                currentAnim = currentAnimation;
            if (parameters.Length >= 5 && parameters[4] is float tim)
                timer = tim;
            if (parameters.Length >= 6 && parameters[5] is bool disp)
                displayed = disp;

            currentImage = 0;
            internalTimer = timer;
        }

        public void SetAnim(string anim)
        {
            currentAnim = anim;
            currentImage = 0;
            internalTimer = timer;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentAnim.Length >= 0 && anims.ContainsKey(currentAnim))
            {
                if (internalTimer <= 0)
                {
                    if (currentImage >= anims[currentAnim].Count - 1)
                        currentImage = 0;
                    else
                        currentImage++;
                    internalTimer = timer;
                }
                internalTimer -= (float)gameTime.elapsedGameTime.TotalMilliseconds;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && sprite.Length > 0 && currentAnim.Length > 0 && spriteSize != new Vec2(0) && anims.ContainsKey(currentAnim))
            {
                var texture = GetWindow().textureManager.GetTexture(sprite);
                Vec2 positionSource = new Vec2(spriteSize.x * (int)(anims[currentAnim][currentImage] % (texture.Width / spriteSize.x)), spriteSize.y * (anims[currentAnim][currentImage] / (int)(texture.Height / spriteSize.y)));
                GetSpriteBatch().Draw(texture, (tc.position - CameraManager.position).ToMG(), new Rect(positionSource, spriteSize).ToMG(), Color.WHITE.ToMG(), MathHelper.ToRadians(tc.rotation), (spriteSize / 2).ToMG(), tc.scale.ToMG(), SpriteEffects.None, 1);
            }
        }
    }
}

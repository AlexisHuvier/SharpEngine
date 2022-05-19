using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SharpEngine.Components
{
    /// <summary>
    /// Composant ajoutant l'affichage d'animations à partir d'un SpriteSheet
    /// </summary>
    public class SpriteSheetComponent : Component
    {
        public string sprite;
        public Vec2 spriteSize;
        public Dictionary<string, List<int>> animations;
        private string currentAnim;
        private int currentImage;
        public float timer;
        private float internalTimer;
        public bool displayed;

        /// <summary>
        /// Initialise le Componsant.
        /// </summary>
        /// <param name="sprite">Nom de la texture</param>
        /// <param name="spriteSize">Taille d'un sprite</param>
        /// <param name="animations">Dictionnaire des animations (nom -> liste id)</param>
        /// <param name="currentAnim">Animation actuelle </param>
        /// <param name="timer">Temps en ms entre chaque frame</param>
        /// <param name="displayed">Est affiché</param>
        public SpriteSheetComponent(string sprite, Vec2 spriteSize, Dictionary<string, List<int>> animations, string currentAnim = "", float timer = 250, bool displayed = true) : base()
        {
            this.sprite = sprite;
            this.spriteSize = spriteSize;
            this.animations = animations;
            this.currentAnim = currentAnim;
            this.timer = timer;
            this.displayed = displayed;

            currentImage = 0;
            internalTimer = timer;
        }

        public void SetAnim(string anim)
        {
            currentAnim = anim;
            currentImage = 0;
            internalTimer = timer;
        }

        public string GetAnim() => currentAnim;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (currentAnim.Length >= 0 && animations.ContainsKey(currentAnim))
            {
                if (internalTimer <= 0)
                {
                    if (currentImage >= animations[currentAnim].Count - 1)
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

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc && displayed && sprite.Length > 0 && currentAnim.Length > 0 && spriteSize != new Vec2(0) && animations.ContainsKey(currentAnim))
            {
                var texture = GetWindow().textureManager.GetTexture(sprite);
                Vec2 positionSource = new Vec2(spriteSize.x * (int)(animations[currentAnim][currentImage] % (texture.Width / spriteSize.x)), spriteSize.y * (animations[currentAnim][currentImage] / (int)(texture.Height / spriteSize.y)));
                GetSpriteBatch().Draw(texture, (tc.position - CameraManager.position).ToMG(), new Rect(positionSource, spriteSize).ToMG(), Color.WHITE.ToMG(), MathHelper.ToRadians(tc.rotation), (spriteSize / 2).ToMG(), tc.scale.ToMG(), SpriteEffects.None, 1);
            }
        }

        public override string ToString() => $"SpriteSheetComponent(sprite={sprite}, spriteSize={spriteSize}, nbAnimations={animations.Count}, currentAnim={currentAnim})";
    }
}

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Barre de progresssion
    /// </summary>
    public class ProgressBar: Widget
    {
        public Color color;
        public Vec2 size;
        public int value;

        /// <summary>
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Couleur de la barre (<seealso cref="Color"/>) (Color.GREEN)<para/>
        /// -> Paramètre 3 : Taille (<seealso cref="Vec2"/>) (Vec2(200, 30))<para/>
        /// -> Paramètre 4 : Valeur (int) (0)
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public ProgressBar(params object[] parameters): base(parameters)
        {
            color = Color.GREEN;
            size = new Vec2(200, 30);
            value = 0;

            if (parameters.Length >= 2 && parameters[1] is Color clr)
                color = clr;
            if (parameters.Length >= 3 && parameters[2] is Vec2 siz)
                size = siz;
            if (parameters.Length >= 4 && parameters[3] is int val)
                value = val;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (scene == null || !displayed)
                return;

            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - size / 2, size).ToMG(), Color.BLACK.ToMG());
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - (size - new Vec2(4)) / 2, (size - new Vec2(4))).ToMG(), Color.WHITE.ToMG());
            Vec2 barSize = (size - new Vec2(8));
            Vec2 realSize = new Vec2(barSize.x * value / 100, barSize.y);
            scene.window.internalGame.spriteBatch.Draw(scene.window.textureManager.GetTexture("blank"), new Rect(position - barSize / 2, realSize).ToMG(), color.ToMG());
        }
    }
}

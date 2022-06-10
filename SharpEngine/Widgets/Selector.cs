using System.Collections.Generic;

namespace SharpEngine.Widgets
{
    /// <summary>
    /// Sélecteur
    /// </summary>
    public class Selector: Widget
    {
        private readonly List<string> texts;
        private Button LeftButton;
        private Button RightButton;
        private Label text;
        private string font;
        private int selected;

        /// <summary>
        /// Initialise le Widget.
        /// </summary>
        /// <param name="position">Position (Vec2(0))</param>
        /// <param name="font">Nom de la police</param>
        /// <param name="texts">Liste des possibilités</param>
        public Selector(Vec2 position = null, string font = "", List<string> texts = null) : base(position)
        {
            this.font = font;
            this.texts = texts ?? new List<string>();
            selected = 0;
        }

        public string GetValue() => texts[selected];

        public override void LoadContent()
        {
            base.LoadContent();

            float maxsize = 0;
            foreach (string text in texts)
            {
                float temp = scene.window.fontManager.GetFont(font).MeasureString(text).X;
                if (maxsize < temp)
                    maxsize = temp;
            }
            LeftButton = new Button(position + new Vec2(20) / 2 - new Vec2(maxsize / 2 + 25, 20 / 2), "<", font, new Vec2(20))
            {
                command = (Button b) =>
                {
                    if (selected == 0)
                        selected = texts.Count - 1;
                    else
                        selected--;
                    text.text = texts[selected];
                }
            };
            RightButton = new Button(position + new Vec2(20) / 2 + new Vec2(maxsize / 2 + 10, -20 / 2), ">", font, new Vec2(20))
            {
                command = (Button b) =>
                {
                    if (selected == texts.Count - 1)
                        selected = 0;
                    else
                        selected++;
                    text.text = texts[selected];
                }
            };
            text = new Label(position, texts[selected], font);

            LeftButton.SetScene(scene);
            RightButton.SetScene(scene);
            text.SetScene(scene);

            LeftButton.SetParent(parent);
            RightButton.SetParent(parent);
            text.SetParent(parent);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LeftButton.Update(gameTime);
            RightButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (scene == null)
                return;

            LeftButton.Draw(gameTime);
            RightButton.Draw(gameTime);
            text.Draw(gameTime);
        }
    }
}

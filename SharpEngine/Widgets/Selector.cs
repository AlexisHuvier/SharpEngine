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
        /// Initialise le Widget.<para/>
        /// -> Paramètre 1 : Position (<seealso cref="Vec2"/>) (Vec2(0))<para/>
        /// -> Paramètre 2 : Nom de la police (string) ("")<para/>
        /// -> Paramètre 3.. : Liste des mots (null)<para/>
        /// </summary>
        /// <param name="parameters">Paramètres du Widget</param>
        public Selector(params object[] parameters) : base(parameters)
        {
            font = "";
            texts = new List<string>();
            selected = 0;

            if (parameters.Length >= 2 && parameters[1] is string fnt)
                font = fnt;
            if (parameters.Length >= 3)
            {
                foreach (string obj in parameters[2..])
                    texts.Add(obj);
            }
        }

        public string GetValue()
        {
            return texts[selected];
        }

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

            LeftButton.scene = scene;
            RightButton.scene = scene;
            text.scene = scene;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            LeftButton.Update(gameTime);
            RightButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (scene == null)
                return;

            LeftButton.Draw(gameTime);
            RightButton.Draw(gameTime);
            text.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}

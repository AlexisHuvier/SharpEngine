using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Widgets;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {
            AddWidget<Label>(new Vec2(100), "SALUT LES BROS !", "arial", Color.WHITE);
            Button b = AddWidget<Button>(new Vec2(300), "Print", "arial", new Vec2(200, 40), Color.BLACK, Color.GRAY);
            b.command = PrintNada;
            AddWidget<Image>(new Vec2(300, 400), "test");
            AddWidget<Checkbox>(new Vec2(500));
            AddWidget<Checkbox>(new Vec2(500, 300), "Heyo", "arial", 2f, Color.WHITE, true);
            AddWidget<Checkbox>(new Vec2(500, 400), "Heyo", "arial", 1f, Color.WHITE, true);
            AddWidget<LineEdit>(new Vec2(600, 100), "", "arial");
        }

        public void PrintNada(Button _)
        {
            System.Console.WriteLine("Nada");
        }
    }
}

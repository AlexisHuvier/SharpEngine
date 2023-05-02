using SharpEngine.Utils.Math;
using SharpEngine.Widgets;

namespace Manamon.Scene;

public class MainMenu: SharpEngine.Scene
{
    public MainMenu()
    {
        AddWidget(new Label(new Vec2(600, 200), "Manamon", "title"));
        AddWidget(new Button(new Vec2(600, 400), "Jouer", "basic", new Vec2(200, 50))).Command =
            button => button.GetWindow().IndexCurrentScene = 1;
        AddWidget(new Button(new Vec2(600, 500), "Options", "basic", new Vec2(200, 50)));
        AddWidget(new Button(new Vec2(600, 600), "Quitter", "basic", new Vec2(200, 50))).Command =
            button => button.GetWindow().Stop();
    }
}
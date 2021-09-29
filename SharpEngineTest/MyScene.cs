using SharpEngine;
using SharpEngine.Components;
using SharpEngine.Widgets;

namespace SharpEngineTest
{
    class MyScene: Scene
    {
        public MyScene(): base()
        {

            var ent = new Entity();
            ent.AddComponent<TransformComponent>(new Vec2(300, 100), new Vec2(2, 2));
            ent.AddComponent<SpriteComponent>("test");
            ent.AddComponent<ControlComponent>(ControlType.CLASSICJUMP);
            ent.AddComponent<RectCollisionComponent>(new Vec2(90));
            ent.AddComponent<PhysicsComponent>();
            AddEntity(ent);

            var ent2 = new Entity();
            ent2.AddComponent<TransformComponent>(new Vec2(300), new Vec2(1, 1));
            ent2.AddComponent<TextComponent>("Testing", "arial", Color.WHITE);
            ent2.AddComponent<RectCollisionComponent>(new Vec2(20));
            AddEntity(ent2);

            AddWidget<Label>(new Vec2(100), "SALUT LES BROS !", "arial", Color.WHITE);
        }
    }
}

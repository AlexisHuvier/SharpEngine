using SharpEngine;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils;
using SharpEngine.Utils.Input;
using SharpEngine.Widget;
using tainicom.Aether.Physics2D.Dynamics;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public MyScene()
    {
        var e1 = new Entity();
        e1.AddComponent(new TransformComponent(new Vec2(100)));
        e1.AddComponent(new RectComponent(Color.Blue, new Vec2(50)));
        e1.AddComponent(new PhysicsComponent(ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(new Vec2(50));
        e1.AddComponent(new ControlComponent());
        AddEntity(e1);
        
        var e2 = new Entity();
        e2.AddComponent(new TransformComponent(new Vec2(200)));
        e2.AddComponent(new RectComponent(Color.Red, new Vec2(50)));
        e2.AddComponent(new PhysicsComponent(BodyType.Static, ignoreGravity: true, fixedRotation: true)).AddRectangleCollision(new Vec2(50));
        AddEntity(e2);

        AddWidget(new Label(new Vec2(400),
                "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHI\nJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmn\nopqrstuvwxyz{|}~¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓ\nÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷\nøùúûüýþÿ",
                "basic"))
            .AddChild(new Label(new Vec2(0, -300), "&é-è_àçù", "basic"))
            .AddChild(new LineInput(new Vec2(0, 100), "Test", "basic"));
    }
}

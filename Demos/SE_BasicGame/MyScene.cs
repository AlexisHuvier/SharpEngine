using SE_BasicWindow.Entity;
using SharpEngine;

namespace SE_BasicWindow;

internal class MyScene : Scene
{
    public readonly Player Player;
    
    public MyScene()
    {
        Player = AddEntity(new Player());
    }
}

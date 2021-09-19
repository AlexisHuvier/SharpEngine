using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Components
{
    public class ControlComponent: Component
    {
        public ControlType controlType;
        public int speed;

        public ControlComponent(params object[] parameters): base(parameters)
        {
            controlType = ControlType.MOUSEFOLLOW;
            speed = 5;

            if (parameters.Length >= 1 && parameters[0] is ControlType control)
                controlType = control;
            if (parameters.Length >= 2 && parameters[0] is int spd)
                speed = spd;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                Vec2 pos = new Vec2(tc.position.x, tc.position.y);

                switch(controlType)
                {
                    case ControlType.MOUSEFOLLOW:
                        var mp = InputManager.GetMousePosition();
                        if (pos.x < mp.x - speed / 2)
                            pos.x += speed;
                        else if (pos.x > mp.x + speed / 2)
                            pos.x -= speed;

                        if (pos.y < mp.y - speed / 2)
                            pos.y += speed;
                        else if (pos.y > mp.y + speed / 2)
                            pos.y -= speed;
                        break;
                }

                if (entity.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc)
                {
                    if (rcc.can_go(pos, "ControlComponent"))
                        tc.position = pos;
                    else
                    {
                        Console.WriteLine($"CANNOT PASS : {pos} {tc.position}");
                    }
                }
                else
                    tc.position = pos;
            }
        }

        public override string ToString()
        {
            return $"ControlComponent(controlType={controlType}, speed={speed})";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpEngine.Components
{
    public class ControlComponent: Component
    {
        public ControlType controlType;
        public int speed;
        public int jumpForce;
        private Dictionary<ControlKey, Inputs.Key> keys;

        public ControlComponent(params object[] parameters): base(parameters)
        {
            controlType = ControlType.MOUSEFOLLOW;
            speed = 5;
            jumpForce = 5;
            keys = new Dictionary<ControlKey, Inputs.Key>()
            {
                { ControlKey.UP, Inputs.Key.UP },
                { ControlKey.DOWN, Inputs.Key.DOWN },
                { ControlKey.LEFT, Inputs.Key.LEFT },
                { ControlKey.RIGHT, Inputs.Key.RIGHT }
            };

            if (parameters.Length >= 1 && parameters[0] is ControlType control)
                controlType = control;
            if (parameters.Length >= 2 && parameters[1] is int spd)
                speed = spd;
            if (parameters.Length >= 3 && parameters[2] is int jF)
                jumpForce = jF;
        }

        public Inputs.Key GetKey(ControlKey controlKey)
        {
            return keys[controlKey];
        }

        public void SetKey(ControlKey controlKey, Inputs.Key key)
        {
            keys[controlKey] = key;
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
                    case ControlType.LEFTRIGHT:
                        if (InputManager.IsKeyDown(keys[ControlKey.LEFT]))
                            pos.x -= speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.RIGHT]))
                            pos.x += speed;
                        break;
                    case ControlType.UPDOWN:
                        if (InputManager.IsKeyDown(keys[ControlKey.UP]))
                            pos.y -= speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.DOWN]))
                            pos.y += speed;
                        break;
                    case ControlType.FOURDIRECTION:
                        if (InputManager.IsKeyDown(keys[ControlKey.LEFT]))
                            pos.x -= speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.RIGHT]))
                            pos.x += speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.UP]))
                            pos.y -= speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.DOWN]))
                            pos.y += speed;
                        break;
                    case ControlType.CLASSICJUMP:
                        if (InputManager.IsKeyDown(keys[ControlKey.LEFT]))
                            pos.x -= speed;
                        if (InputManager.IsKeyDown(keys[ControlKey.RIGHT]))
                            pos.x += speed;
                        if (InputManager.IsKeyPressed(keys[ControlKey.UP]))
                        {
                            if (entity.GetComponent<PhysicsComponent>() is PhysicsComponent pc && pc.grounded)
                            {
                                pc.grounded = false;
                                pc.gravity = -jumpForce;
                            }
                        }
                        break;
                }

                if (entity.GetComponent<RectCollisionComponent>() is RectCollisionComponent rcc)
                {
                    if (rcc.CanGo(pos, "ControlComponent"))
                        tc.position = pos;
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

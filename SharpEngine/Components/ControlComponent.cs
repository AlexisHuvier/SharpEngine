using System.Collections.Generic;

namespace SharpEngine.Components
{
    /// <summary>
    /// Composant de contrôle basique
    /// </summary>
    public class ControlComponent: Component
    {
        public ControlType controlType;
        public int speed;
        public int jumpForce;
        private Dictionary<ControlKey, Inputs.Key> keys;
        public bool isMoving;

        /// <summary>
        /// Initialise le Composant.
        /// </summary>
        /// <param name="controlType">Type de controle</param>
        /// <param name="speed">Vitesse du mouvement</param>
        /// <param name="jumpForce">Force du saut</param>
        public ControlComponent(ControlType controlType = ControlType.MOUSEFOLLOW, int speed = 5, int jumpForce = 5): base()
        {
            this.controlType = controlType;
            this.speed = speed;
            this.jumpForce = jumpForce;
            isMoving = false;
            keys = new Dictionary<ControlKey, Inputs.Key>()
            {
                { ControlKey.UP, Inputs.Key.UP },
                { ControlKey.DOWN, Inputs.Key.DOWN },
                { ControlKey.LEFT, Inputs.Key.LEFT },
                { ControlKey.RIGHT, Inputs.Key.RIGHT }
            };
        }

        public Inputs.Key GetKey(ControlKey controlKey) => keys[controlKey];
        public void SetKey(ControlKey controlKey, Inputs.Key key) => keys[controlKey] = key;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            isMoving = false;

            if (entity.GetComponent<TransformComponent>() is TransformComponent tc)
            {
                Vec2 pos = new Vec2(tc.position.x, tc.position.y);

                switch (controlType)
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
                            throw new System.NotImplementedException();
                        }
                        break;
                }

                if (tc.position != pos)
                {
                    isMoving = true;
                    if (entity.GetComponent<PhysicsComponent>() is PhysicsComponent pc)
                        pc.SetPosition(new Vec2(pos.x, pos.y));
                    else
                        tc.position = pos;
                }
            }
        }

        public override string ToString() => $"ControlComponent(controlType={controlType}, speed={speed})";
    }
}

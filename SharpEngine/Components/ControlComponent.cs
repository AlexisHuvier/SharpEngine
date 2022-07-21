using System;
using System.Collections.Generic;
using SharpEngine.Managers;
using SharpEngine.Utils;

namespace SharpEngine.Components;

/// <summary>
/// Composant de contrôle basique
/// </summary>
public class ControlComponent: Component
{
    public ControlType ControlType;
    public int Speed;
    public int JumpForce;
    private Dictionary<ControlKey, Key> _keys;
    public bool IsMoving;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="controlType">Type de controle</param>
    /// <param name="speed">Vitesse du mouvement</param>
    /// <param name="jumpForce">Force du saut</param>
    public ControlComponent(ControlType controlType = ControlType.MouseFollow, int speed = 5, int jumpForce = 5)
    {
        ControlType = controlType;
        Speed = speed;
        JumpForce = jumpForce;
        IsMoving = false;
        _keys = new Dictionary<ControlKey, Key>()
        {
            { ControlKey.Up, Key.Up },
            { ControlKey.Down, Key.Down },
            { ControlKey.Left, Key.Left },
            { ControlKey.Right, Key.Right }
        };
    }

    public Key GetKey(ControlKey controlKey) => _keys[controlKey];
    public void SetKey(ControlKey controlKey, Key key) => _keys[controlKey] = key;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        IsMoving = false;

        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        var pos = new Vec2(tc.Position.X, tc.Position.Y);

        switch (ControlType)
        {
            case ControlType.MouseFollow:
                var mp = InputManager.GetMousePosition();
                if (pos.X < mp.X - Speed / 2f)
                    pos.X += Speed;
                else if (pos.X > mp.X + Speed / 2f)
                    pos.X -= Speed;

                if (pos.Y < mp.Y - Speed / 2f)
                    pos.Y += Speed;
                else if (pos.Y > mp.Y + Speed / 2f)
                    pos.Y -= Speed;
                break;
            case ControlType.LeftRight:
                if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                    pos.X -= Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                    pos.X += Speed;
                break;
            case ControlType.UpDown:
                if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                    pos.Y -= Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                    pos.Y += Speed;
                break;
            case ControlType.FourDirection:
                if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                    pos.X -= Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                    pos.X += Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                    pos.Y -= Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                    pos.Y += Speed;
                break;
            case ControlType.ClassicJump:
                if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                    pos.X -= Speed;
                if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                    pos.X += Speed;
                if (InputManager.IsKeyPressed(_keys[ControlKey.Up]))
                {
                    throw new NotImplementedException();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(ControlType), ControlType, null);
        }

        if (tc.Position == pos) return;
            
        IsMoving = true;
        if (Entity.GetComponent<PhysicsComponent>() is { } pc)
            pc.SetPosition(new Vec2(pos.X, pos.Y));
        else
            tc.Position = pos;
    }

    public override string ToString() => $"ControlComponent(controlType={ControlType}, speed={Speed})";
}

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
    public bool UseGamePad;
    public GamePadIndex GamePadIndex;
    public bool IsMoving;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="controlType">Type de controle</param>
    /// <param name="speed">Vitesse du mouvement</param>
    /// <param name="jumpForce">Force du saut</param>
    /// <param name="useGamePad">Utiliser la manette</param>
    /// <param name="gamePadIndex">Index de la manette utilisée</param>
    public ControlComponent(ControlType controlType = ControlType.MouseFollow, int speed = 5, int jumpForce = 5, bool useGamePad = true, GamePadIndex gamePadIndex = GamePadIndex.One)
    {
        ControlType = controlType;
        Speed = speed;
        JumpForce = jumpForce;
        IsMoving = false;
        UseGamePad = useGamePad;
        GamePadIndex = gamePadIndex;
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
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed;
                }
                break;
            case ControlType.UpDown:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0)
                    pos.Y -= Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        pos.Y -= Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        pos.Y += Speed;
                }

                break;
            case ControlType.FourDirection:
                if (UseGamePad && (InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0 ||
                                   InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0))
                {
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                    pos.Y -= Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY);
                }
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        pos.Y -= Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        pos.Y += Speed;
                }

                break;
            case ControlType.ClassicJump:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed;
                }

                if (InputManager.IsKeyPressed(_keys[ControlKey.Up]) ||
                    (UseGamePad && InputManager.IsGamePadButtonPressed(GamePadIndex, GamePadButton.A)))
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

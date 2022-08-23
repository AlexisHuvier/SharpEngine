using System;
using System.Collections.Generic;
using SharpEngine.Managers;
using SharpEngine.Utils.Control;
using SharpEngine.Utils.Math;

namespace SharpEngine.Components;

/// <summary>
/// Composant de contrôle basique
/// </summary>
public class ControlComponent: Component
{
    public ControlType ControlType;
    public int Speed;
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
    public ControlComponent(ControlType controlType = ControlType.MouseFollow, int speed = 300, bool useGamePad = true, GamePadIndex gamePadIndex = GamePadIndex.One)
    {
        ControlType = controlType;
        Speed = speed;
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
        if (Entity.GetComponent<PhysicsComponent>() is { } pctemp)
            pctemp.SetLinearVelocity(new Vec2(0));

        if (Entity.GetComponent<TransformComponent>() is not { } tc) return;
        
        var pos = new Vec2(tc.Position.X, tc.Position.Y);

        switch (ControlType)
        {
            case ControlType.MouseFollow:
                var mp = InputManager.GetMousePosition();
                if (pos.X < mp.X - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    pos.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else if (pos.X > mp.X + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    pos.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (pos.Y < mp.Y - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    pos.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else if (pos.Y > mp.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    pos.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                break;
            case ControlType.LeftRight:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                break;
            case ControlType.UpDown:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0)
                    pos.Y -= Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        pos.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        pos.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                break;
            case ControlType.FourDirection:
                if (UseGamePad && (InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0 ||
                                   InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0))
                {
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    pos.Y -= Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        pos.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        pos.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }

                break;
            case ControlType.ClassicJump:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    pos.X += Speed * InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        pos.X -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        pos.X += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
            pc.SetLinearVelocity((pos - tc.Position) / (float)gameTime.ElapsedGameTime.TotalSeconds);
        else
            tc.Position = pos;
    }

    public override string ToString() => $"ControlComponent(controlType={ControlType}, speed={Speed})";
}

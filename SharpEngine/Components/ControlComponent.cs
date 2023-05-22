using System;
using System.Collections.Generic;
using SharpEngine.Managers;
using SharpEngine.Utils;
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
    public bool UseGamePad;
    public GamePadIndex GamePadIndex;
    public bool IsMoving { get; private set; }
    public Vec2 Direction { get; private set; }
    
    private readonly Dictionary<ControlKey, Key> _keys;
    private TransformComponent _transformComponent;
    private PhysicsComponent _physicsComponent;

    /// <summary>
    /// Initialise le Composant.
    /// </summary>
    /// <param name="controlType">Type de controle</param>
    /// <param name="speed">Vitesse du mouvement</param>
    /// <param name="useGamePad">Utiliser la manette</param>
    /// <param name="gamePadIndex">Index de la manette utilisée</param>
    public ControlComponent(ControlType controlType = ControlType.MouseFollow, int speed = 300, bool useGamePad = true, GamePadIndex gamePadIndex = GamePadIndex.One)
    {
        ControlType = controlType;
        Speed = speed;
        IsMoving = false;
        UseGamePad = useGamePad;
        GamePadIndex = gamePadIndex;
        _keys = new Dictionary<ControlKey, Key>
        {
            { ControlKey.Up, Key.Up },
            { ControlKey.Down, Key.Down },
            { ControlKey.Left, Key.Left },
            { ControlKey.Right, Key.Right }
        };
    }

    public override void Initialize()
    {
        base.Initialize();

        _transformComponent = Entity.GetComponent<TransformComponent>();
        _physicsComponent = Entity.GetComponent<PhysicsComponent>();
    }

    public Key GetKey(ControlKey controlKey) => _keys[controlKey];
    public void SetKey(ControlKey controlKey, Key key) => _keys[controlKey] = key;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        IsMoving = false;
        _physicsComponent?.SetLinearVelocity(Vec2.Zero);

        if (_transformComponent == null) return;

        var posX = _transformComponent.Position.X;
        var posY = _transformComponent.Position.Y;

        switch (ControlType)
        {
            case ControlType.MouseFollow:
                var mp = InputManager.GetMousePosition();
                if (posX < mp.X - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    posX += 1;
                else if (posX > mp.X + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    posX -= 1;

                if (posY < mp.Y - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    posY += 1;
                else if (posY > mp.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds / 2f)
                    posY -= 1;
                break;
            case ControlType.LeftRight:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    posX += InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX -= 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX += 1;
                }
                break;
            case ControlType.UpDown:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0)
                    posY -= InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        posY -= 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        posY += 1;
                }

                break;
            case ControlType.FourDirection:
                if (UseGamePad && (InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0 ||
                                   InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY) != 0))
                {
                    posX += InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                    posY -= InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftY);
                }
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX -= 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX += 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        posY -= 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        posY += 1;
                }

                break;
            case ControlType.ClassicJump:
                if (UseGamePad && InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX) != 0)
                    posX += InputManager.GetGamePadJoyStickAxis(GamePadIndex, GamePadJoyStickAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX -= 1;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX += 1;
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

        if (Math.Abs(_transformComponent.Position.X - posX) < InternalUtils.FloatTolerance &&
            Math.Abs(_transformComponent.Position.Y - posY) < InternalUtils.FloatTolerance) return;
            
        IsMoving = true;
        Direction = new Vec2(posX - _transformComponent.Position.X, posY - _transformComponent.Position.Y).Normalized;
        if (_physicsComponent != null)
            _physicsComponent.SetLinearVelocity(Direction * Speed);
        else
            _transformComponent.Position += new Vec2(
                Direction.X * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                Direction.Y * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds
            );
    }

    public override string ToString() => $"ControlComponent(controlType={ControlType}, speed={Speed})";
}

using System;
using System.Collections.Generic;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.Input;

namespace SharpEngine.Component;

/// <summary>
/// Component which represents Controls
/// </summary>
public class ControlComponent: Component
{
    /// <summary>
    /// Type of Control
    /// </summary>
    public ControlType ControlType { get; set; }
    
    /// <summary>
    /// Speed of Control
    /// </summary>
    public int Speed { get; set; }
    
    /// <summary>
    /// If Control use GamePad
    /// </summary>
    public bool UseGamePad { get; set; }
    
    /// <summary>
    /// Index of GamePad
    /// </summary>
    public int GamePadIndex { get; set; }
    
    /// <summary>
    /// Jump force
    /// </summary>
    public float JumpForce { get; set; }
    
    /// <summary>
    /// If Entity is moving
    /// </summary>
    public bool IsMoving { get; private set; }
    
    /// <summary>
    /// If Entity can jump
    /// </summary>
    public bool CanJump { get; private set; }
    
    /// <summary>
    /// Direction of Control
    /// </summary>
    public Vec2 Direction { get; private set; }

    private readonly Dictionary<ControlKey, Key> _keys;
    private TransformComponent? _transform;
    private PhysicsComponent? _physics;

    /// <summary>
    /// Create Control Component
    /// </summary>
    /// <param name="controlType">Control Type (FourDirection)</param>
    /// <param name="speed">Speed (300)</param>
    /// <param name="useGamePad">Use Game Pad (false)</param>
    /// <param name="gamePadIndex">Game Pad Index (1)</param>
    public ControlComponent(ControlType controlType = ControlType.FourDirection, int speed = 300,
        bool useGamePad = false, int gamePadIndex = 1)
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

    /// <summary>
    /// Get Key for a Control
    /// </summary>
    /// <param name="controlKey">Key Control</param>
    /// <returns>Key</returns>
    public Key GetKey(ControlKey controlKey) => _keys[controlKey];
    
    /// <summary>
    /// Set Key for a Control
    /// </summary>
    /// <param name="controlKey">Key Control</param>
    /// <param name="key">Key</param>
    public void SetKey(ControlKey controlKey, Key key) => _keys[controlKey] = key;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        _transform = Entity?.GetComponentAs<TransformComponent>();
        _physics = Entity?.GetComponentAs<PhysicsComponent>();
    }

    /// <inheritdoc />
    public override void Update(float delta)
    {
        base.Update(delta);

        IsMoving = false;
        _physics?.SetLinearVelocity(Vec2.Zero);
        
        if(_transform == null) return;

        var posX = 0f;
        var posY = 0f;

        switch (ControlType)
        {
            case ControlType.MouseFollow:
                var mp = InputManager.GetMousePosition();
                if (posX < mp.X - Speed * delta)
                    posX++;
                else if (posX > mp.X + Speed * delta)
                    posX--;

                if (posY < mp.Y - Speed * delta)
                    posY++;
                else
                    posY--;
                break;
            case ControlType.LeftRight:
                if (UseGamePad && InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX) != 0)
                    posX += InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX--;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX++;
                }

                break;
            case ControlType.UpDown:
                if (UseGamePad && InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY) != 0)
                    posY += InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        posY--;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        posY++;
                }

                break;
            case ControlType.FourDirection:
                if (UseGamePad && InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX) != 0 ||
                    InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY) != 0)
                {
                    posX += InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX);
                    posY += InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftY);
                }
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX--;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX++;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Up]))
                        posY--;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Down]))
                        posY++;
                }
                break;
            case ControlType.ClassicJump:
                if (UseGamePad && InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX) != 0)
                    posX += InputManager.GetGamePadAxis(GamePadIndex, GamePadAxis.LeftX);
                else
                {
                    if (InputManager.IsKeyDown(_keys[ControlKey.Left]))
                        posX--;
                    if (InputManager.IsKeyDown(_keys[ControlKey.Right]))
                        posX++;
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
        
        if (posX == 0 && posY == 0) return;

        IsMoving = true;
        Direction = new Vec2(posX, posY).Normalized();
        if (_physics != null)
            _physics.SetLinearVelocity(Direction * Speed);
        else
            _transform.Position += new Vec2(Direction.X * Speed * delta, Direction.Y * Speed * delta);
    }
}
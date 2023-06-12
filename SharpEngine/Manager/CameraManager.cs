using System;
using System.Numerics;
using Raylib_cs;
using SharpEngine.Component;
using SharpEngine.Math;
using CameraMode = SharpEngine.Utils.CameraMode;

namespace SharpEngine.Manager;

/// <summary>
/// Class which manager Camera information
/// </summary>
public class CameraManager
{
    /// <summary>
    /// Entity followed on mode Follow and FollowSmooth
    /// <seealso cref="Mode"/>
    /// </summary>
    public Entity.Entity? FollowEntity;
    
    /// <summary>
    /// Camera Mode
    /// </summary>
    public CameraMode Mode;

    /// <summary>
    /// Minimum Speed used when mode is FollowSmooth
    /// </summary>
    public float MinSpeed = 30;

    /// <summary>
    /// Minimum Effect Length used when mode is FollowSmooth
    /// </summary>
    public float MinEffectLength = 10;

    /// <summary>
    /// Fraction Speed used when mode is FollowSmooth
    /// </summary>
    public float FractionSpeed = 0.8f;
    
    internal Camera2D Camera2D;

    /// <summary>
    /// Rotation of Camera
    /// </summary>
    public float Rotation
    {
        get => Camera2D.rotation;
        set => Camera2D.rotation = value;
    }
    
    /// <summary>
    /// Zoom of Camera
    /// </summary>
    public float Zoom
    {
        get => Camera2D.zoom;
        set => Camera2D.zoom = value;
    }

    /// <summary>
    /// Create Camera Manager
    /// </summary>
    public CameraManager()
    {
        Camera2D = new Camera2D();
        Mode = CameraMode.Basic;
    }

    internal void SetScreenSize(Vec2 screenSize)
    {
        Camera2D.offset = screenSize / 2;
    }

    /// <summary>
    /// Update Camera Manager
    /// </summary>
    /// <param name="delta">Delta frame</param>
    public void Update(float delta)
    {
        switch (Mode)
        {
            case CameraMode.Follow:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transform)
                    Camera2D.target = transform.Position;
                break;
            case CameraMode.FollowSmooth:
                if (FollowEntity?.GetComponentAs<TransformComponent>() is { } transformSmooth)
                {
                    var diff = transformSmooth.Position - (Vec2)Camera2D.target;
                    var length = diff.Length;
                    if (length > MinEffectLength)
                    {
                        var speed = MathF.Max(FractionSpeed * length, MinSpeed);
                        Camera2D.target += new Vector2(
                            diff.X * (speed * delta / length),
                            diff.Y * (speed * delta / length)
                        );
                    }
                }
                break;
        }
    }
}
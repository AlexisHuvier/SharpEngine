﻿using System;

namespace SharpEngine.Math;

/// <summary>
/// Struct which represents Rectangle
/// </summary>
public struct Rect
{
    /// <summary>
    /// X Position
    /// </summary>
    public float X;
    
    /// <summary>
    /// Y Position
    /// </summary>
    public float Y;
    
    /// <summary>
    /// Width Size
    /// </summary>
    public float Width;
    
    /// <summary>
    /// Height Size
    /// </summary>
    public float Height;


    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="size">Size</param>
    public Rect(Vec2 position, Vec2 size): this(position.X, position.Y, size.X, size.Y) { }
    
    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <param name="size">Size</param>
    public Rect(float x, float y, Vec2 size): this(x, y, size.X, size.Y) { }

    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="position">Position</param>
    /// <param name="width">Width Size</param>
    /// <param name="height">Height Size</param>
    public Rect(Vec2 position, float width, float height): this(position.X, position.Y, width, height) { }

    /// <summary>
    /// Rectangle
    /// </summary>
    /// <param name="x">X Position</param>
    /// <param name="y">Y Position</param>
    /// <param name="width">Width Size</param>
    /// <param name="height">Height Size</param>
    public Rect(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <inheritdoc />
    public override bool Equals(object? other)
    {
        if (other is Rect rect)
            return this == rect;
        return other != null && other.Equals(this);
    }

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(X, Y, Width, Height);

    /// <inheritdoc />
    public override string ToString() => $"Rect(X={X}, Y={Y}, Width={Width}, Height={Height})";
    
    /// <summary>
    /// Operator inequality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is not equals to second</returns>
    public static bool operator !=(Rect r1, Rect r2) => !(r1 == r2);
    
    /// <summary>
    /// Operator equality
    /// </summary>
    /// <param name="r1">First Rect</param>
    /// <param name="r2">Second Rect</param>
    /// <returns>If first is equals to second</returns>
    public static bool operator ==(Rect r1, Rect r2) => System.Math.Abs(r1.X - r2.X) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Y - r2.Y) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Width - r2.Width) < Internal.FloatTolerance &&
                                                        System.Math.Abs(r1.Height - r2.Height) < Internal.FloatTolerance;
    /// <summary>
    /// Convert RectI to Rect
    /// </summary>
    /// <param name="rectI">RectI</param>
    /// <returns>Rect</returns>
    public static implicit operator Rect(RectI rectI) => new(rectI.X, rectI.Y, rectI.Width, rectI.Height);
}
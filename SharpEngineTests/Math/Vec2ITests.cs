﻿using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;


[TestClass]
public class Vec2ITests
{
    [TestMethod]
    public void Constructors()
    {
        var vec = new Vec2I(2);
        var vec2 = new Vec2I(2, 2);
        var vec3 = new Vec2I();

        Assert.AreEqual(0, vec3.X);
        Assert.AreEqual(0, vec3.Y);
        Assert.AreEqual(2, vec.X);
        Assert.AreEqual(2, vec.Y);
        Assert.AreEqual(2, vec2.X);
        Assert.AreEqual(2, vec2.Y);
        Assert.AreEqual(vec2, vec);
    }

    [TestMethod]
    public void StaticProperties()
    {
        var vec = Vec2I.Zero;
        var vec2 = Vec2I.One;

        Assert.AreEqual(0, vec.X);
        Assert.AreEqual(0, vec.Y);
        Assert.AreEqual(1, vec2.X);
        Assert.AreEqual(1, vec2.Y);
    }

    [TestMethod]
    public void Length_LengthSquared()
    {
        var vec = new Vec2I(3, 4);

        Assert.AreEqual(5, vec.Length());
        Assert.AreEqual(25, vec.LengthSquared());
    }

    [TestMethod]
    public void Normalized()
    {
        var vec = new Vec2I(5, 0);
        var vec2 = new Vec2I(0, 5);
        var vec3 = new Vec2I(3, 4);

        Assert.AreEqual(new Vec2(1, 0), vec.Normalized());
        Assert.AreEqual(new Vec2(0, 1), vec2.Normalized());
        Assert.AreEqual(new Vec2(.6f, .8f), vec3.Normalized());
    }

    [TestMethod]
    public void DistanceTo()
    {
        var vec = new Vec2I(8, 5);
        var vec2 = new Vec2I(20, 10);
        
        Assert.AreEqual(0, vec.DistanceTo(vec));
        Assert.AreEqual(13, vec.DistanceTo(vec2));
    }

    [TestMethod]
    public void Convert()
    {
        Assert.IsInstanceOfType<Vec2>((Vec2)new Vec2I(0));
        Assert.IsInstanceOfType<Vector2>((Vector2)new Vec2I(0));
        Assert.IsInstanceOfType<Vec2I>((Vec2I)new Vector2(0));
    }

    [TestMethod]
    public void ArithmeticOperations()
    {
        var vec = new Vec2I(2, 3);
        Assert.AreEqual(new Vec2I(2, 3), vec);
        vec += new Vec2I(2, 1);
        Assert.AreEqual(new Vec2I(4, 4), vec);
        vec += 8;
        Assert.AreEqual(new Vec2I(12, 12), vec);
        vec /= new Vec2I(3, 6);
        Assert.AreEqual(new Vec2I(4, 2), vec);
        vec /= 2;
        Assert.AreEqual(new Vec2I(2, 1), vec);
        vec *= new Vec2I(4, 3);
        Assert.AreEqual(new Vec2I(8, 3), vec);
        vec *= 3;
        Assert.AreEqual(new Vec2I(24, 9), vec);
        vec -= new Vec2I(8, 5);
        Assert.AreEqual(new Vec2I(16, 4), vec);
        vec -= 1;
        Assert.AreEqual(new Vec2I(15, 3), vec);
        vec = -vec;
        Assert.AreEqual(new Vec2I(-15, -3), vec);
    }
    
    [TestMethod]
    public void OtherMethods()
    {
        var v = new Vec2I(0, 1);
        var v2 = new Vec2I(0, 1);
        
        Assert.IsFalse(v.Equals(null));
        Assert.IsFalse(v != v2);
        Assert.AreEqual(v.GetHashCode(), v2.GetHashCode());
        Assert.AreEqual(v.ToString(), "Vec2I(X=0, Y=1)");
    }
    
}
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;

[TestClass]
public class Vec2Tests
{
    [TestMethod]
    public void Constructors()
    {
        var vec = new Vec2(2);
        var vec2 = new Vec2(2, 2);

        Assert.AreEqual(2, vec.X);
        Assert.AreEqual(2, vec.Y);
        Assert.AreEqual(2, vec2.X);
        Assert.AreEqual(2, vec2.Y);
        Assert.AreEqual(vec2, vec);
    }

    [TestMethod]
    public void StaticProperties()
    {
        var vec = Vec2.Zero;
        var vec2 = Vec2.One;

        Assert.AreEqual(0, vec.X);
        Assert.AreEqual(0, vec.Y);
        Assert.AreEqual(1, vec2.X);
        Assert.AreEqual(1, vec2.Y);
    }

    [TestMethod]
    public void Length_LengthSquared()
    {
        var vec = new Vec2(3, 4);

        Assert.AreEqual(5, vec.Length());
        Assert.AreEqual(25, vec.LengthSquared());
    }

    [TestMethod]
    public void Normalized()
    {
        var vec = new Vec2(5, 0);
        var vec2 = new Vec2(0, 5);
        var vec3 = new Vec2(3, 4);

        Assert.AreEqual(new Vec2(1, 0), vec.Normalized());
        Assert.AreEqual(new Vec2(0, 1), vec2.Normalized());
        Assert.AreEqual(new Vec2(.6f, .8f), vec3.Normalized());
    }

    [TestMethod]
    public void DistanceTo()
    {
        var vec = new Vec2(8, 5);
        var vec2 = new Vec2(20, 10);
        
        Assert.AreEqual(0, vec.DistanceTo(vec));
        Assert.AreEqual(13, vec.DistanceTo(vec2));
    }

    [TestMethod]
    public void ArithmeticOperations()
    {
        var vec = new Vec2(2, 3);
        Assert.AreEqual(new Vec2(2, 3), vec);
        vec += new Vec2(2, 1);
        Assert.AreEqual(new Vec2(4, 4), vec);
        vec += 2f;
        Assert.AreEqual(new Vec2(6, 6), vec);
        vec /= new Vec2(2, 3);
        Assert.AreEqual(new Vec2(3, 2), vec);
        vec /= 2f;
        Assert.AreEqual(new Vec2(1.5f, 1), vec);
        vec *= new Vec2(4, 3);
        Assert.AreEqual(new Vec2(6, 3), vec);
        vec *= 3f;
        Assert.AreEqual(new Vec2(18, 9), vec);
        vec -= new Vec2(8, 5);
        Assert.AreEqual(new Vec2(10, 4), vec);
        vec -= 1f;
        Assert.AreEqual(new Vec2(9, 3), vec);
        vec = -vec;
        Assert.AreEqual(new Vec2(-9, -3), vec);
    }

    [TestMethod]
    public void Convert()
    {
        Assert.IsInstanceOfType<Vector2>((Vector2)new Vec2(0));
        Assert.IsInstanceOfType<tainicom.Aether.Physics2D.Common.Vector2>((tainicom.Aether.Physics2D.Common.Vector2)new Vec2(0));
        Assert.IsInstanceOfType<Vec2>((Vec2)new Vector2(0));
        Assert.IsInstanceOfType<Vec2>((Vec2)new tainicom.Aether.Physics2D.Common.Vector2(0));
    }
    
    [TestMethod]
    public void OtherMethods()
    {
        var v = new Vec2(0, 1);
        var v2 = new Vec2(0, 1);
        
        Assert.IsFalse(v.Equals(null));
        Assert.IsFalse(v != v2);
        Assert.AreEqual(v.GetHashCode(), v2.GetHashCode());
        Assert.AreEqual(v.ToString(), "Vec2(X=0, Y=1)");
    }
}
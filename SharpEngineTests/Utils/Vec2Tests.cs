using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngineTests.Utils;

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
    public void Length_LengthSquared()
    {
        var vec = new Vec2(3, 4);

        Assert.AreEqual(5, vec.Length);
        Assert.AreEqual(25, vec.LengthSquared);
    }

    [TestMethod]
    public void Normalized()
    {
        var vec = new Vec2(5, 0);
        var vec2 = new Vec2(0, 5);
        var vec3 = new Vec2(3, 4);

        Assert.AreEqual(new Vec2(1, 0), vec.Normalized);
        Assert.AreEqual(new Vec2(0, 1), vec2.Normalized);
        Assert.AreEqual(new Vec2(.6f, .8f), vec3.Normalized);
    }

    [TestMethod]
    public void Convert()
    {
        Assert.IsInstanceOfType((Vector2)new Vec2(0), typeof(Vector2));
    }

    [TestMethod]
    public void Vec2ToString() => Assert.AreEqual($"Vec2(x=1, y=1)", new Vec2(1).ToString());

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
}

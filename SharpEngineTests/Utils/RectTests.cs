using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngineTests.Utils;

[TestClass]
public class RectTests
{
    [TestMethod]
    public void Constructors()
    {
        var r = new Rect(0, 0, 0, 0);
        var r2 = new Rect(new Vec2(0), new Vec2(0));
        var r3 = new Rect(new Vec2(0), 0, 0);
        var r4 = new Rect(0, 0, new Vec2(0));
        Assert.AreEqual(r, r2);
        Assert.AreEqual(r, r3);
        Assert.AreEqual(r, r4);
        Assert.AreEqual(0, r.X);
        Assert.AreEqual(0, r.Y);
        Assert.AreEqual(0, r.Width);
        Assert.AreEqual(0, r.Height);
    }

    [TestMethod]
    public void ToMg() =>
        Assert.IsInstanceOfType((Rectangle)new Rect(0, 0, 0, 0), typeof(Rectangle));
}

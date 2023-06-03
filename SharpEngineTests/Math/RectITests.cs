using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;

[TestClass]
public class RectITests
{
    [TestMethod]
    public void Constructors()
    {
        var r = new RectI(0, 1, 2, 3);
        var r2 = new RectI(new Vec2I(0, 1), new Vec2I(2, 3));
        var r3 = new RectI(new Vec2I(0, 1), 2, 3);
        var r4 = new RectI(0, 1, new Vec2I(2, 3));
        Assert.AreEqual(r, r2);
        Assert.AreEqual(r, r3);
        Assert.AreEqual(r, r4);
        Assert.AreEqual(0, r.X);
        Assert.AreEqual(1, r.Y);
        Assert.AreEqual(2, r.Width);
        Assert.AreEqual(3, r.Height);
    }
    
    [TestMethod]
    public void Convert() => Assert.IsInstanceOfType<Rect>((Rect)new RectI(0, 0, 0, 0));
}
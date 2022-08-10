using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Assert.AreEqual(new Vec2(0), r.Position);
        Assert.AreEqual(new Vec2(0), r.Size);
    }

    [TestMethod]
    public void ToMg() =>
        Assert.IsInstanceOfType(new Rect(0, 0, 0, 0).ToMg(), typeof(Microsoft.Xna.Framework.Rectangle));
}

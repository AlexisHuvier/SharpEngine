using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;

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
    public void Contains()
    {
        var r = new Rect(0, 1, 2, 3);
        
        Assert.IsFalse(r.Contains(new Vec2(-1, -1)));
        Assert.IsTrue(r.Contains(new Vec2(1, 3)));
    }
    
    [TestMethod]
    public void OtherMethods()
    {
        var r = new Rect(0, 1, 2, 3);
        var r2 = new Rect(0, 1, 2, 3);
        
        Assert.IsFalse(r.Equals(null));
        Assert.IsFalse(r != r2);
        Assert.AreEqual(r.GetHashCode(), r2.GetHashCode());
        Assert.AreEqual(r.ToString(), "Rect(X=0, Y=1, Width=2, Height=3)");
    }
}
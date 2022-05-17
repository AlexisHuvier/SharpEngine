using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine;

namespace SharpEngineTests.Utils
{
    [TestClass]
    public class RectTests
    {
        [TestMethod]
        public void Constructors()
        {
            Rect r = new Rect(0, 0, 0, 0);
            Rect r2 = new Rect(new Vec2(0), new Vec2(0));
            Rect r3 = new Rect(new Vec2(0), 0, 0);
            Rect r4 = new Rect(0, 0, new Vec2(0));
            Assert.AreEqual(r, r2);
            Assert.AreEqual(r, r3);
            Assert.AreEqual(r, r4);
            Assert.AreEqual(new Vec2(0), r.position);
            Assert.AreEqual(new Vec2(0), r.size);
        }

        [TestMethod]
        public void ToMG() => Assert.IsInstanceOfType(new Rect(0, 0, 0, 0).ToMG(), typeof(Microsoft.Xna.Framework.Rectangle));
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine;

namespace SharpEngineTests.Utils
{
    [TestClass]
    public class ColorTests
    {
        [TestMethod]
        public void Constructors()
        {
            Color color = new Color(255, 255, 255);
            Color color2 = new Color(255, 255, 255, 255);
            Assert.AreEqual(255, color.r);
            Assert.AreEqual(255, color.g);
            Assert.AreEqual(255, color.b);
            Assert.AreEqual(255, color.a);
            Assert.AreEqual(255, color2.r);
            Assert.AreEqual(255, color2.g);
            Assert.AreEqual(255, color2.b);
            Assert.AreEqual(255, color2.a);
            Assert.AreEqual(color, color2);
        }

        [TestMethod]
        public void ToMG() => Assert.IsInstanceOfType(Color.BLACK.ToMG(), typeof(Microsoft.Xna.Framework.Color));
    }
}

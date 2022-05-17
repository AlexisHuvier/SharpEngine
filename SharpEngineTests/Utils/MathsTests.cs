using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine;

namespace SharpEngineTests.Utils
{
    [TestClass]
    public class MathsTests
    {
        [TestMethod]
        public void Degrees_Radians()
        {
            Assert.AreEqual(180, Math.ToDegrees(Math.PI));
            Assert.AreEqual(Math.TWOPI, Math.ToRadians(360));
            Assert.AreEqual(345, Math.ToDegrees(Math.ToRadians(345)));
        }

        [TestMethod]
        public void Distance()
        {
            Assert.AreEqual(0.8f, System.MathF.Round(Math.Distance(1.5f, 2.3f), 1));
            Assert.AreEqual(2, Math.Distance(2, 4));
        }

        [TestMethod]
        public void Clamp()
        {
            Assert.AreEqual(0, Math.Clamp(-5, 0, 5));
            Assert.AreEqual(2, Math.Clamp(2, 0, 5));
            Assert.AreEqual(5, Math.Clamp(10, 0, 5));
        }
    }
}

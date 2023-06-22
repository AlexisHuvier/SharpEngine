using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;

[TestClass]
public class MathTests
{
    [TestMethod]
    public void Degrees_Radians()
    {
        Assert.AreEqual(180, MathHelper.ToDegrees(MathHelper.Pi));
        Assert.AreEqual(MathHelper.TwoPi, MathHelper.ToRadians(360));
        Assert.AreEqual(345, MathHelper.ToDegrees(MathHelper.ToRadians(345)));
    }

    [TestMethod]
    public void Clamp()
    {
        Assert.AreEqual(0, MathHelper.Clamp(-5, 0, 5));
        Assert.AreEqual(2, MathHelper.Clamp(2, 0, 5));
        Assert.AreEqual(5, MathHelper.Clamp(10, 0, 5));
    }
}
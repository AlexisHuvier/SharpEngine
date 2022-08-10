﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Utils;
using SharpEngine.Utils.Math;

namespace SharpEngineTests.Utils;

[TestClass]
public class MathsTests
{
    [TestMethod]
    public void Degrees_Radians()
    {
        Assert.AreEqual(180, MathUtils.ToDegrees(MathUtils.Pi));
        Assert.AreEqual(MathUtils.TwoPi, MathUtils.ToRadians(360));
        Assert.AreEqual(345, MathUtils.ToDegrees(MathUtils.ToRadians(345)));
    }

    [TestMethod]
    public void Distance()
    {
        Assert.AreEqual(0.8f, System.MathF.Round(MathUtils.Distance(1.5f, 2.3f), 1));
        Assert.AreEqual(2, MathUtils.Distance(2, 4));
    }

    [TestMethod]
    public void Clamp()
    {
        Assert.AreEqual(0, MathUtils.Clamp(-5, 0, 5));
        Assert.AreEqual(2, MathUtils.Clamp(2, 0, 5));
        Assert.AreEqual(5, MathUtils.Clamp(10, 0, 5));
    }
}

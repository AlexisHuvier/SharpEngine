using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Utils;

namespace SharpEngineTests.Utils;

[TestClass]
public class ColorTests
{
    [TestMethod]
    public void Constructors()
    {
        var color = new Color(255, 255, 255);
        var color2 = new Color(255, 255, 255, 255);
        Assert.AreEqual(255, color.R);
        Assert.AreEqual(255, color.G);
        Assert.AreEqual(255, color.B);
        Assert.AreEqual(255, color.A);
        Assert.AreEqual(255, color2.R);
        Assert.AreEqual(255, color2.G);
        Assert.AreEqual(255, color2.B);
        Assert.AreEqual(255, color2.A);
        Assert.AreEqual(color, color2);
    }

    [TestMethod]
    public void ToRayLib() => Assert.IsInstanceOfType<Raylib_cs.Color>((Raylib_cs.Color)Color.Black);

    [TestMethod]
    public void DarkerLighter()
    {
        var color = new Color(100, 100, 100);
        
        Assert.AreEqual(color.Darker(), new Color(90, 90, 90));
        Assert.AreEqual(color.Darker(4), new Color(60, 60, 60));
        Assert.AreEqual(color.Lighter(), new Color(110, 110, 110));
        Assert.AreEqual(color.Lighter(4), new Color(140, 140, 140));
    }
    
    [TestMethod]
    public void TranslateTo()
    {
        var startColor = new Color(200, 200, 200);
        var endColor = new Color(100, 100, 100);
        Assert.AreEqual(new Color(150, 150, 150), startColor.TranslateTo(endColor, .5f, 1f));
        Assert.AreEqual(new Color(125, 125, 125), startColor.TranslateTo(endColor, .75f, 1f));
    }

    [TestMethod]
    public void Equals()
    {
        var color = new Color(255, 255, 255);
        var color2 = new Color(255, 255, 255);
        
        Assert.IsFalse(color.Equals(null));
        Assert.IsFalse(color != color2);
        Assert.AreEqual(color.GetHashCode(), color2.GetHashCode());
        Assert.AreEqual(color.ToString(), "Color(R=255, G=255, B=255, A=255)");
    }
}
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
    public void ToMg() => Assert.IsInstanceOfType((Microsoft.Xna.Framework.Color)Color.Black, typeof(Microsoft.Xna.Framework.Color));

    [TestMethod]
    public void ColorTransition()
    {
        var color = new Color(200, 200, 200);
        var color2 = new Color(100, 100, 100);
        Assert.AreEqual(new Color(150, 150, 150), Color.GetColorBetween(color2, color, 0.5f, 1f));
        Assert.AreEqual(new Color(125, 125, 125), Color.GetColorBetween(color, color2, 0.75f, 1f));
    }
}

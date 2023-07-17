using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Math;

namespace SharpEngineTests.Math;

[TestClass]
public class RandTests
{
    [TestMethod]
    public void GetRand()
    {
        var rand = Rand.GetRand();
        var rand2 = Rand.GetRand(5);
        var rand3 = Rand.GetRand(2, 50);
        
        Assert.IsFalse(rand < 0);
        
        Assert.IsFalse(rand2 < 0);
        Assert.IsFalse(rand2 >= 5);
        
        Assert.IsFalse(rand3 < 2);
        Assert.IsFalse(rand3 >= 50);
    }
    
    [TestMethod]
    public void GetRandF()
    {
        var rand = Rand.GetRandF();
        var rand2 = Rand.GetRandF(5);
        var rand3 = Rand.GetRandF(2, 50);
        
        Assert.IsFalse(rand < 0);
        
        Assert.IsFalse(rand2 < 0);
        Assert.IsFalse(rand2 >= 5);
        
        Assert.IsFalse(rand3 < 2);
        Assert.IsFalse(rand3 >= 50);
    }
}
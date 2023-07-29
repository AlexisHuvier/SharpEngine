using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.File.Save;

namespace SharpEngineTests.File;

[TestClass]
public class SaveTests
{
    [TestMethod]
    public void Binary()
    {
        var save = new BinarySave();
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "TEST");
        save.Read("Resources/Save/save.sesave");
        save.Write("Resources/Save/save.sesave");
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "Oui");
        Assert.AreEqual(save.GetObjectAs("integer", 5), 1);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 1.2);
        Assert.AreEqual(save.GetObject("boolean", false), true);
        Assert.AreEqual(save.GetObject("bla", "TEST"), "TEST");
        save.SetObject("double", 2.0);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 2.0);
    }

    [TestMethod]
    public void Json()
    {
        var save = new JsonSave();
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "TEST");
        save.Read("Resources/Save/save.json");
        save.Write("Resources/Save/save.json");
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "Oui");
        Assert.AreEqual(save.GetObjectAs<long>("integer", 5), 1);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 1.2);
        Assert.AreEqual(save.GetObject("boolean", false), true);
        Assert.AreEqual(save.GetObject("bla", "TEST"), "TEST");
        save.SetObject("double", 2.0);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 2.0);
    }
}
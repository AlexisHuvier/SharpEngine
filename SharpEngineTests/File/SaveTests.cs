using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Utils.File.Save;

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
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "Oui");
        Assert.AreEqual(save.GetObjectAs("integer", 5), 1);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 1.2);
        Assert.AreEqual(save.GetObjectAs("boolean", false), true);
        Assert.AreEqual(save.GetObjectAs("bla", "TEST"), "TEST");
        save.SetObject("double", 2.0);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 2.0);
    }

    [TestMethod]
    public void Json()
    {
        var save = new JsonSave();
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "TEST");
        save.Read("Resources/Save/save.json");
        Assert.AreEqual(save.GetObjectAs("string", "TEST"), "Oui");
        Assert.AreEqual(save.GetObjectAs<long>("integer", 5), 1);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 1.2);
        Assert.AreEqual(save.GetObjectAs("boolean", false), true);
        Assert.AreEqual(save.GetObjectAs("bla", "TEST"), "TEST");
        save.SetObject("double", 2.0);
        Assert.AreEqual(save.GetObjectAs("double", 2.5), 2.0);
    }
}
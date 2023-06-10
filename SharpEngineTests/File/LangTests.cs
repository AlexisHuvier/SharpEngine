using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Utils.File.Lang;

namespace SharpEngineTests.File;

[TestClass]
public class LangTests
{
    [TestMethod]
    public void JsonLang()
    {
        var jsonLang = new JsonLang("Resources/Lang/lang.json");
        Assert.AreEqual(jsonLang.GetTranslation("test", "bla"), "Test de JSONLANG");
        Assert.AreEqual(jsonLang.GetTranslation("test2", "bla"), "bla");
        
        jsonLang.Reload("Resources/Lang/lang2.json");
        Assert.AreEqual(jsonLang.GetTranslation("test", "bla"), "Ceci n'est pas un test");
        Assert.AreEqual(jsonLang.GetTranslation("test2", "bla"), "bla");
    }

    [TestMethod]
    public void McLang()
    {
        var mcLang = new McLang("Resources/Lang/mclang.lang");
        Assert.AreEqual(mcLang.GetTranslation("test", "bla"), "Test de MCLANG");
        Assert.AreEqual(mcLang.GetTranslation("test2", "bla"), "bla");
        
        mcLang.Reload("Resources/Lang/mclang2.lang");
        Assert.AreEqual(mcLang.GetTranslation("test", "bla"), "Ceci n'est pas un test");
        Assert.AreEqual(mcLang.GetTranslation("test2", "bla"), "bla");
    }
}
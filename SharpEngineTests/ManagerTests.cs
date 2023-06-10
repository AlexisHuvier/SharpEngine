using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Component;
using SharpEngine.Entity;
using SharpEngine.Manager;
using SharpEngine.Math;
using SharpEngine.Utils.File.Lang;
using SharpEngine.Utils.File.Save;

namespace SharpEngineTests;

[TestClass]
public class ManagerTests
{
    [TestMethod]
    public void Save()
    {
        Assert.IsFalse(SaveManager.Saves.Any());

        var bSave = new BinarySave();
        var jSave = new JsonSave();
        
        SaveManager.AddSave("binary", bSave);
        SaveManager.AddSave("json", jSave);

        Assert.ThrowsException<Exception>(() => SaveManager.GetSave("test"));
        Assert.AreEqual(SaveManager.GetSave("binary"), bSave);
        Assert.AreEqual(SaveManager.GetSave("json"), jSave);
    }
    
    [TestMethod]
    public void Lang()
    {
        Assert.AreEqual(LangManager.CurrentLang, "default");
        Assert.IsFalse(LangManager.Langs.Any());
        
        LangManager.AddLang("jsonLang", new JsonLang("Resources/Lang/lang.json"));
        LangManager.AddLang("mcLang", new McLang("Resources/Lang/mclang.lang"));

        Assert.AreEqual(LangManager.GetTranslation("test", "bla"), "bla");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "jsonLang"), "Test de JSONLANG");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "mcLang"), "Test de MCLANG");

        LangManager.CurrentLang = "jsonLang";
        
        Assert.AreEqual(LangManager.GetTranslation("test", "bla"), "Test de JSONLANG");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "jsonLang"), "Test de JSONLANG");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "mcLang"), "Test de MCLANG");

        LangManager.CurrentLang = "mcLang";
        
        Assert.AreEqual(LangManager.GetTranslation("test", "bla"), "Test de MCLANG");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "jsonLang"), "Test de JSONLANG");
        Assert.AreEqual(LangManager.GetTranslation("test", "bla", "mcLang"), "Test de MCLANG");
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEngine.Data.DataTable;

namespace SharpEngineTests.Data;

[TestClass]
public class DataTableTests
{
    public class Data
    {
        public string? Name { get; set; }
    }

    [TestMethod]
    public void JsonDataTable()
    {
        var dataTable = new JsonDataTable<Data>("Resources/DataTable/data.json");
        Assert.AreEqual("Data1", dataTable.Get(x => x.Name == "Data1")?.Name);
        Assert.AreEqual("Data2", dataTable.Get(x => x.Name == "Data2")?.Name);
        Assert.AreEqual(null, (Data?)dataTable.Get(x => x.Name == "Data3")?.Name);
    }

    [TestMethod]
    public void SQLiteDataTable()
    {
        var dataTable = new SQLiteDataTable<Data>("Resources/DataTable/data.db");
        Assert.AreEqual("Data1", dataTable.Get(x => x.Name == "Data1")?.Name);
        Assert.AreEqual("Data2", dataTable.Get(x => x.Name == "Data2")?.Name);
        Assert.AreEqual(null, (Data?)dataTable.Get(x => x.Name == "Data3")?.Name);
    }
}
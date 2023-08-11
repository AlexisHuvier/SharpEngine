using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SharpEngine.Data.DataTable;

/// <summary>
/// Sqlite Data Table
/// </summary>
public class SQLiteDataTable<T>: IDataTable where T : new()
{
    private List<dynamic> Objects { get; }
    
    /// <summary>
    /// Create Data Table from SQLite
    /// </summary>
    /// <param name="dbFile">SQLite File</param>
    /// <param name="version">Version</param>
    /// <exception cref="NotImplementedException">If use not implement type</exception>
    public SQLiteDataTable(string dbFile, string version = "3")
    {
        Objects = new List<dynamic>();
        
        var connection = new SQLiteConnection($"Data Source={dbFile};Version={version};New=True;Compress=True;");
        
        connection.Open();
        
        var cmd = connection.CreateCommand();
        cmd.CommandText = $"SELECT * FROM {typeof(T).Name};";
        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var obj = new T();
            
            var index = 0;
            foreach (var property in typeof(T).GetProperties())
            {
                if(reader.IsDBNull(index))
                    property.SetValue(obj, null);
                else if(property.PropertyType == typeof(string))
                    property.SetValue(obj, reader.GetString(index));
                else if(property.PropertyType == typeof(int))
                    property.SetValue(obj, reader.GetInt32(index));
                else if(property.PropertyType == typeof(bool))
                    property.SetValue(obj, reader.GetBoolean(index));
                else if (property.PropertyType == typeof(float))
                    property.SetValue(obj, reader.GetFloat(index));
                else
                    throw new NotImplementedException($"Not implemented type : {property.PropertyType.Name}");
                index++;
            }
            Objects.Add(obj);
        }
        connection.Close();
    }

    /// <inheritdoc />
    public dynamic? Get(Predicate<dynamic> predicate)
    {
        return Objects.Find(predicate);
    }
}
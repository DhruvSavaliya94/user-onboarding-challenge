using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextFileDataStore;

public class TextFileStorage<T> : ITextFileStorage<T> where T : class
{
    private readonly string _filePath;
    private readonly object _lock = new object();

    public TextFileStorage(string filePath)
    {
        _filePath = filePath;
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }

    private List<T> ReadAll()
    {
        lock (_lock)
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }
    }

    private void WriteAll(List<T> data)
    {
        lock (_lock)
        {
            var json = JsonSerializer.Serialize(data);
            File.WriteAllText(_filePath, json);
        }
    }

    public IEnumerable<T> GetAll()
    {
        return ReadAll();
    }

    public T GetById(int id)
    {
        var data = ReadAll();
        var item = data.FirstOrDefault(x => GetId(x) == id);
        return item;
    }

    public void Add(T item)
    {
        var data = ReadAll();
        var newId = data.Count == 0 ? 1 : data.Max(x => GetId(x)) + 1;
        SetId(item, newId);
        data.Add(item);
        WriteAll(data);
    }

    public void Update(T item)
    {
        var data = ReadAll();
        var id = GetId(item);
        var index = data.FindIndex(x => GetId(x) == id);
        if (index >= 0)
        {
            data[index] = item;
            WriteAll(data);
        }
    }

    public void Remove(int id)
    {
        var data = ReadAll();
        data.RemoveAll(x => GetId(x) == id);
        WriteAll(data);
    }

    // Reflection-based ID getter/setter, assume property name "Id"
    private int GetId(T item)
    {
        var prop = typeof(T).GetProperty("Id");
        return (int)prop.GetValue(item);
    }

    private void SetId(T item, int id)
    {
        var prop = typeof(T).GetProperty("Id");
        prop.SetValue(item, id);
    }
}

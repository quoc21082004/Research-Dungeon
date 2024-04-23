using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveLoadHandler
{
    public readonly static string SAVE_PATH = Application.persistentDataPath;
    public static string GET_PATH(string _filename)
    {
        return SAVE_PATH + "/" + _filename;
    }
    public static void SaveToJson<T>(T type, string _filename)
    {
        var saveData = JsonConvert.SerializeObject(type, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        string file_path = GET_PATH(_filename);
        Debug.Log(file_path);
        File.WriteAllText(file_path, saveData);
    }
    public static T LoadFromFile<T>(string _fileName) 
    {
        string _path = GET_PATH(_fileName);
        if (!File.Exists(_path))
            return default;
        var loadData = File.ReadAllText(_path);
        return JsonConvert.DeserializeObject<T>(loadData);
    }

    #region SAve
    /*public static void SaveToJson<T>(List<T> typeData, string _filename)
    {
        string content = JsonHelper.ToJson<T>(typeData.ToArray());
        Debug.Log("save to :" + GetPath(_filename));
        WriteFile(GetPath(_filename), content);
    }
    public static void ReadFromJson()
    {

    }
    public static string GetPath(string _filename)
    {
        return SAVE_PATH + "/" + _filename;
    }
    public static void WriteFile(string _path, string _content)
    {
        FileStream fileStream = new FileStream(_path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(_content);
        }
    }
    public static string ReadFile()
    {
        return "";
    }
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }*/
    #endregion
}

#region tx
/*public readonly static string SAVE_PATH = Application.persistentDataPath;
public static void SaveToFile<T>(string _filename, T typeData)
{
    string _path = Path.Combine(SAVE_PATH, _filename);
    string _json = JsonUtility.ToJson(typeData);
    Debug.Log("save to file :" + _path);
    File.WriteAllText(_path, _json);
}
public static T LoadFromFile<T>(string fileName) where T : class
{
    string path = Path.Combine(SAVE_PATH, fileName);
    if (!File.Exists(path))
    {
        return null;
    }
    try
    {
        string json = File.ReadAllText(path);
        Debug.Log("Load :" + path);
        return JsonUtility.FromJson<T>(json);
    }
    catch
    {
        return null;
    }
}*/
#endregion
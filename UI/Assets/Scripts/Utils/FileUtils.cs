using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtils
{
    const string novelSaveDataFileName = "novelSaveData.json";
    private void WriteAllText(string path, string value)
    {
        File.WriteAllText(path, value);
    }

    private string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path); //受け取ったパスのファイルを読み込む
        string dataStr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();//ファイルを閉じる

        return dataStr;
    }

    public void WriteNovelSaveData(string json)
    {
        string path = Path.Combine(Application.persistentDataPath, novelSaveDataFileName);
        //Debug.Log($"save : path : {path}");
        WriteAllText(path: path, value: json);
    }

    public NovelSaveData ReadNovelSaveData()
    {
        string path = Path.Combine(Application.persistentDataPath, novelSaveDataFileName);
        string json = ReadFile(path);
        return JsonUtility.FromJson<NovelSaveData>(json);//読み込んだJSONファイルをPlayerData型に
    }
}

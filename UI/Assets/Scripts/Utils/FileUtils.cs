using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Const;

namespace Util{
    public static class FileUtils
    {
        public static void WriteAllText(string path, string value)
        {
            File.WriteAllText(path, value);
        }

        public static string ReadFile(string path)
        {
            StreamReader reader = new StreamReader(path); //受け取ったパスのファイルを読み込む
            string dataStr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
            reader.Close();//ファイルを閉じる

            return dataStr;
        }

        public static bool ExistFile(string path)
        {
            return File.Exists(path);
        }

        public static void WriteNovelSaveData(string json, int saveNum)
        {
            string folderPath = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{saveNum}/");
            //Debug.Log($"save : path : {path}");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string path = Path.Combine(folderPath, SaveConst.novelSaveDataFileName);
            WriteAllText(path: path, value: json);
        }

        public static NovelSaveData ReadNovelSaveData(int saveNum)
        {
            string path = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{saveNum}/", SaveConst.novelSaveDataFileName);
            string json = ReadFile(path);
            return JsonUtility.FromJson<NovelSaveData>(json);//読み込んだJSONファイルをPlayerData型に
        }

        public static void WriteNovelRouteData(string json, int saveNum)
        {
            string folderPath = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{saveNum}/");
            //Debug.Log($"save : path : {path}");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string path = Path.Combine(folderPath, SaveConst.novelRouteDataFileName);
            WriteAllText(path: path, value: json);
        }

        public static NovelRouteSaveData ReadNovelRouteData(int saveNum)
        {
            string path = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{saveNum}/", SaveConst.novelRouteDataFileName);
            string json = ReadFile(path);
            //Debug.Log($"ReadNovelRouteData : json : {json}");
            return JsonUtility.FromJson<NovelRouteSaveData>(json);//読み込んだJSONファイルをPlayerData型に
        }
    }
}

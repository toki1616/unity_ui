using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Const;

namespace Util
{
    public static class PlayerDataUtils
    {
        public static void SaveNovelSaveData(NovelSaveData novelSaveData)
        {
            string jsonStr = JsonUtility.ToJson(novelSaveData);
            //Debug.Log($"SaveNovelSaveData : json : {jsonStr}");
            FileUtils.WriteNovelSaveData(jsonStr, novelSaveData.SaveNum);
        }

        public static NovelSaveData LoadNovelSaveData(int saveNum)
        {
            NovelSaveData novelSaveData = FileUtils.ReadNovelSaveData(saveNum);
            return novelSaveData;
        }

        public static List<NovelSaveData> LoadAllNovelSaveData()
        {
            List<NovelSaveData> novelSaveDataList = new List<NovelSaveData>();

            for (int i = SaveConst.startSelectSaveNum; i < SaveConst.saveCount; i++)
            {
                string path = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{i}/", SaveConst.novelSaveDataFileName);
                if (FileUtils.ExistFile(path))
                {
                    NovelSaveData novelSaveData = LoadNovelSaveData(i);
                    novelSaveDataList.Add(novelSaveData);
                }
            }

            return novelSaveDataList;
        }
    }
}

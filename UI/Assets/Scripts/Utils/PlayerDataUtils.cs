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
        //Novel
        //SaveData
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

            for (int i = SaveConst.quickSaveNum; i < SaveConst.saveCount; i++)
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

        //RouteData
        public static void SaveNovelRouteData(NovelRouteSaveData novelRouteDataList)
        {
            string jsonStr = JsonUtility.ToJson(novelRouteDataList);
            Debug.Log($"SaveNovelSaveData : json : {jsonStr}");
            FileUtils.WriteNovelRouteData(jsonStr, novelRouteDataList.SaveNum);
        }

        public static NovelUseRouteData LoadNovelRouteData(int saveNum)
        {
            NovelUseRouteData novelSaveData = FileUtils.ReadNovelRouteData(saveNum);
            return novelSaveData;
        }

        public static List<NovelRouteSaveData> LoadAllNovelRouteData()
        {
            List<NovelRouteSaveData> novelRouteSaveDataList = new List<NovelRouteSaveData>();

            for (int i = SaveConst.quickSaveNum; i < SaveConst.saveCount; i++)
            {
                string path = Path.Combine(Application.persistentDataPath, $"{SaveConst.novelSaveDataFilePath}{i}/", SaveConst.novelSaveDataFileName);
                if (FileUtils.ExistFile(path))
                {
                    NovelUseRouteData novelRouteDataList = LoadNovelRouteData(i);
                    NovelRouteSaveData novelRouteSaveData = new NovelRouteSaveData(saveNum: i, novelUseRouteData: novelRouteDataList);
                    novelRouteSaveDataList.Add(novelRouteSaveData);
                }
            }

            return novelRouteSaveDataList;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataUtils
{
    private readonly FileUtils _fileUtils;

    public PlayerDataUtils(FileUtils fileUtils)
    {
        _fileUtils = fileUtils;
    }

    public void SaveNovelSaveData(NovelMessage novelMessage, int saveNum)
    {
        NovelSaveData novelSaveData = new NovelSaveData(storyNum: novelMessage.GetStoryNum(), saveNum: saveNum);
        string jsonStr = JsonUtility.ToJson(novelSaveData);
        Debug.Log($"SaveNovelSaveData : json : {jsonStr}");
        _fileUtils.WriteNovelSaveData(jsonStr);
    }

    public int LoadNovelSaveData()
    {
        NovelSaveData novelSaveData = _fileUtils.ReadNovelSaveData();
        return novelSaveData.GetStoryNum();
    }
}

[Serializable]
public class NovelSaveData {
    [SerializeField]
    private int saveNum;

    [SerializeField]
    private int storyNum;

    public NovelSaveData(int storyNum, int saveNum)
    {
        this.saveNum = saveNum;
        this.storyNum = storyNum;
    }

    public int GetSaveNum()
    {
        return saveNum;
    }

    public int GetStoryNum()
    {
        return storyNum;
    }
}

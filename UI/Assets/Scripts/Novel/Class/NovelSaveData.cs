using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Util;
[Serializable]
public class NovelSaveDataList
{
    [SerializeField]
    private List<NovelSaveData> novelSaveDataList;

    public NovelSaveDataList()
    {
        novelSaveDataList = PlayerDataUtils.LoadAllNovelSaveData();
    }

    public int GetLoadStoryNum(int saveNum)
    {
        return novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum).StoryNum;
    }

    public void Save(NovelMessage novelMessage, int saveNum)
    {
        novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum).StoryNum = novelMessage.GetStoryNum();
        PlayerDataUtils.SaveNovelSaveData(novelMessage: novelMessage, saveNum: saveNum);
    }
}

[Serializable]
public class NovelSaveData
{
    [SerializeField]
    private int saveNum;
    public int SaveNum
    {
        get
        {
            return saveNum;
        }
        set
        {
            saveNum = value;
        }
    }

    [SerializeField]
    private int storyNum;
    public int StoryNum
    {
        get
        {
            return storyNum;
        }
        set
        {
            storyNum = value;
        }
    }

    public NovelSaveData(int saveNum, int storyNum)
    {
        this.saveNum = saveNum;
        this.storyNum = storyNum;
    }
}

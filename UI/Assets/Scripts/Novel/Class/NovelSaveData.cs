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
        return novelSaveDataList.Find(novelSaveData => novelSaveData.GetSaveNum() == saveNum).GetStoryNum();
    }
}

[Serializable]
public class NovelSaveData
{
    [SerializeField]
    private int saveNum;

    [SerializeField]
    private int storyNum;

    public NovelSaveData(int saveNum, int storyNum)
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
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

    public NovelSaveData GetNovelSaveData(int saveNum)
    {
        var foundData = novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum);
        if (foundData != null)
        {
            return foundData;
        }
        else
        {
            return null;
        }
    }

    public int GetLoadStoryNum(int saveNum)
    {
        var foundData = novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum);
        if (foundData != null)
        {
            return foundData.StoryNum;
        }
        else
        {
            return NovelMessageConst.notFindStoryNum;
        }
    }

    public NovelSaveData SaveAndGetSaveData(NovelMessage novelMessage, int saveNum)
    {
        NovelSaveData newSaveData = new NovelSaveData(saveNum: saveNum, storyNum: novelMessage.GetStoryNum());

        NovelSaveData foundData = novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum);
        if (foundData != null)
        {
            foundData = newSaveData;
        }
        else
        {
            novelSaveDataList.Add(newSaveData);
        }

        PlayerDataUtils.SaveNovelSaveData(newSaveData);
        return newSaveData;
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

    [SerializeField]
    private string saveDate;
    public string SaveDate
    {
        get
        {
            return saveDate;
        }
        set
        {
            saveDate = value;
        }
    }

    public NovelSaveData(int saveNum, int storyNum)
    {
        this.saveNum = saveNum;
        this.storyNum = storyNum;
    }

    public bool isCanLoad()
    {
        return storyNum != NovelMessageConst.notFindStoryNum || this == null;
    }
}

public class NovelSaveDataButtonData
{
    private NovelMessage novelMessage;
    private NovelSaveData novelSaveData;

    public NovelSaveDataButtonData(NovelMessage novelMessage, NovelSaveData novelSaveData)
    {
        this.novelMessage = novelMessage;
        this.novelSaveData = novelSaveData;
    }

    public NovelSaveDataButtonData(NovelSaveData novelSaveData)
    {
        this.novelSaveData = novelSaveData;
    }

    public NovelMessage GetNovelMessage()
    {
        return novelMessage;
    }

    public NovelSaveData GetNovelSaveData()
    {
        return novelSaveData;
    }

    public Sprite GetBackgroundImage()
    {
        return novelMessage.GetBackgroundImage();
    }

    public List<Sprite> GetCharacterImageList()
    {
        return novelMessage.GetCharacterImageList();
    }
}

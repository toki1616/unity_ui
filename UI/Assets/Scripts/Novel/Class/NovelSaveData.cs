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
            return new NovelSaveData(saveNum: saveNum, storyNum: NovelMessageConst.notFindStoryNum);
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

    public void Save(NovelMessage novelMessage, int saveNum)
    {
        var foundData = novelSaveDataList.Find(novelSaveData => novelSaveData.SaveNum == saveNum);
        if (foundData != null)
        {
            foundData.StoryNum = novelMessage.GetStoryNum();
        }

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
        return storyNum != NovelMessageConst.notFindStoryNum;
    }
}

public class NovelSaveDataButtonData
{
    private NovelMessage novelMessage;
    private NovelSaveData novelSaveData;
    private Sprite backgroundImage;
    private List<Sprite> characterImageList = new List<Sprite>();

    public NovelSaveDataButtonData(NovelMessage novelMessage, NovelSaveData novelSaveData)
    {
        this.novelMessage = novelMessage;
        this.novelSaveData = novelSaveData;

        backgroundImage = ResourcesUtils.GetNovelBackgroundImage(path: novelMessage.GetPlaceImage());

        //characterImage
        string[] characterImagePaths = StringSplitUtils.GetSplitNovelCharacterImagePaths(novelMessage.GetCharacterImage());
        foreach (string path in characterImagePaths)
        {
            //Debug.Log($"path : {path}");
            Sprite characterImage = ResourcesUtils.GetNovelCharacterImage(path: path);
            characterImageList.Add(characterImage);
        }
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
        return backgroundImage;
    }

    public List<Sprite> GetCharacterImageList()
    {
        return characterImageList;
    }
}

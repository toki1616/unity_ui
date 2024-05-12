using System.Collections.Generic;
using UnityEngine;

using Util;
public class NovelMessageData
{
    List<NovelMessage> novelMessageData;
    //読み込んでいないため-1から始める
    private int nowStoryNum = -1;

    public NovelMessageData()
    {
        novelMessageData = CsvUtils.ReadNovelCsvFile();

        //foreach (NovelMessage message in novelMessageData)
        //{
        //    Debug.Log($"NovelMessage : storyNum ：{message.GetStoryNum()}, route ：{message.GetRoute()}, message ：{message.GetMessage()}, selectMessage ：{message.GetSelectMessage()}, characterName ：{message.GetCharacterName()}, characterImagePath : {message.GetCharacterImagePath()}, backgroundImagePath : {message.GetBackgroundImagePath()}");
        //}
    }

    public NovelMessage GetNextMessage()
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", backgroundImagePath: "", characterImagePath: "");
        }

        nowStoryNum++;

        if (novelMessageData.Count <= nowStoryNum)
        {
            nowStoryNum = 0;
            //Debug.Log("not next message");
            return novelMessageData[0];
        }

        var novelMessage = novelMessageData.FindAll(novelMessage => novelMessage.GetStoryNum() == nowStoryNum)[0];
        return novelMessage;
    }

    public NovelMessage GetNowMessage()
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", backgroundImagePath: "", characterImagePath: "");
        }

        return novelMessageData[nowStoryNum];
    }
    public NovelMessage GetLoadMessage(int StoryNum)
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", backgroundImagePath: "", characterImagePath: "");
        }

        nowStoryNum = StoryNum;
        return novelMessageData[StoryNum];
    }

    public NovelMessage GetSaveDataNovelMessage(int StoryNum)
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", backgroundImagePath: "", characterImagePath: "");
        }

        return novelMessageData[StoryNum];
    }
}

public class NovelMessage
{
    private int storyNum;
    private string route;
    private string message;
    private string[] selectMessages;
    private string characterName;
    private string backgroundImagePath;
    private string characterImagePath;

    private Sprite backgroundImage;
    private List<Sprite> characterImageList = new List<Sprite>();

    public NovelMessage(int storyNum, string characterName, string message, string backgroundImagePath, string characterImagePath)
    {
        this.storyNum = storyNum;
        this.characterName = characterName;
        this.message = message;
        this.backgroundImagePath = backgroundImagePath;
        this.characterImagePath = characterImagePath;
    }

    public NovelMessage(int storyNum, string route, string message, string selectMessage, string characterName, string backgroundImagePath, string characterImagePath)
    {
        this.storyNum = storyNum;
        this.route = route;
        this.message = message;
        this.selectMessages = StringSplitUtils.GetStringArraySplitAnd(selectMessage);
        this.characterName = characterName;
        this.backgroundImagePath = backgroundImagePath;
        this.characterImagePath = characterImagePath;

        //Debug.Log($"NovelMessage : storyNum ：{this.storyNum}, route ：{this.route}, message ：{this.message}, selectMessage ：{selectMessage}, characterName ：{this.characterName}, characterImagePath : {this.characterImagePath}, backgroundImagePath : {this.backgroundImagePath}");
        //foreach (string value in selectMessages)
        //{
        //    Debug.Log($"selectMessage : {value}");
        //}

        backgroundImage = ResourcesUtils.GetNovelBackgroundImage(path: backgroundImagePath);
        //Debug.Log($"NovelMessage : backgroundImage : {backgroundImage.name}");

        //characterImage
        string[] characterImagePaths = StringSplitUtils.GetStringArraySplitAnd(characterImagePath);
        foreach (string path in characterImagePaths)
        {
            //Debug.Log($"path : {path}");
            Sprite characterImage = ResourcesUtils.GetNovelCharacterImage(path: path);
            //Debug.Log($"NovelMessage : characterImage : {characterImage.name}");
            characterImageList.Add(characterImage);
        }
    }

    public int GetStoryNum()
    {
        return storyNum;
    }

    public string GetRoute()
    {
        return route;
    }

    public string GetMessage()
    {
        return message;
    }

    public string[] GetSelectMessages()
    {
        return selectMessages;
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public string GetBackgroundImagePath()
    {
        return backgroundImagePath;
    }

    public string GetCharacterImagePath()
    {
        return characterImagePath;
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

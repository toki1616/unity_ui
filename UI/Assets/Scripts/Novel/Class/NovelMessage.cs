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
        //    Debug.Log($"storyNum ：{message.GetStoryNum()} ,characterName ：{message.GetCharacterName()} ,message ：{message.GetMessage()} ,placeImage ：{message.GetPlaceImage()} ,characterImage ：{message.GetCharacterImage()}");
        //}
    }

    public NovelMessage GetNextMessage()
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", placeImage: "", characterImage: "");
        }

        nowStoryNum++;

        if (novelMessageData.Count <= nowStoryNum)
        {
            nowStoryNum = 0;
            //Debug.Log("not next message");
            return novelMessageData[0];
        }

        return novelMessageData[nowStoryNum];
    }

    public NovelMessage GetNowMessage()
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", placeImage: "", characterImage: "");
        }

        return novelMessageData[nowStoryNum];
    }
    public NovelMessage GetLoadMessage(int StoryNum)
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, characterName: "データがありません", message: "データがありません", placeImage: "", characterImage: "");
        }

        return novelMessageData[StoryNum];
    }
}

public class NovelMessage
{
    private int storyNum;
    private string characterName;
    private string message;
    private string placeImage;
    private string characterImage;

    public NovelMessage(int storyNum, string characterName, string message, string placeImage, string characterImage)
    {
        this.storyNum = storyNum;
        this.characterName = characterName;
        this.message = message;
        this.placeImage = placeImage;
        this.characterImage = characterImage;
    }

    public int GetStoryNum()
    {
        return storyNum;
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public string GetMessage()
    {
        return message;
    }

    public string GetPlaceImage()
    {
        return placeImage;
    }

    public string GetCharacterImage()
    {
        return characterImage;
    }
}

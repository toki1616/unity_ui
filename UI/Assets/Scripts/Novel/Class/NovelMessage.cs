using System.Collections.Generic;
using UnityEngine;

public class NovelMessageData
{
    List<NovelMessage> novelMessageData;
    private int nextStoryNum = 0;

    public NovelMessageData(CsvUtils csvUtils)
    {
        novelMessageData = csvUtils.ReadNovelCsvFile();

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

        if (novelMessageData.Count <= nextStoryNum)
        {
            nextStoryNum = 0;
            //Debug.Log("not next message");
            return novelMessageData[0];
        }

        nextStoryNum++;
        return novelMessageData[nextStoryNum - 1];
    }
}

public class NovelMessage
{
    int storyNum;
    string characterName;
    string message;
    string placeImage;
    string characterImage;

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

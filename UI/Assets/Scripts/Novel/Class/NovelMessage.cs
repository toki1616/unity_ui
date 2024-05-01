using System.Collections.Generic;
using UnityEngine;

public class NovelMessageData
{
    List<NovelMessage> novelMessageData;

    public NovelMessageData()
    {
        CsvUtils csvUtils = new CsvUtils();
        novelMessageData = csvUtils.ReadNovelCsvFile();
        foreach (NovelMessage message in novelMessageData)
        {
            Debug.Log($"storyNum ：{message.GetStoryNum()} ,characterName ：{message.GetCharacterName()} ,message ：{message.GetMessage()} ,placeImage ：{message.GetPlaceImage()} ,characterImage ：{message.GetCharacterImage()}");
        }
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
        this.placeImage = characterImage;
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

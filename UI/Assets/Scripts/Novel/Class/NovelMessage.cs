﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Util;
public class NovelMessageData
{
    private readonly NovelRouteDataList _novelRouteDataList;

    List<NovelMessage> novelMessageData;
    //読み込んでいないため-1から始める
    private int nowStoryNum = -1;

    public NovelMessageData(NovelRouteDataList novelRouteDataList)
    {
        novelMessageData = CsvUtils.ReadNovelCsvFile();
        _novelRouteDataList = novelRouteDataList;

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
            return new NovelMessage(storyNum: 0, route: "", displayRouteCondition: "", message: "データがありません", selectMessage: "", characterName: "データがありません", characterImagePath: "", backgroundImagePath: "");
        }

        nowStoryNum++;

        if (novelMessageData.Count <= nowStoryNum)
        {
            nowStoryNum = 0;
            //Debug.Log("not next message");
            return novelMessageData[0];
        }

        return GetNovelMessage(nowStoryNum);
    }

    public NovelMessage GetNowMessage()
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, route: "", displayRouteCondition: "", message: "データがありません", selectMessage: "", characterName: "データがありません", characterImagePath: "", backgroundImagePath: "");
        }

        return GetNovelMessage(nowStoryNum);
    }
    public NovelMessage GetLoadMessage(int storyNum)
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, route: "", displayRouteCondition: "", message: "データがありません", selectMessage: "", characterName: "データがありません", characterImagePath: "", backgroundImagePath: "");
        }

        nowStoryNum = storyNum;
        return GetNovelMessage(nowStoryNum);
    }

    public NovelMessage GetSaveDataNovelMessage(int storyNum)
    {
        if (novelMessageData.Count <= 0)
        {
            //Debug.Log("not message data");
            return new NovelMessage(storyNum: 0, route: "", displayRouteCondition: "", message: "データがありません", selectMessage: "", characterName: "データがありません", characterImagePath: "", backgroundImagePath: "");
        }

        return GetNovelMessage(storyNum);
    }

    private NovelMessage GetNovelMessage(int storyNum)
    {
        var novelMessage = new NovelMessage();

        var novelMessages = novelMessageData.FindAll(novelMessage => novelMessage.GetStoryNum() == storyNum);
        novelMessages.Sort((a, b) => b.GetDisplayRouteConditions().Length.CompareTo(a.GetDisplayRouteConditions().Length));

        List<string> nowRouteList = _novelRouteDataList.NowNovelUseRouteData.GetRouteConditionsFromNovelRouteData();
        //foreach (string nowRoute in nowRouteList)
        //{
        //    Debug.Log($"nowRoute : {nowRoute}");
        //}

        foreach (NovelMessage novelMessageTest in novelMessages)
        {
            string[] routeConditions = novelMessageTest.GetDisplayRouteConditions();
            if (routeConditions.Length > 0 || routeConditions != null)
            {
                //foreach (string routeCondition in routeConditions)
                //{
                //    Debug.Log($"routeCondition : {routeCondition}");
                //}

                bool containsAll = routeConditions.All(routeCondition => nowRouteList.Contains(routeCondition));
                if (containsAll)
                {
                    novelMessage = novelMessageTest;
                    break;
                }
            }
            else
            {
                novelMessage = novelMessageTest;
            }
        }

        return novelMessage;
    }
}

public class NovelMessage
{
    private int storyNum;
    private string route;
    private string[] displayRouteConditions;
    private string message;
    private string[] selectMessages;
    private string characterName;
    private string backgroundImagePath;
    private string characterImagePath;

    private Sprite backgroundImage;
    private List<Sprite> characterImageList = new List<Sprite>();

    public NovelMessage()
    {
        
    }

    public NovelMessage(int storyNum, string route, string displayRouteCondition, string message, string selectMessage, string characterName, string backgroundImagePath, string characterImagePath)
    {
        this.storyNum = storyNum;
        this.route = route;
        this.displayRouteConditions = StringSplitUtils.GetStringArraySplitAnd(displayRouteCondition);
        this.message = message;
        this.selectMessages = StringSplitUtils.GetStringArraySplitAnd(selectMessage);
        this.characterName = characterName;
        this.backgroundImagePath = backgroundImagePath;
        this.characterImagePath = characterImagePath;

        //Debug.Log($"NovelMessage : storyNum ：{this.storyNum}, route ：{this.route}, displayRouteCondition : {displayRouteCondition}, message ：{this.message}, selectMessage ：{selectMessage}, characterName ：{this.characterName}, characterImagePath : {this.characterImagePath}, backgroundImagePath : {this.backgroundImagePath}");
        //Debug.Log($"NovelMessage : storyNum ：{GetStoryNum()}, route ：{GetStoryNum()}, message ：{GetMessage()}, selectMessage ：{GetSelectMessages()}, characterName ：{GetCharacterName()}, characterImagePath : {GetCharacterImagePath()}, backgroundImagePath : {GetBackgroundImagePath()}");

        //foreach (string value in displayRouteConditions)
        //{
        //    Debug.Log($"displayRouteCondition : {value}");
        //}

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

    public string[] GetSelectMessages()
    {
        return selectMessages;
    }

    public string GetMessage()
    {
        return message;
    }

    public string[] GetDisplayRouteConditions()
    {
        return displayRouteConditions;
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

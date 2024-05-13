﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;
using Util;

public class NovelRouteDataList
{
    private List<NovelRouteSaveData> novelRouteSaveDataList;
    private NovelUseRouteData nowNovelUseRouteData;
    public NovelUseRouteData NowNovelUseRouteData
    {
        get
        {
            return nowNovelUseRouteData;
        }
    }

    public NovelRouteDataList()
    {
        novelRouteSaveDataList = new List<NovelRouteSaveData>();
        //novelRouteSaveDataList = PlayerDataUtils.LoadAllNovelRouteData();
        nowNovelUseRouteData = new NovelUseRouteData();
    }

    public void SaveNovelRouteData(int saveNum)
    {
        foreach (NovelRouteData novelRouteData in nowNovelUseRouteData.NovelRouteDataList)
        {
            Debug.Log($"nowNovelRouteData : Route : {novelRouteData.Route}, routeCondition : {novelRouteData.RouteCondition}");
        }

        NovelRouteSaveData novelRouteSaveData = new NovelRouteSaveData(saveNum: saveNum, novelUseRouteData: nowNovelUseRouteData);
        PlayerDataUtils.SaveNovelRouteData(novelRouteSaveData);
    }

    public void LoadNovelRouteData(int saveNum)
    {
        var foundData = novelRouteSaveDataList.Find(novelRouteSaveData => novelRouteSaveData.SaveNum == saveNum);
        if (foundData != null)
        {
            nowNovelUseRouteData = foundData.NovelUseRouteData;
        }
    }

    public void AddSelectRoute(string route, int routeCondition)
    {
        NovelRouteData novelRouteData = new NovelRouteData(route: route, routeCondition: routeCondition);
        nowNovelUseRouteData.AddNovelUseRouteData(novelRouteData);
    }
}

[Serializable]
public class NovelRouteSaveData
{
    [SerializeField]
    private int saveNum;
    public int SaveNum
    {
        get
        {
            return saveNum;
        }
    }

    [SerializeField]
    private NovelUseRouteData novelUseRouteData;
    public NovelUseRouteData NovelUseRouteData
    {
        get
        {
            return novelUseRouteData;
        }
    }

    public NovelRouteSaveData(int saveNum, NovelUseRouteData novelUseRouteData)
    {
        this.saveNum = saveNum;
        this.novelUseRouteData = novelUseRouteData;
    }
}

[Serializable]
public class NovelUseRouteData
{
    [SerializeField]
    private List<NovelRouteData> novelRouteDataList;
    public List<NovelRouteData> NovelRouteDataList
    {
        get
        {
            return novelRouteDataList;
        }
    }

    public NovelUseRouteData()
    {
        novelRouteDataList = new List<NovelRouteData>();
    }

    public void AddNovelUseRouteData(NovelRouteData novelRouteData)
    {
        var foundData = novelRouteDataList.Find(routeData => routeData.Route == novelRouteData.Route);
        if (foundData != null)
        {
            foundData.RouteCondition = novelRouteData.RouteCondition;
        }
        else
        {
            novelRouteDataList.Add(novelRouteData);
        }
    }
}

[Serializable]
public class NovelRouteData
{
    [SerializeField]
    private string route;
    public string Route
    {
        get
        {
            return route;
        }
    }

    [SerializeField]
    private int routeCondition;
    public int RouteCondition
    {
        get
        {
            return routeCondition;
        }
        set
        {
            routeCondition = value;
        }
    }

    public NovelRouteData(string route, int routeCondition)
    {
        this.route = route;
        this.routeCondition = routeCondition;
    }
}

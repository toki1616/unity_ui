﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NovelPresenter
{
    private readonly NovelModel _novelModel;

    public NovelPresenter
        (
        NovelModel novelModel
        )
    {
        Debug.Log("NovelPresenter : Inject");
        _novelModel = novelModel;
    }

    public void SendTap()
    {
        //Debug.Log($"test : NovelPresenter : SendTap");
        _novelModel.SendTap();
    }

    //Message
    public IObservable<NovelMessage> sendMessageAsObservable =>
            _novelModel.SendNextMessage
            .Skip(1)    //登録時に走らないように
            .Share();

    public void OnComplatedMessageView()
    {
        _novelModel.OnComplatedMessageView();
    }

    public IObservable<Unit> skipUpdateMessageAsObservable =>
            _novelModel.skipUpdateMessage
            .Publish()
            .RefCount();

    public IObservable<Sprite> sendBackgroundImage =>
            _novelModel.SendBackGroundImage
            .Skip(1)    //登録時に走らないように
            .Share();

    public IObservable<List<Sprite>> sendCharacterImage =>
            _novelModel.SendCharacterImage
            .Skip(1)    //登録時に走らないように
            .Share();

    //SelectMessage
    public IObservable<string[]> sendSelectMessagesAsObservable =>
            _novelModel.SendSelectMessages
            .Skip(1)
            .Publish()
            .RefCount();

    public IObservable<Unit> onClickSelectButtonSubjectAsObservable =>
        _novelModel.onClickSelectButtonSubject
        .Publish()
        .RefCount();

    public void OnClickSelectMessageButton(int buttonNum)
    {
        _novelModel.OnClickSelectMessageButton(buttonNum);
    }

    public void OnClickUnderButton(NovelButtonEnum.Menu menu)
    {
        _novelModel.OnClickUnderButton(menu);
    }

    //SaveData
    public IObservable<bool> activeSaveDataUI =>
            _novelModel.ActiveSaveDataUI
            //.Do(value => Debug.Log($"activeSaveDataUI : {value}"))
            .Publish()
            .RefCount();

    public IObservable<NovelSaveDataButtonData> sendSaveDataButtonData =>
            _novelModel.SendSaveDataButtonData
            //.Do(value => Debug.Log($"activeSaveDataUI : {value}"))
            .Skip(1)
            .Publish()
            .RefCount();

    //SaveButton
    public NovelSaveDataButtonData GetSaveDataButtonData(int saveNum)
    {
        return _novelModel.GetSaveDataButtonData(saveNum);
    }

    public void OnClickSaveDataButton(int saveNum)
    {
        _novelModel.OnClickSaveDataButton(saveNum);
    }

    /// <summary>
    /// Auto
    /// </summary>


    /// <summary>
    /// Skip
    /// </summary>


    /// <summary>
    /// Log
    /// </summary>
    public IObservable<bool> activeLogUI =>
            _novelModel.ActiveLogUI
            //.Do(value => Debug.Log($"activeSaveDataUI : {value}"))
            .Publish()
            .RefCount();

    /// <summary>
    /// Option
    /// </summary>


    //Hidden
    public IObservable<bool> isUIHidden =>
            _novelModel.IsUIActive
            //.Do(value => Debug.Log($"activeSaveDataUI : {value}"))
            .Publish()
            .RefCount();

    //CloseUI
    public void OnClickClose(NovelButtonEnum.CloseUI closeUI)
    {
        _novelModel.OnClickClose(closeUI);
    }
}

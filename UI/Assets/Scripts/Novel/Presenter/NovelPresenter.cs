using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

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

    //Title
    public IObservable<bool> titleUIAsObservable =>
            _novelModel.TitleUIReactiveProperty
            //.Do(value => Debug.Log($"activeSaveDataUI : {value}"))
            .Publish()
            .RefCount();

    public void OnClickTitleMenuButton(NovelButtonEnum.StartMenu menu)
    {
        _novelModel.OnClickTitleMenuButton(menu);
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

    public IObservable<NovelMessage> sendMessageLogObservable =>
            _novelModel.SendMessageLogReactiveProperty
            .Skip(1)    //登録時に走らないように
            .Share();

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

    //End
    public IObservable<bool> endUIActiveAsObservable =>
            _novelModel.EndUIActiveReactiveProperty
            //.Do(value => Debug.Log($"endUIActiveAsObservable : {value}"))
            .Publish()
            .RefCount();

    public IObservable<bool> fadeEndTextActiveAsObservable =>
            _novelModel.fadeEndTextActiveSubject
            //.Do(value => Debug.Log($"fadeEndTextActiveAsObservable : {value}"))
            .Publish()
            .RefCount();

    public void ComplateFadeEndTextActive()
    {
        _novelModel.ComplateFadeEndTextActive();
    }

    public void ComplateFadeEndTextEnactive()
    {
        _novelModel.ComplateFadeEndTextEnactive();
    }

    //Fade
    public IObservable<bool> uiHiddenViewfadeIsActiveAsObservable =>
            _novelModel.uiHiddenViewfadeIsActiveSubject
            .Publish()
            .RefCount();

    public IObservable<bool> uiHiddenViewIsActiveAsObservable =>
            _novelModel.uiHiddenViewIsActiveSubject
            .Publish()
            .RefCount();

    public void ComplateFadeImageActive()
    {
        _novelModel.ComplateFadeImageActive();
    }

    public void ComplateFadeImageEnactive()
    {
        _novelModel.ComplateFadeImageEnactive();
    }
}

using System;
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

    public void SendNextMessage()
    {
        //Debug.Log($"test : NovelPresenter : SendNextMessageText");
        _novelModel.SendNextMessageText();
    }

    public IObservable<NovelMessage> sendMessageAsObservable =>
            _novelModel.SendNextMessage
            .Skip(1)    //登録時に走らないように
            .Share();

    public IObservable<Sprite> sendBackgroundImage =>
            _novelModel.SendBackGroundImage
            .Skip(1)    //登録時に走らないように
            .Share();

    public IObservable<List<Sprite>> sendCharacterImage =>
            _novelModel.SendCharacterImage
            .Skip(1)    //登録時に走らないように
            .Share();

    public void OnClickUnderButton(NovelUnderButtonEnum.Menu menu)
    {
        _novelModel.OnClickUnderButton(menu);
    }

    //SaveButton
    public NovelSaveDataButtonData GetSaveDataButtonData(int saveNum)
    {
        return _novelModel.GetSaveDataButtonData(saveNum);
    }

    public void CreateSaveDataButton()
    {
        _novelModel.CreateSaveButton();
    }
    public IObservable<NovelSaveDataButtonData> sendSaveDataButtonUseNovelMessage =>
            _novelModel.SendSaveDataButtonUseNovelMessage
            .Do(data => Debug.Log($"sendSaveDataButtonUseNovelMessage : save : {data.GetNovelSaveData().SaveNum} : Story : {data.GetNovelSaveData().StoryNum}"))
            .Publish()
            .RefCount();

    public void OnClickSaveDataButton(int saveNum)
    {
        _novelModel.OnClickSaveDataButton(saveNum);
    }
}

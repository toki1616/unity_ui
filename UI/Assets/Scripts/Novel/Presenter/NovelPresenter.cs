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
}

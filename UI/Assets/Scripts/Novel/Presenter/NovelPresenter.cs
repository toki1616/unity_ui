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
        Debug.Log($"test : NovelPresenter : SendNextMessageText");
        _novelModel.SendNextMessageText();
    }

    public IObservable<NovelMessage> sendMessageAsObservable =>
            _novelModel.SendNextMessage
            .Skip(1)    //�o�^���ɑ���Ȃ��悤��
            .Share();

    public void SaveNowMessage()
    {

    }

    public void LoadMessage()
    {

    }
}

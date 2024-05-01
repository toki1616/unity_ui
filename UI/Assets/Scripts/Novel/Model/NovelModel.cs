using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NovelModel
{
    NovelMessageData novelMessageData;
    public NovelModel()
    {
        novelMessageData = new NovelMessageData();
    }

    private readonly ReactiveProperty<NovelMessage> _sendNextMessage = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendNextMessage => _sendNextMessage;

    public void SendNextMessageText()
    {
        Debug.Log($"test : NovelModel : SendNextMessageText");
        NovelMessage novelMessage = novelMessageData.GetNextMessage();
        _sendNextMessage.SetValueAndForceNotify(novelMessage);
    }
}

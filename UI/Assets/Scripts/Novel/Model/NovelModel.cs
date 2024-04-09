using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NovelModel
{
    private readonly ReactiveProperty<NovelMessage> _sendNextMessage = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendNextMessage => _sendNextMessage;

    public void SendNextMessageText()
    {
        Debug.Log($"test : NovelModel : SendNextMessageText");
        NovelMessage novelMessage = new NovelMessage(name: "name", message: "message");
        _sendNextMessage.SetValueAndForceNotify(novelMessage);
    }
}

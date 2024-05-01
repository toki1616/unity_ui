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
        NovelMessage novelMessage = new NovelMessage(storyNum: 0, characterName: "name", message: "message", placeImage: "test", characterImage: "test");
        _sendNextMessage.SetValueAndForceNotify(novelMessage);
    }
}

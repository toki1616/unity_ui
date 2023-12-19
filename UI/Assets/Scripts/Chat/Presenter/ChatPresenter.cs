using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChatPresenter
{
    private readonly ChatModel _chatModel;

    public ChatPresenter
        (
        ChatModel chatModel
        )
    {
        Debug.Log("ChatPresenter : Inject");
        _chatModel = chatModel;
    }

    public void SendMessage(string message)
    {
        Debug.Log($"test : ChatPresenter : SendMessage : {message}");
        _chatModel.SendMessageText(message);
    }

    public IObservable<string> sendMessageAsObservable =>
            _chatModel.SendMessage
            .Skip(1)    //“o˜^Žž‚É‘–‚ç‚È‚¢‚æ‚¤‚É
            .Share();

    public void SendReaction(ChatEnum.Reaction reaction)
    {

    }
}

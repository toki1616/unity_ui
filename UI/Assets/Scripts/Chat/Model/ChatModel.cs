using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ChatModel 
{
    private readonly ReactiveProperty<string> _sendMessage = new ReactiveProperty<string>();
    public IReadOnlyReactiveProperty<string> SendMessage => _sendMessage;

    public void SendMessageText(string message)
    {
        if (string.IsNullOrEmpty(message)) { return; }
        Debug.Log($"test : ChatModel : SendMessageText : {message}");

        _sendMessage.SetValueAndForceNotify(message);
    }
}

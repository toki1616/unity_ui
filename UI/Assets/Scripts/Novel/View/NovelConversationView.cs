using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using System;

public class NovelConversationView : MonoBehaviour
{
    private NovelPresenter _novelPresenter;

    [Inject]
    public void Construct
        (
        NovelPresenter novelPresenter
        )
    {
        _novelPresenter = novelPresenter;
    }

    [SerializeField]
    private Text messageText;
    [SerializeField]
    private Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.sendMessageAsObservable.Subscribe(_ => ReceivedMessage(_)).AddTo(this);
    }

    private void ReceivedMessage(NovelMessage novelMessage)
    {
        //Debug.Log($"NovelConversationView : ReceivedMessage");
        UpdateName(novelMessage.GetCharacterName());
        UpdateMessage(novelMessage.GetMessage());
    }

    private void UpdateName(string name)
    {
        nameText.text = $"{name}";
    }

    private void UpdateMessage(string message)
    {
        messageText.text = "";
        //messageText.text = $"{message}";
        Observable.Range(0, message.Length)
            .Select(index => Observable.Timer(TimeSpan.FromSeconds(0.1)).Select(_ => index))
            .Concat()
            .Subscribe(_ =>
            {
                messageText.text += $"{message[_]}";
            },
            () => {
                Debug.Log("message : complate");
            });
    }
}

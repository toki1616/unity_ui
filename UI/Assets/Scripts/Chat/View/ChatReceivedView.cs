using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class ChatReceivedView : MonoBehaviour
{
    [SerializeField]
    private GameObject _chatMessageLeftIconPanel;

    [SerializeField]
    private GameObject _chatMessageRightIconPanel;

    [SerializeField]
    private RectTransform _scrollRectTransform;

    private ChatPresenter _chatPresenter;
    private LayoutGroup _layoutGroup;

    [Inject]
    public void Construct
            (
            ChatPresenter chatPresenter
            )
    {
        _chatPresenter = chatPresenter;
    }

    // Start is called before the first frame update
    void Start()
    {
        _chatPresenter.sendMessageAsObservable.Subscribe(_ => ReceivedMesssage(_)).AddTo(this);

        _layoutGroup = this.GetComponent<LayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // スクロールビューの高さ調整
        Vector2 offsetMin = _scrollRectTransform.offsetMin;
        _scrollRectTransform.offsetMin = offsetMin;
    }

    private void ReceivedMesssage(string message)
    {
        Debug.Log($"test : ChatReceivedView : ReceivedMesssage : {message}");

        CreateMessagePanel(message, ChatEnum.MessageSendUser.Other);
    }

    private void CreateMessagePanel(string message, ChatEnum.MessageSendUser user)
    {
        GameObject spawnObject;
        if (user == ChatEnum.MessageSendUser.self)
        {
            spawnObject = Instantiate(_chatMessageRightIconPanel, this.transform);
            //spawnObject.GetComponent<SelectMenuButtonView>().SetMenuEnum(menuValue);
        }
        else
        {
            spawnObject = Instantiate(_chatMessageLeftIconPanel, this.transform);
        }

        spawnObject.GetComponent<ChatView>().changeMessageText(message);

        _layoutGroup.CalculateLayoutInputHorizontal();
        _layoutGroup.CalculateLayoutInputVertical();
        _layoutGroup.SetLayoutHorizontal();
        _layoutGroup.SetLayoutVertical();
    }
}

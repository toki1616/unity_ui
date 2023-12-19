using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class ChatSendView : MonoBehaviour
{
    [SerializeField]
    private InputField _sendInputField;

    [SerializeField]
    private Button _sendButton;

    private ChatPresenter _chatPresenter;

    [Inject]
    public void Construct
            (
            ChatPresenter chatPresenter
            )
    {
        _chatPresenter = chatPresenter;
    }

    void Awake()
    {
        _sendButton.onClick.AsObservable()
            .Subscribe(_ => OnClickSendButton())
            .AddTo(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickSendButton()
    {
        if (string.IsNullOrEmpty(_sendInputField.text)) { return; }
        string sendMessage = _sendInputField.text;
        Debug.Log($"test : ChatSendView : OnClickSendButton : {sendMessage}");

        _chatPresenter.SendMessage(sendMessage);
        _sendInputField.text = "";
    }
}

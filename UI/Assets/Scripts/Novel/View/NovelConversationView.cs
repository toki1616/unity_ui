using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            UpdateName("namae");
            UpdateMessage("message");
        }
    }

    private void ReceivedMessage(NovelMessage novelMessage)
    {
        Debug.Log($"NovelConversationView : ReceivedMessage");
        nameText.text = $"{novelMessage.GetName()}";
        messageText.text = $"{novelMessage.GetMessage()}";
    }

    private void UpdateName(string name)
    {
        nameText.text = $"{name}";
    }

    private void UpdateMessage(string message)
    {
        messageText.text = $"{message}";
    }
}

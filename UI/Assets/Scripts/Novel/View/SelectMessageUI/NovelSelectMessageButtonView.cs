using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class NovelSelectMessageButtonView : MonoBehaviour
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
    private Text selectMessageText;

    [SerializeField]
    private Button button;

    private int buttonNum;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AsObservable()
            .Subscribe(_ => onClickButton())
            .AddTo(gameObject);
    }

    public void SetButtonNum(int buttonNum)
    {
        this.buttonNum = buttonNum;
    }
    
    public void SetSelectMessage(string selectMessage)
    {
        selectMessageText.text = selectMessage;
    }

    private void onClickButton()
    {
        //Debug.Log($"NovelSelectMessageButtonView : buttonNum : {buttonNum}");
        _novelPresenter.OnClickSelectMessageButton(buttonNum);
    }
}

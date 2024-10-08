﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class NovelCloseButtonView : MonoBehaviour
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
    private Button button;

    [SerializeField]
    private NovelButtonEnum.CloseUI closeUI;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AsObservable()
            .Subscribe(_ => onClickButton())
            .AddTo(gameObject);
    }

    private void onClickButton()
    {
        //Debug.Log($"NovelSaveDataButtonView : saveNum : {saveNum}");
        _novelPresenter.OnClickClose(closeUI);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class NovelOptionView : MonoBehaviour
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
    private GameObject optionUI;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.optionUIActiveObservable.Subscribe(_ => SettingUIChangeActive(_)).AddTo(this);
    }

    private void SettingUIChangeActive(bool isActive)
    {
        optionUI.SetActive(isActive);
    }
}

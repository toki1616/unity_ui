using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class NovelUIView : MonoBehaviour
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
    private GameObject saveDataUI;

    [SerializeField]
    private GameObject logUI;

    [SerializeField]
    private GameObject optionUI;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.activeSaveDataUI.Subscribe(_ => SaveDataUIChangeActive(_)).AddTo(this);
        _novelPresenter.activeLogUI.Subscribe(_ => LogUIChangeActive(_)).AddTo(this);
    }

    private void SaveDataUIChangeActive(bool isActive)
    {
        saveDataUI.SetActive(isActive);
    }

    private void LogUIChangeActive(bool isActive)
    {
        logUI.SetActive(isActive);
    }

    private void OptionUIChangeActive(bool isActive)
    {
        optionUI.SetActive(isActive);
    }
}

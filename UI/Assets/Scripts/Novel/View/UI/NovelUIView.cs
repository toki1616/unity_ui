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
    private GameObject optionUI;

    [SerializeField]
    private GameObject saveDataUI;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.activeSaveDataUI.Subscribe(_ => SaveDataUIChangeActive(_)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OptionUIChangeActive(bool isActive)
    {
        optionUI.SetActive(isActive);
    }

    private void SaveDataUIChangeActive(bool isActive)
    {
        saveDataUI.SetActive(isActive);
    }
}

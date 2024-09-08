using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class NovelTitleView : MonoBehaviour
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
    private GameObject titleView;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.titleUIAsObservable.Subscribe(_ => TitleUIChangeActive(_)).AddTo(this);
    }

    private void TitleUIChangeActive(bool isActive)
    {
        titleView.SetActive(isActive);
    }
}

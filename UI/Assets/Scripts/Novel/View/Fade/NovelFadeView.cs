using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

using Extention;

public class NovelFadeView : MonoBehaviour
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
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.fadeActiveAsObservable.Subscribe(async _ => await FadeImageActive()).AddTo(this);
        _novelPresenter.fadeEnactiveAsObservable.Subscribe(async _ => await FadeImageEnactive()).AddTo(this);
    }

    private async UniTask FadeImageActive()
    {
        await image.FadeImageAlphaAsync(1f, 1f);
        _novelPresenter.ComplateFadeImageActive();
    }

    private async UniTask FadeImageEnactive()
    {
        await image.FadeImageAlphaAsync(0f, 1f);
        _novelPresenter.ComplateFadeImageEnactive();
    }
}

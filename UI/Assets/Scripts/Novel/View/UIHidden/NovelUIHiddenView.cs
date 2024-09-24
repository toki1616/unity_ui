using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

using MyExtention;

public class NovelUIHiddenView : MonoBehaviour
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
        _novelPresenter.uiHiddenViewfadeIsActiveAsObservable.Subscribe(async _ => await fadeImage(_)).AddTo(this);
        _novelPresenter.uiHiddenViewIsActiveAsObservable.Subscribe(async _ => await activeChangeImage(_)).AddTo(this);
    }

    private async UniTask fadeImage(bool isActive)
    {
        if (isActive)
        {
            await FadeImageActive();
        }
        else
        {
            await FadeImageEnactive();
        }
    }

    private async UniTask FadeImageActive()
    {
        image.raycastTarget = true;
        await image.FadeImageAlphaAsync(1f, 1f);
        _novelPresenter.ComplateFadeImageActive();
    }

    private async UniTask FadeImageEnactive()
    {
        await image.FadeImageAlphaAsync(0f, 1f);
        _novelPresenter.ComplateFadeImageEnactive();
        image.raycastTarget = false;
    }

    private async UniTask activeChangeImage(bool isActive)
    {
        if (isActive)
        {
            await ImageActive();
        }
        else
        {
            await ImageEnactive();
        }
    }

    private async UniTask ImageActive()
    {
        image.raycastTarget = true;
        await image.FadeImageAlphaAsync(1f, 0f);
        _novelPresenter.ComplateFadeImageActive();
    }

    private async UniTask ImageEnactive()
    {
        await image.FadeImageAlphaAsync(0f, 0f);
        _novelPresenter.ComplateFadeImageEnactive();
        image.raycastTarget = false;
    }
}

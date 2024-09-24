using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Cysharp.Threading.Tasks;

using MyExtention;

public class NovelEndTextView : MonoBehaviour
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
    private Text endText;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.fadeEndTextActiveAsObservable.Subscribe(async _ => await fadeText(_)).AddTo(this);
    }

    private async UniTask fadeText(bool isActive)
    {
        if (isActive)
        {
            await FadeTextActive();
        }
        else
        {
            await FadeTextEnactive();
        }
    }

    private async UniTask FadeTextActive()
    {
        await endText.FadeTextAlphaAsync(1f, 1f);
        _novelPresenter.ComplateFadeEndTextActive();
    }

    private async UniTask FadeTextEnactive()
    {
        await endText.FadeTextAlphaAsync(0f, 1f);
        _novelPresenter.ComplateFadeEndTextEnactive();
    }
}

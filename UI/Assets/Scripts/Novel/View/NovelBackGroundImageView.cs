using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class NovelBackGroundImageView : MonoBehaviour
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
    private Image backGroundImage;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.sendBackgroundImage.Subscribe(_ => ReceivedBackgroundImage(_)).AddTo(this);
    }

    private void ReceivedBackgroundImage(Sprite image)
    {
        //Debug.Log($"NovelBackGroundImageView : ReceivedBackgroundImage");
        backGroundImage.sprite = image;
    }
}

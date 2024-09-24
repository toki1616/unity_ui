using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;


public class NovelEndView : MonoBehaviour
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
    private GameObject endView;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.endUIActiveAsObservable.Subscribe(_ => endViewActiveChange(_)).AddTo(this);
    }

    private void endViewActiveChange(bool isActive)
    {
        //Debug.Log("endViewActiveChange");
        endView.SetActive(isActive);
    }
}

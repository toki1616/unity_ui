using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class NovelOperationView : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.isUIHidden.Subscribe(_ => ChangeOperationUIActive(_)).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeOperationUIActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}

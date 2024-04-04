using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class NovelTapView : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("NovelTapView : Tap");
            _novelPresenter.SendNextMessage();
        }
    }
}

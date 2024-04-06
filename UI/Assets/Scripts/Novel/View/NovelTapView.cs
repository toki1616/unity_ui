using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using UnityEngine.EventSystems;

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
        var eventTrigger = this.gameObject.AddComponent<ObservableEventTrigger>();
        // PointerDown
        eventTrigger.OnPointerDownAsObservable()
                    .Subscribe(pointerEventData => OnPointerDown(pointerEventData))
                    .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPointerDown(PointerEventData pointerEventData)
    {
        //Debug.Log(pointerEventData.position);
        _novelPresenter.SendNextMessage();
    }
}

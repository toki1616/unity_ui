using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class NovelLogView : MonoBehaviour
{
    [SerializeField]
    private GameObject _novelMessageParentObject;

    [SerializeField]
    private GameObject _novelMessageLeftIconPanel;

    [SerializeField]
    private GameObject _novelMessageRightIconPanel;

    [SerializeField]
    private RectTransform _scrollRectTransform;

    private NovelPresenter _novelPresenter;
    private LayoutGroup _layoutGroup;

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
        _novelPresenter.sendMessageLogObservable.Subscribe(_ => ReceivedMesssage(_)).AddTo(this);

        _layoutGroup = _novelMessageParentObject.GetComponent<LayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // スクロールビューの高さ調整
        Vector2 offsetMin = _scrollRectTransform.offsetMin;
        _scrollRectTransform.offsetMin = offsetMin;
    }

    private void ReceivedMesssage(NovelMessage novelMessage)
    {
        //Debug.Log($"test : ChatReceivedView : ReceivedMesssage : {message}");

        CreateMessagePanel(novelMessage);
    }

    private void CreateMessagePanel(NovelMessage novelMessage)
    {
        GameObject spawnObject;
        spawnObject = Instantiate(_novelMessageRightIconPanel, _novelMessageParentObject.transform);
        spawnObject.GetComponent<NovelMessageLogView>().SetNovelMessage(novelMessage);

        _layoutGroup.CalculateLayoutInputHorizontal();
        _layoutGroup.CalculateLayoutInputVertical();
        _layoutGroup.SetLayoutHorizontal();
        _layoutGroup.SetLayoutVertical();
    }
}

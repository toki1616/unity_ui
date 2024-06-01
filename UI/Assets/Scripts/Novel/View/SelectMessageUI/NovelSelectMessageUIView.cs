using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

public class NovelSelectMessageUIView : MonoBehaviour
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
    private GameObject buttonParentObject;

    [SerializeField]
    private GameObject spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.sendSelectMessagesAsObservable.Subscribe(_ => CreateSelectMessageButton(_)).AddTo(this);
        _novelPresenter.onClickSelectButtonSubjectAsObservable.Subscribe(_ => OnClickSelectButton()).AddTo(this);
    }

    private void CreateSelectMessageButton(string[] selectMessages)
    {
        //Debug.Log($"NovelSelectMessageUIView : CreateSelectMessageButton");

        buttonParentObject.SetActive(true);
        for (int i = 0; i < selectMessages.Length; i++)
        {
            GameObject spawnObject = Instantiate(spawnPrefab, buttonParentObject.transform);
            var createObj = spawnObject.GetComponent<NovelSelectMessageButtonView>();
            createObj.SetSelectMessage(selectMessages[i]);
            createObj.SetButtonNum(i);
        }
    }

    private void OnClickSelectButton()
    {
        buttonParentObject.SetActive(false);
        DeleteSelectButtons();
    }

    private void DeleteSelectButtons()
    {
        //自分の子供を全て調べる
        foreach (Transform child in buttonParentObject.transform)
        {
            //自分の子供をDestroyする
            Destroy(child.gameObject);
        }
    }
}

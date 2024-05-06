using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

using Const;

public class NovelCreateSaveDataButtonView : MonoBehaviour
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
    private GameObject spawnSaveButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateSaveButton();
    }

    private void CreateSaveButton()
    {
        //Debug.Log($"NovelSaveButtonView : ReceivedBackgroundImage");

        for (int i = SaveConst.startSelectSaveNum; i < SaveConst.saveCount; i++)
        {
            GameObject spawnObject = Instantiate(spawnSaveButtonPrefab, this.transform);
            spawnObject.GetComponent<NovelSaveDataButtonView>().SetSaveNum(i);
        }

        //_novelPresenter.CreateSaveDataButton();
    }
}

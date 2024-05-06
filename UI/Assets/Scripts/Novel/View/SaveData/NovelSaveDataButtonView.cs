using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

using Const;

public class NovelSaveDataButtonView : MonoBehaviour
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

    [SerializeField]
    private GameObject characterImageParentObject;

    [SerializeField]
    private GameObject spawnCharacterImagePrefab;

    [SerializeField]
    private Text messageText;

    [SerializeField]
    private Text dateText;

    [SerializeField]
    private Button button;

    private int saveNum = SaveConst.startSelectSaveNum;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AsObservable()
            .Subscribe(_ => onClickButton())
            .AddTo(gameObject);

        _novelPresenter.sendSaveDataButtonData.Subscribe(_ => UpdateNovelSaveDataButtonData(_)).AddTo(this);
    }

    public void SetSaveNum(int saveNum)
    {
        this.saveNum = saveNum;
        SetNovelSaveDataButtonData(_novelPresenter.GetSaveDataButtonData(saveNum));
    }

    private void SetNovelSaveDataButtonData(NovelSaveDataButtonData novelSaveDataButtonData)
    {
        //Debug.Log($"NovelSaveDataButtonView : SetNovelSaveDataButtonData");
        //Debug.Log($"NovelSaveDataButtonView : Self : saveNum : {saveNum}");
        //Debug.Log($"NovelModel : SetNovelSaveDataButtonData : save : {novelSaveDataButtonData.GetNovelSaveData().SaveNum} : story : {novelSaveDataButtonData.GetNovelSaveData().StoryNum}");
        if (!novelSaveDataButtonData.GetNovelSaveData().isCanLoad())
        {
            return;
        }

        //Debug.Log($"NovelSaveDataButtonView : SetNovelMessage");
        SetBackgroundImage(novelSaveDataButtonData.GetBackgroundImage());
        CreateCharacter(novelSaveDataButtonData.GetCharacterImageList());
        UpdateMessage(novelSaveDataButtonData.GetNovelMessage().GetMessage());
    }

    private void UpdateNovelSaveDataButtonData(NovelSaveDataButtonData novelSaveDataButtonData)
    {
        if (novelSaveDataButtonData.GetNovelSaveData().SaveNum != saveNum)
        {
            return;
        }

        SetNovelSaveDataButtonData(novelSaveDataButtonData);
    }

    private void SetBackgroundImage(Sprite image)
    {
        //Debug.Log($"NovelSaveDataButtonView : SetBackgroundImage");
        backGroundImage.sprite = image;
    }

    private void CreateCharacter(List<Sprite> imageList)
    {
        //Debug.Log($"NovelSaveDataButtonView : CreateCharacter");

        foreach (var image in imageList)
        {
            GameObject spawnObject = Instantiate(spawnCharacterImagePrefab, characterImageParentObject.transform);
            spawnObject.GetComponent<NovelCharacterImageView>().ReceivedCharacterImage(image);
        }
    }

    private void UpdateMessage(string message)
    {
        //Debug.Log($"NovelSaveDataButtonView : UpdateMessage : {message}");
        messageText.text = $"{message}";
    }

    private void UpdateDate(string date)
    {
        dateText.text = $"{name}";
    }

    private void onClickButton()
    {
        //Debug.Log($"NovelSaveDataButtonView : saveNum : {saveNum}");
        _novelPresenter.OnClickSaveDataButton(saveNum);
    }
}

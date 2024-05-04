using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NovelModel
{
    private readonly NovelMessageData _novelMessageData;
    private readonly ResourcesUtils _resourcesUtils;
    private readonly StringSplitUtils _stringSplitUtils;
    private readonly PlayerDataUtils _playerDataUtils;

    public NovelModel(ResourcesUtils resourcesUtils, CsvUtils csvUtils, StringSplitUtils stringSplitUtils, PlayerDataUtils playerDataUtils)
    {
        _novelMessageData = new NovelMessageData(csvUtils);
        _resourcesUtils = resourcesUtils;
        _stringSplitUtils = stringSplitUtils;
        _playerDataUtils = playerDataUtils;
    }

    private readonly ReactiveProperty<NovelMessage> _sendNextMessage = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendNextMessage => _sendNextMessage;

    private readonly ReactiveProperty<Sprite> _sendBackGroundImage = new ReactiveProperty<Sprite>();
    public IReadOnlyReactiveProperty<Sprite> SendBackGroundImage => _sendBackGroundImage;

    private readonly ReactiveProperty<List<Sprite>> _sendCharacterImage = new ReactiveProperty<List<Sprite>>();
    public IReadOnlyReactiveProperty<List<Sprite>> SendCharacterImage => _sendCharacterImage;

    public void SendNextMessageText()
    {
        //Debug.Log($"test : NovelModel : SendNextMessageText");
        NovelMessage novelMessage = _novelMessageData.GetNextMessage();
        SendMessage(novelMessage);
    }

    private void SendMessage(NovelMessage novelMessage)
    {
        //Debug.Log($"storyNum ：{novelMessage.GetStoryNum()} ,characterName ：{novelMessage.GetCharacterName()} ,novelMessage ：{novelMessage.GetMessage()} ,placeImage ：{novelMessage.GetPlaceImage()} ,characterImage ：{novelMessage.GetCharacterImage()}");

        //novelMessage
        _sendNextMessage.SetValueAndForceNotify(novelMessage);

        //BackgroundImage
        Sprite bgImage = _resourcesUtils.GetNovelBackgroundImage(path: novelMessage.GetPlaceImage());
        _sendBackGroundImage.SetValueAndForceNotify(bgImage);

        //characterImage
        string[] characterImagePaths = _stringSplitUtils.GetSplitNovelCharacterImagePaths(novelMessage.GetCharacterImage());
        List<Sprite> characterImageList = new List<Sprite>();

        foreach (string path in characterImagePaths)
        {
            //Debug.Log($"path : {path}");
            Sprite characterImage = _resourcesUtils.GetNovelCharacterImage(path: path);
            characterImageList.Add(characterImage);
        }

        _sendCharacterImage.SetValueAndForceNotify(characterImageList);
    }

    public void OnClickUnderButton(NovelUnderButtonEnum.Menu menu)
    {
        //Debug.Log($"NovelModel : click : {menu}");

        switch (menu)
        {
            case NovelUnderButtonEnum.Menu.Save:
                Save(1);
                break;
            case NovelUnderButtonEnum.Menu.Load:
                Load();
                break;
            case NovelUnderButtonEnum.Menu.QuickSave:
                Save(0);
                break;
            case NovelUnderButtonEnum.Menu.QuickLoad:
                Load();
                break;
            case NovelUnderButtonEnum.Menu.Auto:
                break;
            case NovelUnderButtonEnum.Menu.Skip:
                break;
            case NovelUnderButtonEnum.Menu.Log:
                break;
            case NovelUnderButtonEnum.Menu.Option:
                break;
            case NovelUnderButtonEnum.Menu.Hidden:
                break;
            default:
                break;
        }
    }

    private void Save(int saveNum)
    {
        NovelMessage novelMessage = _novelMessageData.GetNowMessage();
        _playerDataUtils.SaveNovelSaveData(novelMessage: novelMessage, saveNum: saveNum);
    }

    private void Load()
    {
        NovelMessage novelMessage = _novelMessageData.GetLoadMessage(_playerDataUtils.LoadNovelSaveData());
        SendMessage(novelMessage);
    }
}

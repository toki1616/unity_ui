using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

using Util;
using Const;

public class NovelModel
{
    private readonly NovelMessageData _novelMessageData;
    private readonly NovelSaveDataList _novelSaveDataList;
    private readonly NovelRouteDataList _novelRouteDataList;

    public NovelModel()
    {
        _novelRouteDataList = new NovelRouteDataList();
        _novelMessageData = new NovelMessageData(_novelRouteDataList);
        _novelSaveDataList = new NovelSaveDataList();
    }

    public void SendTap()
    {
        //Debug.Log($"test : NovelModel : SendTap");
        if (!_isUIActive.Value)
        {
            Hidden(true);
            return;
        }

        SendNextMessageText();
    }

    private readonly ReactiveProperty<NovelMessage> _sendNextMessage = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendNextMessage => _sendNextMessage;

    private readonly ReactiveProperty<Sprite> _sendBackGroundImage = new ReactiveProperty<Sprite>();
    public IReadOnlyReactiveProperty<Sprite> SendBackGroundImage => _sendBackGroundImage;

    private readonly ReactiveProperty<List<Sprite>> _sendCharacterImage = new ReactiveProperty<List<Sprite>>();
    public IReadOnlyReactiveProperty<List<Sprite>> SendCharacterImage => _sendCharacterImage;

    private readonly ReactiveProperty<string[]> _sendSelectMessages = new ReactiveProperty<string[]>();
    public IReadOnlyReactiveProperty<string[]> SendSelectMessages => _sendSelectMessages;

    public void SendNextMessageText()
    {
        //Debug.Log($"test : NovelModel : SendNextMessageText");
        NovelMessage novelMessage = _novelMessageData.GetNextMessage();
        SendMessage(novelMessage);
    }

    private void SendMessage(NovelMessage novelMessage)
    {
        //Debug.Log($"NovelMessage : storyNum ：{novelMessage.GetStoryNum()}, route ：{novelMessage.GetRoute()}, message ：{novelMessage.GetMessage()}, selectMessage ：{novelMessage.GetSelectMessage()}, characterName ：{novelMessage.GetCharacterName()}, characterImagePath : {novelMessage.GetCharacterImageList()}, backgroundImagePath : {novelMessage.GetBackgroundImage().name}");

        //novelMessage
        _sendNextMessage.SetValueAndForceNotify(novelMessage);

        //BackgroundImage
        _sendBackGroundImage.SetValueAndForceNotify(novelMessage.GetBackgroundImage());

        //characterImage
        _sendCharacterImage.SetValueAndForceNotify(novelMessage.GetCharacterImageList());

        //SelectMessages
        string[] selectMessages = novelMessage.GetSelectMessages();
        if (selectMessages.Length <= 0 || selectMessages == null)
        {
            return;
        }
        _sendSelectMessages.SetValueAndForceNotify(novelMessage.GetSelectMessages());
    }

    //SelectMessage
    public Subject<Unit> onClickSelectButtonSubject = new Subject<Unit>();

    public void OnClickSelectMessageButton(int buttonNum)
    {
        //Debug.Log($"NovelModel : OnClickSelectMessageButton : {buttonNum}");
        NovelMessage novelMessage = _novelMessageData.GetNowMessage();

        _novelRouteDataList.AddSelectRoute(route: novelMessage.GetRoute(), routeCondition: buttonNum);
        onClickSelectButtonSubject.OnNext(Unit.Default);
        SendNextMessageText();
    }

    public void OnClickUnderButton(NovelUnderButtonEnum.Menu menu)
    {
        //Debug.Log($"NovelModel : click : {menu}");

        switch (menu)
        {
            case NovelUnderButtonEnum.Menu.Save:
                OpenSaveDataUI(NovelDataEnum.SaveDataMode.Save);
                break;
            case NovelUnderButtonEnum.Menu.Load:
                OpenSaveDataUI(NovelDataEnum.SaveDataMode.Load);
                break;
            case NovelUnderButtonEnum.Menu.QuickSave:
                Save(SaveConst.quickSaveNum);
                break;
            case NovelUnderButtonEnum.Menu.QuickLoad:
                Load(SaveConst.quickSaveNum);
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
                Hidden(false);
                break;
            default:
                break;
        }
    }

    //SaveData
    private readonly ReactiveProperty<bool> _activeSaveDataUI = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> ActiveSaveDataUI => _activeSaveDataUI;

    private NovelDataEnum.SaveDataMode saveDataMode = NovelDataEnum.SaveDataMode.Save;

    private void OpenSaveDataUI(NovelDataEnum.SaveDataMode mode)
    {
        saveDataMode = mode;
        _activeSaveDataUI.SetValueAndForceNotify(true);
    }

    private void CloseSaveDataUI()
    {
        _activeSaveDataUI.SetValueAndForceNotify(false);
    }

    public void OnClickCloseSaveData()
    {
        CloseSaveDataUI();
    }

    //SaveDataButton
    public NovelSaveDataButtonData GetSaveDataButtonData(int saveNum)
    {
        NovelSaveData novelSaveData = _novelSaveDataList.GetNovelSaveData(saveNum);
        //Debug.Log($"NovelModel : CreateSaveButton : save : {novelSaveData.SaveNum} : story : {novelSaveData.StoryNum}");

        if (!novelSaveData.isCanLoad())
        {
            return new NovelSaveDataButtonData(novelSaveData);
        }

        NovelMessage novelMessage = _novelMessageData.GetSaveDataNovelMessage(novelSaveData.StoryNum);
        //Debug.Log($"NovelModel : CreateSaveButton : message : {novelMessage.GetMessage()}");
        NovelSaveDataButtonData novelSaveDataButtonData = new NovelSaveDataButtonData(novelMessage, novelSaveData);
        return novelSaveDataButtonData;
    }

    public void OnClickSaveDataButton(int saveNum)
    {
        //Debug.Log($"NovelModel : OnClickLoadButton : {saveNum}");
        switch (saveDataMode)
        {
            case NovelDataEnum.SaveDataMode.Save:
                Save(saveNum);
                break;
            case NovelDataEnum.SaveDataMode.Load:
                Load(saveNum);
                break;
            default:
                break;
        }
    }

    private readonly ReactiveProperty<NovelSaveDataButtonData> _sendSaveDataButtonData = new ReactiveProperty<NovelSaveDataButtonData>();
    public IReadOnlyReactiveProperty<NovelSaveDataButtonData> SendSaveDataButtonData => _sendSaveDataButtonData;
    private void Save(int saveNum)
    {
        NovelMessage novelMessage = _novelMessageData.GetNowMessage();
        NovelSaveData novelSaveData = _novelSaveDataList.SaveAndGetSaveData(novelMessage: novelMessage, saveNum: saveNum);
        NovelSaveDataButtonData novelSaveDataButtonData = new NovelSaveDataButtonData(novelMessage, novelSaveData);
        _novelRouteDataList.SaveNovelRouteData(saveNum);
        _sendSaveDataButtonData.SetValueAndForceNotify(novelSaveDataButtonData);
    }

    private void Load(int saveNum)
    {
        NovelMessage novelMessage = _novelMessageData.GetLoadMessage(_novelSaveDataList.GetLoadStoryNum(saveNum));
        _novelRouteDataList.LoadNovelRouteData(saveNum);
        SendMessage(novelMessage);
    }

    //Hidden
    private readonly ReactiveProperty<bool> _isUIActive = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsUIActive => _isUIActive;
    public void Hidden(bool isActive)
    {
        _isUIActive.SetValueAndForceNotify(isActive);
    }
}

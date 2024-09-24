using System;
using System.Collections.Generic;
using Const;
using Cysharp.Threading.Tasks;
using myEnum;
using UniRx;
using UnityEngine;

public class NovelModel
{
    private readonly NovelMessageData _novelMessageData;
    private readonly NovelSaveDataList _novelSaveDataList;
    private readonly NovelRouteDataList _novelRouteDataList;

    private NovelDataEnum.ReadMode nowReadMode = NovelDataEnum.ReadMode.None;

    public NovelModel()
    {
        _novelRouteDataList = new NovelRouteDataList();
        _novelMessageData = new NovelMessageData(_novelRouteDataList);
        _novelSaveDataList = new NovelSaveDataList();
    }

    private void InitializeNovelViewData()
    {
        //novelMessage
        _sendNextMessage.SetValueAndForceNotify(null);
        //BackgroundImage
        _sendBackGroundImage.SetValueAndForceNotify(null);
        //characterImage
        _sendCharacterImage.SetValueAndForceNotify(null);
        //selectMessagea
        _sendSelectMessages.SetValueAndForceNotify(null);

        isSelectMessage = false;
        isComplatedMessage = true;
    }

    public void SendTap()
    {
        //Debug.Log($"test : NovelModel : SendTap");
        if (!_isUIActive.Value)
        {
            Hidden(true);
            return;
        }

        if (nowReadMode == NovelDataEnum.ReadMode.Auto)
        {
            return;
        }

        NextMessageMove();
    }

    /// <summary>
    /// Message
    /// </summary>
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

        if (story != NovelStoryEnum.Story.Main)
        {
            story = NovelStoryEnum.Story.Main;
        }

        isComplatedMessage = false;

        //novelMessage
        _sendNextMessage.SetValueAndForceNotify(novelMessage);

        //BackgroundImage
        _sendBackGroundImage.SetValueAndForceNotify(novelMessage.GetBackgroundImage());

        //characterImage
        _sendCharacterImage.SetValueAndForceNotify(novelMessage.GetCharacterImageList());
    }

    // 選択肢
    private void SendNowSelectMessage()
    {
        NovelMessage novelMessage = _novelMessageData.GetNowMessage();
        SendSelectMessage(novelMessage);
    }

    private bool isSelectMessage = false;
    private void SendSelectMessage(NovelMessage novelMessage)
    {
        string[] selectMessages = novelMessage.GetSelectMessages();
        if (selectMessages.Length <= 0 || selectMessages == null)
        {
            return;
        }

        if (isSelectMessage)
        {
            return;
        }
        isSelectMessage = true;

        _sendSelectMessages.SetValueAndForceNotify(novelMessage.GetSelectMessages());
    }

    private void NextMessageMove()
    {
        //if (!_isUIActive.Value)
        //{
        //    return;
        //}

        if (!isComplatedMessage)
        {
            SendSkipUpdateMessage();
            return;
        }

        if (_novelMessageData.isEnd())
        {
            End();
            return;
        }

        if (_novelMessageData.IsSendSelectMessage())
        {
            SendNowSelectMessage();
            return;
        }

        SendNextMessageText();
    }

    private void SkipNextMessageMove()
    {
        if (!_isUIActive.Value)
        {
            return;
        }

        if (!isSelectMessage)
        {
            if (_novelMessageData.IsSendSelectMessage())
            {
                SendNowSelectMessage();
                return;
            }
        }

        SendNextMessageText();
        SendSkipUpdateMessage();
    }

    private bool isComplatedMessage = true;
    public async UniTask OnComplatedMessageView()
    {
        isComplatedMessage = true;

        if (nowReadMode == NovelDataEnum.ReadMode.Auto)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));

            if (nowReadMode == NovelDataEnum.ReadMode.Auto)
            {
                NextMessageMove();
            }
        }

        if (nowReadMode == NovelDataEnum.ReadMode.Skip)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

            if (nowReadMode == NovelDataEnum.ReadMode.Skip)
            {
                SkipNextMessageMove();
            }
        }
    }

    public Subject<Unit> skipUpdateMessage = new Subject<Unit>();
    private void SendSkipUpdateMessage()
    {
        skipUpdateMessage.OnNext(Unit.Default);
    }

    /// <summary>
    /// UI
    /// </summary>
    //SelectMessage
    public Subject<Unit> onClickSelectButtonSubject = new Subject<Unit>();

    public void OnClickSelectMessageButton(int buttonNum)
    {
        //Debug.Log($"NovelModel : OnClickSelectMessageButton : {buttonNum}");
        NovelMessage novelMessage = _novelMessageData.GetNowMessage();

        _novelRouteDataList.AddSelectRoute(route: novelMessage.GetRoute(), routeCondition: buttonNum);
        onClickSelectButtonSubject.OnNext(Unit.Default);

        if (nowReadMode != NovelDataEnum.ReadMode.Skip)
        {
            SendNextMessageText();
        }
        else
        {
            SkipNextMessageMove();
        }

        isSelectMessage = false;
    }

    public void OnClickUnderButton(NovelButtonEnum.Menu menu)
    {
        //Debug.Log($"NovelModel : click : {menu}");

        switch (menu)
        {
            case NovelButtonEnum.Menu.Save:
                OpenSaveDataUI(NovelDataEnum.SaveDataMode.Save);
                break;
            case NovelButtonEnum.Menu.Load:
                OpenSaveDataUI(NovelDataEnum.SaveDataMode.Load);
                break;
            case NovelButtonEnum.Menu.QuickSave:
                Save(SaveConst.quickSaveNum);
                break;
            case NovelButtonEnum.Menu.QuickLoad:
                Load(SaveConst.quickSaveNum);
                break;
            case NovelButtonEnum.Menu.Auto:
                Auto();
                break;
            case NovelButtonEnum.Menu.Skip:
                Skip();
                break;
            case NovelButtonEnum.Menu.Log:
                Log();
                break;
            case NovelButtonEnum.Menu.Option:
                OpenOptionUI();
                break;
            case NovelButtonEnum.Menu.Hidden:
                Hidden(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Save
    /// </summary>
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

    //SaveDataButton
    public NovelSaveDataButtonData GetSaveDataButtonData(int saveNum)
    {
        //Debug.Log($"GetSaveDataButtonData : saveNum : {saveNum}");

        NovelSaveData novelSaveData = _novelSaveDataList.GetNovelSaveData(saveNum);
        NovelUseRouteData novelUseRouteData = _novelRouteDataList.GetNovelUseRouteData(saveNum);
        //Debug.Log($"NovelModel : CreateSaveButton : save : {novelSaveData.SaveNum} : story : {novelSaveData.StoryNum}");

        if (novelSaveData == null || !novelSaveData.isCanLoad())
        {
            return new NovelSaveDataButtonData(novelSaveData);
        }

        NovelMessage novelMessage = _novelMessageData.GetSaveDataNovelMessage(novelSaveData.StoryNum, novelUseRouteData.GetRouteConditionsFromNovelRouteData());
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

    /// <summary>
    /// Load
    /// </summary>
    /// <param name="saveNum"></param>
    private void Load(int saveNum)
    {
        InitializeNovelViewData();

        _novelRouteDataList.LoadNovelRouteData(saveNum);
        NovelMessage novelMessage = _novelMessageData.GetLoadMessage(_novelSaveDataList.GetLoadStoryNum(saveNum));

        if (_titleUIReactiveProperty.Value)
        {
            _titleUIReactiveProperty.SetValueAndForceNotify(false);
        }

        SendMessage(novelMessage);
    }

    /// <summary>
    /// Auto
    /// </summary>
    private void Auto()
    {
        if (nowReadMode == NovelDataEnum.ReadMode.Auto)
        {
            nowReadMode = NovelDataEnum.ReadMode.None;
        }
        else
        {
            nowReadMode = NovelDataEnum.ReadMode.Auto;
        }

        NextMessageMove();
    }

    /// <summary>
    /// Skip
    /// </summary>
    private void Skip()
    {        
        if (nowReadMode == NovelDataEnum.ReadMode.Skip)
        {
            nowReadMode = NovelDataEnum.ReadMode.None;
        }
        else
        {
            nowReadMode = NovelDataEnum.ReadMode.Skip;
            SkipNextMessageMove();
        }
    }

    /// <summary>
    /// Log
    /// </summary>
    private readonly ReactiveProperty<bool> _activeLogUI = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> ActiveLogUI => _activeLogUI;
    private void Log()
    {
        SendMessageLogs();
        OpenLogUI();
    }

    private void OpenLogUI()
    {
        _activeLogUI.SetValueAndForceNotify(true);
    }

    private void CloseLogUI()
    {
        _activeLogUI.SetValueAndForceNotify(false);
    }

    private readonly ReactiveProperty<NovelMessage> _sendMessageLogReactiveProperty = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendMessageLogReactiveProperty => _sendMessageLogReactiveProperty;
    private void SendMessageLogs()
    {
        List<NovelMessage> novelMessages = _novelMessageData.GetNovelMessagesLog();
        if (novelMessages == null)
        {
            return;
        }

        foreach (var novelMessage in novelMessages)
        {
            SendMessageLog(novelMessage);
        }
    }

    private void SendMessageLog(NovelMessage novelMessage)
    {
        _sendMessageLogReactiveProperty.SetValueAndForceNotify(novelMessage);
    }


    /// <summary>
    /// Option
    /// </summary>
    private readonly ReactiveProperty<bool> _optionUIActiveReactiveProperty = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> OptionUIActiveReactiveProperty => _optionUIActiveReactiveProperty;

    private void OpenOptionUI()
    {
        _optionUIActiveReactiveProperty.SetValueAndForceNotify(true);
    }

    private void CloseOptionUI()
    {
        _optionUIActiveReactiveProperty.SetValueAndForceNotify(false);
    }

    /// <summary>
    /// Hidden
    /// </summary>
    private readonly ReactiveProperty<bool> _isUIActive = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> IsUIActive => _isUIActive;
    public void Hidden(bool isActive)
    {
        _isUIActive.SetValueAndForceNotify(isActive);
    }

    //CloseUI
    public void OnClickClose(NovelButtonEnum.CloseUI closeUI)
    {
        switch (closeUI)
        {
            case NovelButtonEnum.CloseUI.Save:
                CloseSaveDataUI();
                break;

            case NovelButtonEnum.CloseUI.Log:
                CloseLogUI();
                break;
            
            case NovelButtonEnum.CloseUI.Option:
                CloseOptionUI();
                break;
        }
    }

    //StartMenu
    private readonly ReactiveProperty<bool> _titleUIReactiveProperty = new ReactiveProperty<bool>(true);
    public IReadOnlyReactiveProperty<bool> TitleUIReactiveProperty => _titleUIReactiveProperty;

    public void OnClickTitleMenuButton(NovelButtonEnum.StartMenu menu)
    {
        //Debug.Log($"NovelModel : title : click : {menu}");

        switch (menu)
        {
            case NovelButtonEnum.StartMenu.Start:
                StartFadeActive();
                break;

            case NovelButtonEnum.StartMenu.Load:
                OpenSaveDataUI(NovelDataEnum.SaveDataMode.Load);
                break;

            case NovelButtonEnum.StartMenu.QuickLoad:
                Load(SaveConst.quickSaveNum);
                break;

            case NovelButtonEnum.StartMenu.Option:
                OpenOptionUI();
                break;
            default:
                break;
        }
    }

    private void OpenTitleUI()
    {
        InitializeNovelViewData();
        _endUIActiveReactiveProperty.SetValueAndForceNotify(false);
        _titleUIReactiveProperty.SetValueAndForceNotify(true);
    }

    //Start
    private void StartFadeActive()
    {
        _novelMessageData.Initialize();
        story = NovelStoryEnum.Story.Start;

        uiHiddenViewfadeIsActiveSubject.OnNext(true);
    }
    private void StartFadeEnactive()
    {
        _titleUIReactiveProperty.SetValueAndForceNotify(false);

        uiHiddenViewfadeIsActiveSubject.OnNext(false);
    }

    private void StartComplateFade()
    {

        NextMessageMove();
    }

    //End
    private readonly ReactiveProperty<bool> _endUIActiveReactiveProperty = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> EndUIActiveReactiveProperty => _endUIActiveReactiveProperty;

    public Subject<bool> fadeEndTextActiveSubject = new Subject<bool>();

    private void End()
    {
        switch (story)
        {
            case NovelStoryEnum.Story.Main:
                EndFadeImageActive();
                break;

            case NovelStoryEnum.Story.End:
                EndComplateFadeTextEnactive();
                break;

            default:
                break;
        }
    }

    private void EndFadeImageActive()
    {
        story = NovelStoryEnum.Story.EndStart;
        uiHiddenViewfadeIsActiveSubject.OnNext(true);
        _endUIActiveReactiveProperty.SetValueAndForceNotify(true);
    }
    private void EndComplateFadeTextActive()
    {
        //Debug.Log("EndComplateFadeImageActive");
        fadeEndTextActiveSubject.OnNext(true);
    }

    private void EndComplateFadeTextEnactive()
    {
        //Debug.Log("EndComplateFadeTextEnactive");
        fadeEndTextActiveSubject.OnNext(false);
    }

    //fade endText complate
    public void ComplateFadeEndTextActive()
    {
        //Debug.Log("ComplateFadeEndTextActive");
        story = NovelStoryEnum.Story.End;
    }

    public void ComplateFadeEndTextEnactive()
    {
        //Debug.Log("ComplateFadeEndTextEnactive");
        uiHiddenViewfadeIsActiveSubject.OnNext(false);
        OpenTitleUI();
    }

    //fade
    public Subject<bool> uiHiddenViewIsActiveSubject = new Subject<bool>();
    public Subject<bool> uiHiddenViewfadeIsActiveSubject = new Subject<bool>();

    private NovelStoryEnum.Story story = NovelStoryEnum.Story.Start;
    
    public void ComplateFadeImageActive()
    {
        //Debug.Log("ComplateFadeImageActive");

        switch (story)
        {
            case NovelStoryEnum.Story.Start:
                StartFadeEnactive();
                break;

            case NovelStoryEnum.Story.EndStart:
                EndComplateFadeTextActive();
                break;

            default:
                break;
        }
    }

    public void ComplateFadeImageEnactive()
    {
        //Debug.Log("ComplateFadeImageEnactive");

        switch (story)
        {
            case NovelStoryEnum.Story.Start:
                StartComplateFade();
                break;

            default:
                break;
        }
    }
}

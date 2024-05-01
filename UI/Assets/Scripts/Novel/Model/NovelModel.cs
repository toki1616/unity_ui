using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class NovelModel
{
    private readonly NovelMessageData _novelMessageData;
    private readonly ResourcesUtils _resourcesUtils;

    public NovelModel(ResourcesUtils resourcesUtils, CsvUtils csvUtils)
    {
        _novelMessageData = new NovelMessageData(csvUtils);
        _resourcesUtils = resourcesUtils;
    }

    private readonly ReactiveProperty<NovelMessage> _sendNextMessage = new ReactiveProperty<NovelMessage>();
    public IReadOnlyReactiveProperty<NovelMessage> SendNextMessage => _sendNextMessage;

    private readonly ReactiveProperty<Sprite> _sendBackGroundImage = new ReactiveProperty<Sprite>();
    public IReadOnlyReactiveProperty<Sprite> SendBackGroundImage => _sendBackGroundImage;

    public void SendNextMessageText()
    {
        //Debug.Log($"test : NovelModel : SendNextMessageText");
        NovelMessage novelMessage = _novelMessageData.GetNextMessage();
        //Debug.Log($"storyNum ：{novelMessage.GetStoryNum()} ,characterName ：{novelMessage.GetCharacterName()} ,novelMessage ：{novelMessage.GetMessage()} ,placeImage ：{novelMessage.GetPlaceImage()} ,characterImage ：{novelMessage.GetCharacterImage()}");

        _sendNextMessage.SetValueAndForceNotify(novelMessage);

        Sprite bgImage = _resourcesUtils.GetNovelBackgroundImage(path: novelMessage.GetPlaceImage());
        _sendBackGroundImage.SetValueAndForceNotify(bgImage);
    }
}

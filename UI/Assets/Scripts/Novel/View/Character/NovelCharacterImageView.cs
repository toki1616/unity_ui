using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class NovelCharacterImageView : MonoBehaviour
{
    [SerializeField]
    private Image characterImage;

    public void ReceivedCharacterImage(Sprite image)
    {
        //Debug.Log($"NovelCharacterImageView : ReceivedBackgroundImage");
        characterImage.sprite = image;
    }
}

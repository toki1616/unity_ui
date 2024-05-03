using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

public class NovelCreateCharacterView : MonoBehaviour
{
    NovelPresenter _novelPresenter;

    [Inject]
    public void Construct
        (
        NovelPresenter novelPresenter
        )
    {
        _novelPresenter = novelPresenter;
    }

    [SerializeField] GameObject _spawnCharacterImagePrefab;

    // Start is called before the first frame update
    void Start()
    {
        _novelPresenter.sendCharacterImage.Subscribe(_ => CreateCharacter(_)).AddTo(this);
    }

    private void CreateCharacter(List<Sprite> imageList)
    {
        //Debug.Log($"NovelCreateCharacterView : ReceivedBackgroundImage");
        foreach (var image in imageList)
        {
            GameObject spawnObject = Instantiate(_spawnCharacterImagePrefab, this.transform);
            spawnObject.GetComponent<NovelCharacterImageView>().ReceivedCharacterImage(image);
        }
    }
}

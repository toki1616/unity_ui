using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class NovelUnderButtonView : MonoBehaviour
{
    private NovelPresenter _novelPresenter;

    [Inject]
    public void Construct(NovelPresenter novelPresenter)
    {
        _novelPresenter = novelPresenter;
    }

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Text _nameText;

    private NovelButtonEnum.Menu menu = NovelButtonEnum.Menu.Option;

    void Awake()
    {
        _button.onClick.AsObservable()
            .Subscribe(_ => onClickButton())
            .AddTo(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetMenuEnum(NovelButtonEnum.Menu menuEnum)
    {
        menu = menuEnum;

        ChangeButtonText();
        SetObserbavable();
    }

    private void ChangeButtonText()
    {
        var name = new NovelButtonEnum().GetMenuListName(menu);
        _nameText.text = name;
    }

    private void SetObserbavable()
    {
        switch (menu)
        {
            case NovelButtonEnum.Menu.Auto:
                _novelPresenter.autoIsActiveObservable.Subscribe(_ => OnChangeButtonActiveColor(_)).AddTo(this);
                break;

            case NovelButtonEnum.Menu.Skip:
                _novelPresenter.skipIsActiveObservable.Subscribe(_ => OnChangeButtonActiveColor(_)).AddTo(this);
                break;

            default:
                break;
        }
    }

    private void onClickButton()
    {
        //Debug.Log($"NovelUnderButtonView : click : {menu}");
        _novelPresenter.OnClickUnderButton(menu);
    }

    private void OnChangeButtonActiveColor(bool isActive)
    {
        ColorBlock colorBlock = _button.colors;
        if (isActive)
        {
            Color selectedColor = new Color(100f / 255f, 100f / 255f, 100f / 255f);
            colorBlock.normalColor = selectedColor;
            colorBlock.selectedColor = selectedColor;
        }
        else
        {
            colorBlock.normalColor = Color.white;
            colorBlock.selectedColor = Color.white;
        }

        _button.colors = colorBlock;
    }
}

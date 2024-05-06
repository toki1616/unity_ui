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

    private NovelUnderButtonEnum.Menu menu = NovelUnderButtonEnum.Menu.Option;

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

    public void SetMenuEnum(NovelUnderButtonEnum.Menu menuEnum)
    {
        menu = menuEnum;

        ChangeButtonText();
    }

    private void ChangeButtonText()
    {
        var name = new NovelUnderButtonEnum().GetMenuListName(menu);
        _nameText.text = name;
    }

    private void onClickButton()
    {
        //Debug.Log($"NovelUnderButtonView : click : {menu}");
        _novelPresenter.OnClickUnderButton(menu);
    }
}

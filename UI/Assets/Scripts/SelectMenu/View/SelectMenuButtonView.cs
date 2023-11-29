using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class SelectMenuButtonView : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private Text _nameText;

    private SelectMenuEnum.Menu menu = SelectMenuEnum.Menu.Chat;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMenuEnum(SelectMenuEnum.Menu menuEnum)
    {
        menu = menuEnum;

        ChangeButtonText();
    }

    private void ChangeButtonText()
    {
        var name = new SelectMenuEnum().GetMenuListName(menu);
        _nameText.text = name;
    }

    private void onClickButton()
    {
        Debug.Log($"test : click : {menu}");
    }
}

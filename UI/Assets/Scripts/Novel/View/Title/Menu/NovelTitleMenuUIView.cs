using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NovelTitleMenuUIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateUnderButton();
    }

    private void CreateUnderButton()
    {
        Array array = Enum.GetValues(typeof(NovelButtonEnum.StartMenu));
        foreach (var menuValue in array.Cast<NovelButtonEnum.StartMenu>())
        {
            GameObject spawnObject = Instantiate(_spawnPrefab, this.transform);
            spawnObject.GetComponent<NovelTitleMenuButtonView>().SetMenuEnum(menuValue);
        }
    }
}

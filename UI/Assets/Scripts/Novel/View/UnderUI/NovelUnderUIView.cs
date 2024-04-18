using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NovelUnderUIView : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateUnderButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUnderButton()
    {
        Array array = Enum.GetValues(typeof(NovelUnderButtonEnum.Menu));
        foreach (var menuValue in array.Cast<NovelUnderButtonEnum.Menu>())
        {
            GameObject spawnObject = Instantiate(_spawnPrefab, this.transform);
            spawnObject.GetComponent<NovelUnderButtonView>().SetMenuEnum(menuValue);
        }
    }
}

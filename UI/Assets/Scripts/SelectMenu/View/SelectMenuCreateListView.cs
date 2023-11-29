using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectMenuCreateListView : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Array array = Enum.GetValues(typeof(SelectMenuEnum.Menu));
        foreach (var menuValue in array.Cast<SelectMenuEnum.Menu>())
        {
            GameObject spawnObject = Instantiate(_spawnPrefab, this.transform);
            spawnObject.GetComponent<SelectMenuButtonView>().SetMenuEnum(menuValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

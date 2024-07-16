using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelMessageLogView : MonoBehaviour
{
    [SerializeField]
    private Text _messageText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNovelMessage(NovelMessage novelMessage)
    {
        _messageText.text = novelMessage.GetMessage();
    }
}

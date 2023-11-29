using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenuEnum
{
    public enum Menu
    {
        Chat,
        Novel,
    }

    public string GetMenuListName(SelectMenuEnum.Menu menuEnum)
    {
        switch (menuEnum)
        {
            case Menu.Chat:
                return "�`���b�g";
            case Menu.Novel:
                return "�m�x��";
            default:
                return "";
        }
    }
}

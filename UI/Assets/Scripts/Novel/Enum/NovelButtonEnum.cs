using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovelDataEnum
{
    public enum SaveDataMode
    {
        Save,
        Load,
    }

    public enum ReadMode
    {
        None,
        Auto,
        Skip,
    }
}

public class NovelButtonEnum
{
    public enum Menu
    {
        Save,
        Load,
        QuickSave,
        QuickLoad,
        Auto,
        Skip,
        Log,
        Option,
        Hidden,
    }

    public string GetMenuListName(Menu menuEnum)
    {
        switch (menuEnum)
        {
            case Menu.Save:
                return "SAVE";
            case Menu.Load:
                return "LOAD";
            case Menu.QuickSave:
                return "Q.SAVE";
            case Menu.QuickLoad:
                return "Q.LOAD";
            case Menu.Auto:
                return "AUTO";
            case Menu.Skip:
                return "SKIP";
            case Menu.Log:
                return "LOG";
            case Menu.Option:
                return "OPTION";
            case Menu.Hidden:
                return "×";
            default:
                return "";
        }
    }

    public enum StartMenu
    {
        Start,
        Load,
        QuickLoad,
        Option,
    }

    public string GetStartMenuListName(StartMenu startMenuEnum)
    {
        switch (startMenuEnum)
        {
            case StartMenu.Start:
                return "START";
            case StartMenu.Load:
                return "LOAD";
            case StartMenu.QuickLoad:
                return "Q.LOAD";
            case StartMenu.Option:
                return "OPTION";
            default:
                return "";
        }
    }

    public enum CloseUI
    {
        Save,
        Log,
        Option,
    }
}

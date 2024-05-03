using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringSplitUtils
{
    public string[] GetSplitNovelCharacterImagePaths(string value)
    {
        if (!value.Contains("&"))
        {
            return new string[1] {value};
        }

        string[] splitString = value.Split('&');
        return splitString;
    }
}

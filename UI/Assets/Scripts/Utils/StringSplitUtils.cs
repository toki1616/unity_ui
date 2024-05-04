using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class StringSplitUtils
    {
        public static string[] GetSplitNovelCharacterImagePaths(string value)
        {
            if (!value.Contains("&"))
            {
                return new string[1] { value };
            }

            string[] splitString = value.Split('&');
            return splitString;
        }
    }
}

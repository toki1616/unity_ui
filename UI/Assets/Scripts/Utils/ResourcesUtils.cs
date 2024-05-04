using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class ResourcesUtils
    {
        private const string novelDataPath = "NovelData";

        private static Sprite GetSpriteImage(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public static Sprite GetNovelBackgroundImage(string path)
        {
            return GetSpriteImage($"{novelDataPath}/BackgroundImage/{path}");
        }

        public static Sprite GetNovelCharacterImage(string path)
        {
            return GetSpriteImage($"{novelDataPath}/CharacterImage/{path}");
        }
    }
}

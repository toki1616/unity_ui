using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Const;

namespace Util
{
    public static class ResourcesUtils
    {
        private static Sprite GetSpriteImage(string path)
        {
            return Resources.Load<Sprite>(path);
        }

        public static Sprite GetNovelBackgroundImage(string path)
        {
            return GetSpriteImage($"{ResourceConst.novelDataPath}/BackgroundImage/{path}");
        }

        public static Sprite GetNovelCharacterImage(string path)
        {
            return GetSpriteImage($"{ResourceConst.novelDataPath}/CharacterImage/{path}");
        }
    }
}

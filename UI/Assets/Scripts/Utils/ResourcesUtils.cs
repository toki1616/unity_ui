using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesUtils
{
    private const string novelDataPath = "NovelData";

    private Sprite GetSpriteImage(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public Sprite GetNovelBackgroundImage(string path)
    {
        return GetSpriteImage($"{novelDataPath}/BackgroundImage/{path}");
    }

    public Sprite GetNovelCharacterImage(string path)
    {
        return GetSpriteImage($"{novelDataPath}/CharacterImage/{path}");
    }
}

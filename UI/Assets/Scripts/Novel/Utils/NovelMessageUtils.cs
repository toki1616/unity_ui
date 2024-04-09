using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NovelMessageUtils
{
    public class NovelMessage
    {
        public string message;
        public string savePoint;
    }

    private NovelMessage[] CreateNovelMessage()
    {
        NovelMessage[] novelMessages = new NovelMessage[4];

        for (int i = 0; i > 4; i++)
        {
            NovelMessage novelMessage = new NovelMessage();
            novelMessage.message = $"test{i}";
            novelMessage.savePoint = $"test{i}";

            novelMessages.SetValue(novelMessage, i);
        }

        return novelMessages;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace MyExtention
{
    public static class TextEx
    {
        /// <summary>
        /// Textの不透明度を設定する
        /// </summary>
        /// <param name="text">設定対象のTextコンポーネント(this)</param>
        /// <param name="alpha">不透明度。0=透明 1=不透明</param>
        public static void SetOpacity(this Text text, float alpha)
        {
            var c = text.color;
            text.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <param name="text">設定対象のTextコンポーネント(this)</param>
        /// <param name="targetAlpha">不透明度。0=透明 1=不透明</param>
        /// /// <param name="duration">fadeの時間</param>
        public static async UniTask FadeTextAlphaAsync(this Text text, float targetAlpha, float duration = 1f)
        {
            while (!Mathf.Approximately(text.color.a, targetAlpha))
            {
                float changePerFrame = Time.deltaTime / duration;
                float alpha = Mathf.MoveTowards(text.color.a, targetAlpha, changePerFrame);
                text.SetOpacity(alpha);
                await UniTask.Yield();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace MyExtention
{
    public static class ImageEx
    {
        /// <summary>
        /// Imageの不透明度を設定する
        /// </summary>
        /// <param name="image">設定対象のImageコンポーネント(this)</param>
        /// <param name="alpha">不透明度。0=透明 1=不透明</param>
        public static void SetOpacity(this Image image, float alpha)
        {
            var c = image.color;
            image.color = new Color(c.r, c.g, c.b, alpha);
        }

        /// <param name="image">設定対象のImageコンポーネント(this)</param>
        /// <param name="targetAlpha">不透明度。0=透明 1=不透明</param>
        /// /// <param name="duration">fadeの時間</param>
        public static async UniTask FadeImageAlphaAsync(this Image image, float targetAlpha, float duration = 1f)
        {
            while (!Mathf.Approximately(image.color.a, targetAlpha))
            {
                float changePerFrame = Time.deltaTime / duration;
                float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, changePerFrame);
                image.SetOpacity(alpha);
                await UniTask.Yield();
            }
        }
    }
}

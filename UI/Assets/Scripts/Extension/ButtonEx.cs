using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyExtention
{
    public static class ButtonEx
    {
        /// <param name="button">設定対象のButtonコンポーネント(this)</param>
        /// <param name="baseColor">ベースとなる色</param>
        /// <param name="highlightColorDifference">highlightedColorの色のベースカラーとの差</param>
        /// <param name="pressedColorDifference">pressedColorの色のベースカラーとの差</param>
        public static void ChangeColorBlock(this Button button, Color baseColor, float highlightColorDifference = 10f, float pressedColorDifference = 55f, float selectedColorDifference = 10f, float disabledColorDifference = 55f)
        {
            ColorBlock colorBlock = button.colors;

            float highlightDifferenceValue = highlightColorDifference / 255f;
            Color highlightColor = new Color(baseColor.r - highlightDifferenceValue, baseColor.g - highlightDifferenceValue, baseColor.b - highlightDifferenceValue);

            float pressedColorDifferenceValue = pressedColorDifference / 255f;
            Color pressedColor = new Color(baseColor.r - pressedColorDifferenceValue, baseColor.g - pressedColorDifferenceValue, baseColor.b - pressedColorDifferenceValue);

            float selectedColorDifferenceValue = selectedColorDifference / 255f;
            Color selectedColor = new Color(baseColor.r - selectedColorDifferenceValue, baseColor.g - selectedColorDifferenceValue, baseColor.b - selectedColorDifferenceValue);

            float disabledColorDifferenceValue = disabledColorDifference / 255f;
            Color disabledColor = new Color(baseColor.r - disabledColorDifferenceValue, baseColor.g - disabledColorDifferenceValue, baseColor.b - disabledColorDifferenceValue);

            colorBlock.normalColor = baseColor;
            colorBlock.highlightedColor = highlightColor;
            colorBlock.pressedColor = pressedColor;
            colorBlock.selectedColor = selectedColor;
            colorBlock.disabledColor = disabledColor;

            button.colors = colorBlock;
        }
    }
}

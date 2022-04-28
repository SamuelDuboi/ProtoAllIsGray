using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Color Bank", fileName ="new Color Bank")]
public class PlayerColorBank : ScriptableObject
{
    [System.Serializable]
    public struct ColorPair
    {
        [ColorUsage(true, true)]
        public Color mainColor;
        [ColorUsage(true, true)]
        public Color secondaryColor;
    }

    public List<ColorPair> skinColors;
}

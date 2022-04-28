using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSkinHandler : MonoBehaviour
{
    [SerializeField]
    Renderer skinRenderer;
    private MaterialPropertyBlock propBlock;

    private void Awake()
    {
        //propBlock = new MaterialPropertyBlock();
    }

    public void InitColor(PlayerColorBank.ColorPair colorPair)
    {
        propBlock = new MaterialPropertyBlock();
        skinRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_SkinCharaColor1", colorPair.mainColor);
        propBlock.SetColor("_SkinCharaColor2", colorPair.secondaryColor);
        skinRenderer.SetPropertyBlock(propBlock);
    }
}

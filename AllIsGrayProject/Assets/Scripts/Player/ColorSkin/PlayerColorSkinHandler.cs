using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSkinHandler : MonoBehaviour
{
    [SerializeField]
    Renderer skinRenderer;
    [SerializeField]
    TrailRenderer trailRendererOne;
    [SerializeField]
    TrailRenderer trailRendererTwo;
    private MaterialPropertyBlock propBlock;
    private MaterialPropertyBlock jetPackPropBlockOne;
    private MaterialPropertyBlock jetPackPropBlockTwo;

    private void Awake()
    {
        //propBlock = new MaterialPropertyBlock();
    }

    public void InitColor(PlayerColorBank.ColorPair colorPair)
    {
        propBlock = new MaterialPropertyBlock();
        jetPackPropBlockOne = new MaterialPropertyBlock();
        jetPackPropBlockTwo = new MaterialPropertyBlock();

        skinRenderer.GetPropertyBlock(propBlock);
        trailRendererOne.GetPropertyBlock(jetPackPropBlockOne);
        trailRendererTwo.GetPropertyBlock(jetPackPropBlockTwo);

        propBlock.SetColor("_SkinCharaColor1", colorPair.mainColor);
        propBlock.SetColor("_SkinCharaColor2", colorPair.secondaryColor);

        jetPackPropBlockOne.SetColor("_SkinCharaColor2", colorPair.secondaryColor);
        jetPackPropBlockTwo.SetColor("_SkinCharaColor2", colorPair.secondaryColor);

        trailRendererOne.SetPropertyBlock(jetPackPropBlockOne);
        trailRendererTwo.SetPropertyBlock(jetPackPropBlockTwo);
        skinRenderer.SetPropertyBlock(propBlock);
    }
}

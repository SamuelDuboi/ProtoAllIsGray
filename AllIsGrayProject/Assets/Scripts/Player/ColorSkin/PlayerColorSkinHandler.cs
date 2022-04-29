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
    [SerializeField]
    ParticleSystemRenderer particlesRendererOne;
    [SerializeField]
    ParticleSystemRenderer particlesRendererTwo;
    [SerializeField]
    Renderer flamesRendererOne;
    [SerializeField]
    Renderer flamesRendererTwo;

    private MaterialPropertyBlock propBlock;
    private MaterialPropertyBlock jetPackPropBlockOne;
    private MaterialPropertyBlock jetPackPropBlockTwo;
    private MaterialPropertyBlock particlesPropBlockOne;
    private MaterialPropertyBlock particlesPropBlockTwo;
    private MaterialPropertyBlock flamesPropBlockOne;
    private MaterialPropertyBlock flamesPropBlockTwo;


    private void Awake()
    {
        //propBlock = new MaterialPropertyBlock();
    }

    public void InitColor(PlayerColorBank.ColorPair colorPair)
    {
        //Trails
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

        //Particles
        particlesPropBlockOne = new MaterialPropertyBlock();
        particlesPropBlockTwo = new MaterialPropertyBlock();

        particlesRendererOne.GetPropertyBlock(particlesPropBlockOne);
        particlesRendererTwo.GetPropertyBlock(particlesPropBlockTwo);

        particlesPropBlockOne.SetColor("_SkinCharaColor2", colorPair.secondaryColor);
        particlesPropBlockTwo.SetColor("_SkinCharaColor2", colorPair.secondaryColor);

        particlesRendererOne.SetPropertyBlock(particlesPropBlockOne);
        particlesRendererTwo.SetPropertyBlock(particlesPropBlockTwo);

        //Flames
        flamesPropBlockOne = new MaterialPropertyBlock();
        flamesPropBlockTwo = new MaterialPropertyBlock();

        flamesRendererOne.GetPropertyBlock(flamesPropBlockOne);
        flamesRendererTwo.GetPropertyBlock(flamesPropBlockTwo);

        flamesPropBlockOne.SetColor("_SkinCharaColor2", colorPair.secondaryColor);
        flamesPropBlockTwo.SetColor("_SkinCharaColor2", colorPair.secondaryColor);

        flamesRendererOne.SetPropertyBlock(flamesPropBlockOne);
        flamesRendererTwo.SetPropertyBlock(flamesPropBlockTwo);
    }
}

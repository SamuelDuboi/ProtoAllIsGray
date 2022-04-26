using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SceneManagementUI : MonoBehaviour
{
    [Header("Transition References")]
    [SerializeField]
    Image transitionPanel;

    [Header("Transition Values")]
    [SerializeField]
    float fadeInTime;
    [SerializeField]
    float fadeOutTime;

    public Action FadeInEnds;
    public Action FadeOutEnds;

    public void StartFadeIn()
    {
        StartCoroutine(Fader(FadeType.In, fadeInTime));
    }

    public void StartFadeOut()
    {
        StartCoroutine(Fader(FadeType.Out, fadeOutTime));
    }

    enum FadeType { In, Out}
    IEnumerator Fader(FadeType type, float duration)
    {
        float progression = duration;
        Color panelColor = transitionPanel.color;

        void FinishTransition(Color color, Action message)
        {
            transitionPanel.color = color;
            message?.Invoke();
        }

        if (!transitionPanel.gameObject.activeInHierarchy)
            transitionPanel.gameObject.SetActive(true);

        while(progression > 0)
        {
            yield return new WaitForEndOfFrame();
            progression -= Time.deltaTime;

            switch (type)
            {
                case FadeType.In:
                    panelColor.a =  1 - progression / duration;
                    break;
                case FadeType.Out:
                    panelColor.a = progression / duration;
                    break;
            }

            transitionPanel.color = panelColor;
        }

        switch (type)
        {
            case FadeType.In:
                panelColor.a = 1;
                FinishTransition(panelColor, FadeInEnds);

                break;
            case FadeType.Out:
                FinishTransition(panelColor, FadeOutEnds);
                transitionPanel.gameObject.SetActive(false);
                break;
        }
    }
}

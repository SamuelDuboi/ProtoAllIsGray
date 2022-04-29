using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using NaughtyAttributes;
using System;

public class InGameUI : MonoBehaviour
{
    [Header("Oppening")]
    [SerializeField]
    TextMeshProUGUI openningText;
    [SerializeField]
    RectTransform openningRoot;

    [Space]
    [SerializeField]
    float delayStart;
    [SerializeField]
    float delayEnd;
    [SerializeField]
    AnimationCurve openningCurve;

    public Action OpenningEnds;

    [Header("HUD")]
    [SerializeField]
    InGamePortrait[] allPortrait;
    [SerializeField]
    TextMeshProUGUI objBlock;

    public void LaunchOpenning()
    {
        int stepCount = 0;
        void ChangeText()
        {
            switch (stepCount)
            {
                case 0:
                    openningText.text = "3";
                    break;
                case 1:
                    openningText.text = "2";
                    break;
                case 2:
                    openningText.text = "1";
                    break;
                case 3:
                    openningText.text = "Fight!";
                    break;
            }

            stepCount++;
            openningText.rectTransform.localScale = Vector3.one * 1;
        }

        openningRoot.gameObject.SetActive(true);

        Sequence openningSequence = DOTween.Sequence();
        openningSequence.Append(openningText.rectTransform.DOScale(1.5f, delayStart).SetEase(openningCurve));
        openningSequence.AppendCallback(ChangeText);
        openningSequence.Append(openningText.rectTransform.DOScale(1.5f, 0.8f).SetEase(openningCurve));
        openningSequence.AppendCallback(ChangeText);
        openningSequence.Append(openningText.rectTransform.DOScale(1.5f, 0.8f).SetEase(openningCurve));
        openningSequence.AppendCallback(ChangeText);
        openningSequence.Append(openningText.rectTransform.DOScale(1.5f, 0.8f).SetEase(openningCurve));
        openningSequence.AppendCallback(ChangeText);
        openningSequence.Append(openningText.rectTransform.DOScale(1.5f, delayEnd).SetEase(openningCurve));
        openningSequence.AppendCallback(OnOpenningEnds);
        openningSequence.PlayForward();
    }

    public void OnOpenningEnds()
    {
        OpenningEnds?.Invoke();
        openningRoot.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.currentGameInstance.currentSettings.IsTimeAttack)
        {
            objBlock.text = GameManager.currentGameInstance.currentTimer.ToString();
        }
    }

    public void InitHUD()
    {
        var allPlayer = GameManager.currentGameInstance.allActivePlayer;

        if (GameManager.currentGameInstance.currentSettings.IsDeathMatch)
        {
            objBlock.text = "Eject your opponents";
        }
        else
        {
            objBlock.text = (GameManager.currentGameInstance.currentSettings.gameTimer * 60).ToString();
        }

        foreach (var player in allPlayer)
        {
            allPortrait[player.playerIndex].gameObject.SetActive(true);
            allPortrait[player.playerIndex].Init(player);
        }
    }
}

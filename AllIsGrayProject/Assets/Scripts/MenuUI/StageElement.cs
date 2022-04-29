using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using NaughtyAttributes;

public class StageElement : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI stageNameDisplay;
    [SerializeField]
    Image stagePict;
    [SerializeField]
    Image border;

    public RectTransform pointer;
    [SerializeField]
    RectTransform selectedLabel;

    [Space, Expandable]
    public StageInfos stageInfos;

    public void Init()
    {
        stageNameDisplay.text = stageInfos.stageName;
        stagePict.sprite = stageInfos.stageScreen;
        selectedLabel.gameObject.SetActive(false);
    }

    public void UpdateSelection(bool state)
    {
        selectedLabel.gameObject.SetActive(state);
    }
}

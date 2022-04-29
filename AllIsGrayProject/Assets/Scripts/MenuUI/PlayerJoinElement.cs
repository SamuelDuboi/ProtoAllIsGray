using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class PlayerJoinElement : MonoBehaviour
{
    [SerializeField]
    Image portrait;
    [SerializeField]
    TextMeshProUGUI playerName;
    [SerializeField]
    TextMeshProUGUI readyText;
    [SerializeField]
    TextMeshProUGUI waitingText;

    float animationTime;
    AnimationCurve curve;

    public void InitElement(int playerIndex, float _animationDuration, AnimationCurve _curve)
    {
        SetReady(false);

        //Get Portrait from Bank;

        animationTime = _animationDuration;
        curve = _curve;

        //transform.localScale = Vector3.one * 0f;
        //transform.DOScale(Vector3.one, animationDuration)
        //    .SetEase(curve);
    }

    public void StartAnimation()
    {
        //transform.localScale = Vector3.one * 0f;
        //transform.DOScale(Vector3.one, animationTime)
        //    .SetEase(curve);
    }

    public void SetReady(bool state)
    {
        if (state)
        {
            waitingText.gameObject.SetActive(false);
            readyText.gameObject.SetActive(true);
        }
        else
        {
            waitingText.gameObject.SetActive(true);
            readyText.gameObject.SetActive(false);
        }
    }

    //public PlayerJoinElement InstantiatePlayerElement(PlayerJoinElement template, Transform root)
    //{
    //    PlayerJoinElement createdElement = Instantiate(template, root);
    //    createdElement.gameObject.SetActive(true);
    //    return createdElement;
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGamePortrait : MonoBehaviour
{
    [SerializeField]
    Image portrait;
    [SerializeField]
    TextMeshProUGUI playerName;
    [SerializeField]
    TextMeshProUGUI stockDisplay;
    [SerializeField]
    TextMeshProUGUI shieldDisplay;
    [SerializeField]
    RectTransform stockGroup;
    [SerializeField]
    RectTransform shieldGroup;

    [Space]

    [SerializeField]
    Sprite basePortrait;
    [SerializeField]
    Sprite deadPortrait;

    PlayerHandler linkedPlayer;
    GameSettings currentSettings;

    public void Init(PlayerHandler player)
    {
        linkedPlayer = player;
        portrait.sprite = basePortrait;
        currentSettings = GameManager.currentGameInstance.currentSettings;
        player.PlayerDead += UpdateStock;
        player.PlayerBenched += Death;
        player.playerShield.DamageTaken += UpdateShield;

        if (currentSettings.IsDeathMatch)
        {
            stockDisplay.text = currentSettings.stockCount.ToString();
        }
        else
        {
            stockDisplay.text = "0";
        }
    }

    public void UpdateStock()
    {
        if (currentSettings.IsDeathMatch)
        {
            stockDisplay.text = (currentSettings.stockCount- linkedPlayer.deathCount).ToString();
        }
        else
        {
            stockDisplay.text = linkedPlayer.deathCount.ToString();
        }
    }

    public void Death()
    {
        linkedPlayer.PlayerDead -= UpdateStock;
        linkedPlayer.playerShield.DamageTaken -= UpdateShield;
        shieldGroup.gameObject.SetActive(false);
        stockGroup.gameObject.SetActive(false);

        portrait.sprite = deadPortrait;
    }

    public void UpdateShield()
    {
        shieldDisplay.text = linkedPlayer.playerShield.shieldPurcentage.text;
    }
}

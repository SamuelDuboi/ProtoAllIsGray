using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    public GameInstanceHandler currentGameInstance;
    GameSettings CurrentSetting => currentGameInstance.currentSettings;

    public PlayerMovement playerMove;
    public ShieldBehavior playerShield;
    public PlayerColorSkinHandler playerSkin;
    public PlayerInput input;
    public InputDevice device;

    public int deathCount;
    public int currentScore;
    public int playerIndex;

    public Action PlayerDead;
    public Action PlayerBenched;

    public void InitPlayer(GameInstanceHandler instance, PlayerColorBank.ColorPair skinColor, InputDevice _device)
    {
        currentGameInstance = instance;
        RespawnPlayer();
        input.SwitchCurrentControlScheme(new InputDevice[0]);
        device = _device;
        playerShield.ShieldInit();
        playerSkin.InitColor(skinColor);
    }

    public void ResetPlayer()
    {
        //Reset Weapon;
        //Reset Statut;
        //Reset Shield;
        playerShield.ShieldReset();
        //Reset Rotation & Velocity;
        playerMove.rigidbody.velocity = Vector3.zero;
        playerMove.transform.rotation = Quaternion.identity;

        //Reset Fuel;
    }

    public void RespawnPlayer()
    {
        playerMove.transform.position = currentGameInstance.FindRespawnPoint(this).position;
    }

    public void BenchPlayer()
    {
        PlayerBenched?.Invoke();
        currentGameInstance.RemovePlayer(this);
        gameObject.SetActive(false);
    }

    public void PlayerDeath()
    {
        deathCount++;
        PlayerDead?.Invoke();

        if (CurrentSetting.IsDeathMatch)
        {
            if (deathCount >= CurrentSetting.stockCount)
            {
                BenchPlayer();
            }
            else
            {
                ResetPlayer();
                RespawnPlayer();
            }
        }
    }
}

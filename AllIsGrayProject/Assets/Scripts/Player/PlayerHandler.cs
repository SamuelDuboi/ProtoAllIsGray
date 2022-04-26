using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

public class PlayerHandler : MonoBehaviour
{
    public GameInstanceHandler currentGameInstance;
    GameSettings CurrentSetting => currentGameInstance.currentSettings;

    public PlayerMovement playerMove;

    public int deathCount;
    public int currentScore;

    public Action PlayerDead;

    public void InitPlayer(GameInstanceHandler instance)
    {
        currentGameInstance = instance;
        RespawnPlayer();
        //Assign Controler;
    }

    public void ResetPlayer()
    {
        //Reset Weapon;
        //Reset Statut;
        //Reset Shield;
        //Reset Rotation & Velocity;
        //Reset Fuel;
    }

    public void RespawnPlayer()
    {
        transform.position = currentGameInstance.FindRespawnPoint(this).position;
    }

    public void PlayerDeath()
    {
        deathCount++;
        PlayerDead?.Invoke();

        if (CurrentSetting.IsDeathMatch)
        {
            if (deathCount >= CurrentSetting.stockCount)
            {
                currentGameInstance.EndSession();
            }
            else
            {
                ResetPlayer();
                RespawnPlayer();
            }
        }
    }
}

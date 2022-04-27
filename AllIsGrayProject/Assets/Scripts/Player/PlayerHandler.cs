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
        playerMove.rigidbody.velocity = Vector3.zero;
        playerMove.transform.rotation = Quaternion.identity;

        //Reset Fuel;
    }

    public void RespawnPlayer()
    {
        playerMove.transform.position = currentGameInstance.FindRespawnPoint(this).position;
    }

    public void PlayerDeath()
    {
        Debug.Log("Death");
        deathCount++;
        PlayerDead?.Invoke();

        if (CurrentSetting.IsDeathMatch)
        {
            if (deathCount >= CurrentSetting.stockCount)
            {
                currentGameInstance.EndSession();
                Debug.Log("EndSession");
            }
            else
            {
                ResetPlayer();
                RespawnPlayer();
                Debug.Log("Respawn");
            }
        }
    }
}

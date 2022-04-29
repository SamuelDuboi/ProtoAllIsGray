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
    public ShieldBehavior playerShield;
    public PlayerColorSkinHandler playerSkin;

    public int deathCount;
    public int currentScore;
    public int playerIndex;

    public Action PlayerDead;
    public Action PlayerBenched;

    public ParticleSystem psSpawner;

    public void InitPlayer(GameInstanceHandler instance, PlayerColorBank.ColorPair skinColor)
    {
        currentGameInstance = instance;
        RespawnPlayer();
        //Assign Controler;
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
        StartCoroutine(startAndStopParticleSystem());

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

    IEnumerator startAndStopParticleSystem()
    {
        psSpawner.Play();
        yield return new WaitForSeconds(2);
        psSpawner.Stop();
     }
}

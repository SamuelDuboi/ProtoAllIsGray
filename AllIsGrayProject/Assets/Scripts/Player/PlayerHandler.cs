using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PlayerHandler : MonoBehaviour
{
    public GameInstanceHandler currentGameInstance;
    GameSettings CurrentSetting => currentGameInstance.currentSettings;

    public PlayerMovement playerMove;
    public ShieldBehavior playerShield;
    public PlayerColorSkinHandler playerSkin;
    public WeaponManager weapong;
    public PlayerInput input;
    public InputDevice device;

    public int deathCount;
    public int currentScore;
    public int playerIndex;

    public Action PlayerDead;
    public Action PlayerBenched;

    public ParticleSystem psSpawner;
    public AudioSource respawnSource;
    public AudioSource deathSource;

    public TextMeshProUGUI playerTag;


    public void InitPlayer(GameInstanceHandler instance, PlayerColorBank.ColorPair skinColor, InputDevice _device, int index)
    {
        currentGameInstance = instance;
        playerIndex = index;
        RespawnPlayer();
        device = _device;
        input.enabled = false;
        playerShield.ShieldInit();
        playerSkin.InitColor(skinColor);
        playerTag.text = "P" + (index+1).ToString();
        playerTag.color = skinColor.mainColor;
    }

    public void EnablePlayer()
    {
        input.enabled = true;
        input.SwitchCurrentControlScheme(new InputDevice[] { device });
    }

    public void ResetPlayer()
    {
        weapong.KnockBack(0f);
        playerShield.ShieldReset();
        playerMove.rigidbody.velocity = Vector3.zero;
        playerMove.transform.rotation = Quaternion.identity;
    }

    public void RespawnPlayer()
    {
        playerMove.transform.position = currentGameInstance.FindRespawnPoint(this).position;
        respawnSource.Play();
        CameraShake.instance.ShakeCamera(2, 0.5f);
        GetComponentInChildren<Rumbler>().StopRumble();
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
        deathSource.Play();
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
        else
        {
            ResetPlayer();
            RespawnPlayer();
        }
    }

    IEnumerator startAndStopParticleSystem()
    {
        psSpawner.Play();
        yield return new WaitForSeconds(2);
        psSpawner.Stop();
     }
}

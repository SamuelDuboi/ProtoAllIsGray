using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New Game Settings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public enum GameModeType { DeathMatch, TimeAttack}

    public bool IsDeathMatch => gameMode == GameModeType.DeathMatch;
    public bool IsTimeAttack => gameMode == GameModeType.TimeAttack;


    public int playerAmount;
    public GameModeType gameMode;

#if UNITY_EDITOR
    [ShowIf("IsDeathMatch")]
#endif
    public int stockCount;
#if UNITY_EDITOR
    [ShowIf("IsTimeAttack")]
#endif
    public float gameTimer;

    public float weaponSpawnRate;
    public bool specialWeaponSpawn;

    public float powerupSpawnRate;
}

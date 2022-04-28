using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class GameInstanceHandler : MonoBehaviour
{
    public PlayerHandler playerPrefab;

    public GameSettings currentSettings;
    public List<PlayerHandler> allActivePlayer = new List<PlayerHandler>();
    public List<PlayerHandler> benchedPlayer = new List<PlayerHandler>();
    public Transform[] allSpawnPoints;

    public float currentTimer;

    private void Awake()
    {
        GameManager.Instance.ReferenceGameInstance(this);
    }

    private void Start()
    {
        InitSession(currentSettings);
    }

    private void Update()
    {
        if (currentSettings.IsTimeAttack)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                EndSession();
            }
        }
    }

    public void InitSession(GameSettings settings)
    {
        currentSettings = settings;
        currentTimer = settings.gameTimer;

        for (int i = 0; i < settings.playerAmount; i++)
        {
            var player = Instantiate(playerPrefab);
            allActivePlayer.Add(player);
            player.InitPlayer(this);
        }
    }

    public void StartSession()
    {

    }

    public void EndSession()
    {

    }

    public void RemovePlayer(PlayerHandler player)
    {
        allActivePlayer.Remove(player);
        benchedPlayer.Add(player);
    }

    public PlayerHandler SpawnPlayer()
    {
        var player = Instantiate(playerPrefab);
        player.InitPlayer(this);

        return player;
    }

    public Transform FindRespawnPoint(PlayerHandler playerToRespawn)
    {
        Dictionary<Transform, float> allPossiblePoint = new Dictionary<Transform, float>();
        float distanceFromPoint = 0;

        foreach (Transform spawnPoint in allSpawnPoints)
        {
            allPossiblePoint.Add(spawnPoint, Mathf.Infinity);

            foreach (PlayerHandler player in allActivePlayer)
            {
                if (player == null || player == playerToRespawn)
                    continue;
                distanceFromPoint = Vector3.Distance(spawnPoint.position, player.playerMove.transform.position);
                if (allPossiblePoint[spawnPoint] > distanceFromPoint)
                {
                    allPossiblePoint[spawnPoint] = distanceFromPoint;
                }
            }
        }

        var farestPoint = allPossiblePoint.OrderBy(p => p.Value).Last().Key;
        return farestPoint;
    }
}

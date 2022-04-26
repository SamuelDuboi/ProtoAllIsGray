using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class LookAtPointCam : MonoBehaviour
{
    public PlayerInputManager inputManager;
    public List<Transform> players;
    public Transform myTransfrom;
    public CinemachineVirtualCamera vcam;
    public float maxFov;
    public float minFov;
    public Transform[] edgeTransforme;
    private float maxDistance;
    public AnimationCurve camCurve;
    private void Start()
    {
        maxDistance = Vector3.Distance(edgeTransforme[0].position, edgeTransforme[1].position); 
    }
    void Update()
    {
        var position = Vector3.zero;
        var distances = new List<float>();
        for (int i = 0; i < players.Count; i++)
        {
            position += players[i].position;
            for (int x = i+1; x < players.Count; x++)
            {
                distances.Add(Vector3.Distance(players[x].position, players[i].position));
            }
        }
        position /= players.Count;
        distances.Sort();
        myTransfrom.position = position;
        vcam.m_Lens.FieldOfView = Mathf.Clamp( camCurve.Evaluate( distances[distances.Count-1]/ maxDistance)*maxFov, minFov, maxFov);

    }
    public void AddPlayer(PlayerInput player)
    {
        players.Add(player.gameObject.transform);
    }
}

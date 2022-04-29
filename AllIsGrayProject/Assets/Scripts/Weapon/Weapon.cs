using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof(AudioSource))]

public class Weapon : ThrowObject
{
    public GameObject projectile;
    public float knockBackForce;
    public float projectileSpeed = 2;
    protected GameObject instantiatedProjectile;
    public int numberOfBullets = 5;
    public float coolDown;
    protected bool isOnCd;
    private HomingMissileBehavior homing;
    private List<PlayerMovement> players = new List<PlayerMovement>();
   [HideInInspector] public float angleMove;
    PlayerMovement lockedPlayer;
    public Transform target;
    public List<AudioClip> clips;
    private void Start()
    {
        homing = projectile.GetComponent<HomingMissileBehavior>();
       var type = projectile.GetComponent<ThrowObject>();
       if (type as PlasmaBehavior || homing)
       {
            numberOfBullets = Mathf.FloorToInt(numberOfBullets * 0.5f);
        }
       else if (type as GammaBehavior)
            numberOfBullets = Mathf.FloorToInt( numberOfBullets * 0.75f);

        //Set color here
        if(homing)
        target.GetComponent<SpriteRenderer>().color = Color.red;
    }
    /// <summary>
    /// return false if the weapon need to be thrown
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="position"></param>
    /// <param name="force"></param>
    /// <returns></returns>
    public virtual bool Fire(Vector3 direction, Vector3 position, out float force)
    {
        force = 0.001f;
        if (!isOnCd)
        {
            LunchCD();
        }
        else
            return true;
        if (numberOfBullets == 0)
        {
            force = 0;
            return false;
        }
        instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
        //instantiatedProjectile.GetComponent<Movable>().
        instantiatedProjectile.GetComponent<ThrowObject>().Throw(direction, projectileSpeed);
        source.clip = clips[Random.Range(0, clips.Count )];
        source.Play();
        numberOfBullets--;
        if (homing && lockedPlayer)
            instantiatedProjectile.GetComponent<HomingMissileBehavior>().target = lockedPlayer.transform;
        force = knockBackForce;
        return true;
    }
    protected  IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        isOnCd = false;
    }
    public virtual float Release()
    {
        return 0;
    }
    public void LunchCD()
    {
        isOnCd = true;
        StartCoroutine(CoolDown());
    }
    private void Update()
    {
        if (!homing || !lockedPlayer)
            return;
        target.transform.position = lockedPlayer.transform.position;
        target.transform.position += Vector3.back * 0.5f;
    }

    public void HomingDirection(Vector3 direction)
    {

        if (!homing)
            return;
        if (direction == Vector3.zero)
            return;
        if (!target.gameObject.activeSelf)
            target.gameObject.SetActive(true);
        if(players.Count == 0)
        {
            foreach (var player in GameManager.currentGameInstance.allActivePlayer)
            {
                if (player != GetComponentInParent<PlayerMovement>())
                    players.Add(player.playerMove);
            }
        }
        var angle = 360.0f;
            var firstAngle = Mathf.Atan2(direction.y, direction.x);
        foreach (var player in players)
        {
            var secondAngle = Mathf.Atan2(player.transform.position.y- transform.position.y, player.transform.position.x - transform.position.x);
            angleMove =Mathf.Abs( firstAngle - secondAngle);
            if (angleMove < angle)
            {
                angle = angleMove;
                lockedPlayer = player;
            }
        }

    }
}

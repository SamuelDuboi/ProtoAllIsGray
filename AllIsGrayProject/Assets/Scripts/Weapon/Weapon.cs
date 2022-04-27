using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float angleMove;
    private void Start()
    {
        homing = projectile.GetComponent<HomingMissileBehavior>();
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
        force = 0.1f;
        if (!isOnCd)
        {
            isOnCd = true;
            StartCoroutine(CoolDown());
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
        numberOfBullets--;

        force = knockBackForce;
        return true;
    }
    protected  IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        isOnCd = false;
    }
    public virtual void Release()
    {

    }

    public void HomingDirection(Vector3 direction)
    {
        if (!homing)
            return;

        if(players.Count == 0)
        {
            foreach (var player in GameManager.currentGameInstance.allPlayer)
            {
                if (player != GetComponentInParent<PlayerMovement>())
                    players.Add(player.playerMove);
            }
        }
        var angle = 360.0f;
        foreach (var player in players)
        {
            angleMove = Mathf.Abs(Vector3.Angle(direction + transform.position, player.transform.position));
            if (angleMove < angle)
            {
                angle = angleMove;
            }
        }
    }
}

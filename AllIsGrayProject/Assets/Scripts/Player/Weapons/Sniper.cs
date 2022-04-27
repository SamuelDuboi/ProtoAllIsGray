using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public float isCharging;
    public override bool Fire(Vector3 direction, Vector3 position, out float force)
    {

        if (isCharging!= 0)
        {

        }


        /*
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
        return true;*/
    }
}

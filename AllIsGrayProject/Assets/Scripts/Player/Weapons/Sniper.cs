using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public float isCharging;
    public float chargeTime;
    public float accelerationDecharge;

    public bool shoot;

    public Vector3 directionSnip;
    public Vector3 positionSnip;
    public float forceSnip;

    public bool decharging;
    public override bool Fire(Vector3 direction, Vector3 position, out float force)
    {
        directionSnip = direction;
        positionSnip = position;


        isCharging += Time.deltaTime;
        if (isCharging!= 0 && isCharging<chargeTime)
        {
            isCharging += Time.deltaTime;
            Debug.Log(isCharging);
        }
        if (decharging)
        {
            StopCoroutine(Decharge());
            decharging = false;
        }

        force = 0.001f;
        /*
        if (!isOnCd)
        {
            isOnCd = true;
            StartCoroutine(CoolDown());
        }
        else
            return true;*/
        if (numberOfBullets == 0)
        {
            force = 0;
            return false;
        }
        if (shoot)
        {
            shoot = false;
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
            //instantiatedProjectile.GetComponent<Movable>().
            instantiatedProjectile.GetComponent<ThrowObject>().Throw(direction, projectileSpeed);
            numberOfBullets--;

            force = knockBackForce;
        }
        return true;
    }
    public override void Release()
    {
        if( isCharging >= chargeTime)
        {
            shoot = true;
            float rien;
            Fire(directionSnip, positionSnip, out rien);
            isCharging = 0;
            //METTRE FEEDBACK POUR CHARGE DU SNIPER
        }
        else
        {
            StartCoroutine(Decharge());
        }
    }
    IEnumerator Decharge()
    {
        decharging = true;

            while (isCharging > 0)
            {
                isCharging -= accelerationDecharge * Time.deltaTime;
                Debug.Log(isCharging);
            }
        isCharging = 0f;
        decharging = false;
        yield return null;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int numOfProjectile;
    public float modDir;
    public float spreadRate;
    public override bool Fire(Vector3 direction, Vector3 position, out float force)
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
        for(int i=0; i<numOfProjectile; i++)
        {
            modDir = Mathf.FloorToInt(numOfProjectile / 2);
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
            //instantiatedProjectile.GetComponent<Movable>().
            instantiatedProjectile.GetComponent<ThrowObject>().Throw(new Vector3(Mathf.Cos(Mathf.Atan2( direction.y,direction.x) + (0 + (i - modDir)) * spreadRate), Mathf.Sin(Mathf.Atan2(direction.y, direction.x) + (0 + (i - modDir)) * spreadRate),0), projectileSpeed); //SPREAD MAIS PAS SUR QUE CA FONCTION à 360° 
        }
        numberOfBullets--;
        force = knockBackForce;
        return true;
    }
}

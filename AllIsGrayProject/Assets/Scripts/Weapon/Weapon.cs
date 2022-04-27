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
}

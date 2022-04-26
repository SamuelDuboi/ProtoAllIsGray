using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float knockBackForce;
    public float projectileSpeed = 2;
    protected GameObject instantiatedProjectile;
    public int numberOfBullets = 5;
    public float Fire(Vector3 direction, Vector3 position)
    {
        if (numberOfBullets == 0)
            return 0;
        instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = direction* projectileSpeed;
        numberOfBullets--;
        return knockBackForce;
    }
}

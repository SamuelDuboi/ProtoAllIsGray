using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public float knockBackForce;
    public float projectileSpeed = 2;
    protected GameObject instantiatedProjectile;

    public float Fire(Vector3 direction, Vector3 position)
    {
        instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = direction* projectileSpeed;

        return knockBackForce;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    public GameObject Weapon;
    public float spawnCD;
    private GameObject currentWeapon;

    private void Start()
    {
        currentWeapon = Instantiate(Weapon, transform);
    }
    public Weapon Collect()
    {
        StartCoroutine(WaitToSpawn());
        if(currentWeapon)
           return currentWeapon.GetComponent<Weapon>();
        return null;
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(spawnCD);
        currentWeapon = Instantiate(Weapon, transform);
    }
}

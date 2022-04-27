using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{

    public float maxShieldAmount=100;
    public float currentShieldAmount;
    private float propulsionForce = 1;
    public Rigidbody rgb;

    public void Start()
    {
        currentShieldAmount = maxShieldAmount;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var colObject =collision.gameObject.GetComponent<Movable>();
        if (colObject)
        {
            currentShieldAmount -= colObject.damageOnPlayer;
            GetPropulsionForce();
            var direction = colObject.transform.position - transform.position;
            direction.Normalize();
            rgb.AddForce(-direction * propulsionForce);
            ShowShield();
        }
    }

    public float GetPropulsionForce()
    {
        var propForce = 1 - currentShieldAmount / maxShieldAmount;
        propulsionForce = propForce/ 0.05f +1;
        return propulsionForce;
    }

    public void ShowShield()
    {
        // here to displayDamageOnPlayer
    }
}

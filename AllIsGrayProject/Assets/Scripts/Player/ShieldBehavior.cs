using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldBehavior : MonoBehaviour
{

    public float maxShieldAmount=100;
    public float currentShieldAmount;
    private float propulsionForce = 1;
    public Rigidbody rgb;
    public bool isInvicible;
    public TextMeshProUGUI shieldPurcentage;
    public void Start()
    {
        currentShieldAmount = maxShieldAmount;
    }
    private void OnCollisionEnter(Collision collision)
    {
        var colObject =collision.gameObject.GetComponent<Movable>();
        
        if (colObject)
        {
            if (isInvicible)
            {
                Destroy(collision.gameObject);
                return;
            }
            currentShieldAmount -= colObject.damageOnPlayer;
            var propForce = currentShieldAmount / maxShieldAmount;
            shieldPurcentage.text = propForce*100 + " %";
            GetPropulsionForce(1-propForce);
            var direction = colObject.transform.position - transform.position;
            direction.Normalize();
            rgb.AddForce(-direction * propulsionForce, ForceMode.Impulse);
            ShowShield();
            if (colObject as ThrowObject)
                Destroy(colObject.gameObject);
        }
    }

    public float GetPropulsionForce()
    {
        var propForce = 1 - currentShieldAmount / maxShieldAmount;
        propulsionForce = propForce/ 0.05f +1;
        return propulsionForce;
    }
    public float GetPropulsionForce( float propForce)
    {
        propulsionForce = propForce / 0.05f + 1;
        return propulsionForce;
    }
    public void ShowShield()
    {
        // here to displayDamageOnPlayer
    }

    public void SetInvicible(bool value)
    {
        isInvicible = value;
    }
}

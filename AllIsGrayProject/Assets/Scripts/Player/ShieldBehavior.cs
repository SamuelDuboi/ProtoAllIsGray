using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{

    public float maxShieldAmount = 100;
    public float currentShieldAmount;
    private float propulsionForce = 1;
    public Rigidbody rgb;
    public bool isInvincible;
    public float invincibilityDuration;

    public void ShieldInit()
    {
        currentShieldAmount = maxShieldAmount;
    }

    public void ShieldReset()
    {
        StopAllCoroutines();
        currentShieldAmount = maxShieldAmount;
        StartCoroutine(StartInvicibility());
    }

    private void OnCollisionEnter(Collision collision)
    {
        var colObject = collision.gameObject.GetComponent<Movable>();

        if (colObject)
        {
            if (isInvincible)
            {
                Destroy(collision.gameObject);
                return;
            }
            currentShieldAmount -= colObject.damageOnPlayer;
            GetPropulsionForce();
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
        propulsionForce = propForce / 0.05f + 1;
        return propulsionForce;
    }

    public void ShowShield()
    {
        // here to displayDamageOnPlayer
    }

    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    IEnumerator StartInvicibility()
    {
        float timer = invincibilityDuration;
        SetInvincible(true);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SetInvincible(false);
    }
}

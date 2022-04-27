using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShieldBehavior : MonoBehaviour
{

    public float maxShieldAmount = 100;
    public float currentShieldAmount;
    private float propulsionForce = 1;
    public Rigidbody rgb;
    public bool isInvincible;
    public TextMeshProUGUI shieldPurcentage;

    public GameObject Shield;
    private Renderer rendererShield;
    private MaterialPropertyBlock propBlock;

    public float invincibilityDuration;
    public void ShieldInit()
    {
        currentShieldAmount = 0;
        rendererShield = Shield.GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
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
            TakeDamage(colObject);
            var throwObject = colObject as ThrowObject;
            if (throwObject && throwObject.playerDestroy)
                Destroy(colObject.gameObject);
        }
    }
    public void TakeDamage(Movable colObject)
    {
        currentShieldAmount += colObject.damageOnPlayer;
        var propForce = currentShieldAmount / maxShieldAmount;
        shieldPurcentage.text = propForce * 100 + " %";
        GetPropulsionForce( propForce);
        var direction = colObject.transform.position - transform.position;
        direction.Normalize();
        rgb.AddForce(-direction * propulsionForce, ForceMode.Impulse);
        ShowShield();
    }
    public void TakeDamage(float value, Movable colObject)
    {
        currentShieldAmount += value;
        var propForce = currentShieldAmount / maxShieldAmount;
        shieldPurcentage.text = propForce * 100 + " %";
        GetPropulsionForce( propForce);
        var direction = colObject.transform.position - transform.position;
        direction.Normalize();
        rgb.AddForce(-direction * propulsionForce, ForceMode.Impulse);
        ShowShield();
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
        float currentShieldAmountShader = currentShieldAmount * 0.01f;

        rendererShield.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_Damaged", currentShieldAmountShader);
        if (currentShieldAmountShader > 1)
        {
            propBlock.SetFloat("_Over100", 1);
        }
        else { propBlock.SetFloat("_Over100", 0); }
        rendererShield.SetPropertyBlock(propBlock);
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

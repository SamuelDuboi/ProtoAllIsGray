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

    public GameObject[] Shield= new GameObject[2];
    private Renderer[] rendererShield = new Renderer[2];
    private MaterialPropertyBlock[] propBlock = new MaterialPropertyBlock[2];

    public float invincibilityDuration;
    public void ShieldInit()
    {
        currentShieldAmount = 0;
        rendererShield[0] = Shield[0].GetComponent<Renderer>();
        propBlock[0] = new MaterialPropertyBlock();
        rendererShield[1] = Shield[1].GetComponent<Renderer>();
        propBlock[1] = new MaterialPropertyBlock();
    }

    public void ShieldReset()
    {
        StopAllCoroutines();
        currentShieldAmount = 0;
        shieldPurcentage.text = "0 %";
        ShowShield();
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
        float currentShieldAmountShader = currentShieldAmount / maxShieldAmount;

        rendererShield[0].GetPropertyBlock(propBlock[0]);
        propBlock[0].SetFloat("_Damaged", currentShieldAmountShader);
        if (currentShieldAmountShader > 1)
        {
            propBlock[0].SetFloat("_Over100", 1);
        }
        else { propBlock[0].SetFloat("_Over100", 0); }
        rendererShield[0].SetPropertyBlock(propBlock[0]);

        rendererShield[1].GetPropertyBlock(propBlock[1]);
        propBlock[1].SetFloat("_Damaged", currentShieldAmountShader);
        if (currentShieldAmountShader > 1)
        {
            propBlock[1].SetFloat("_Over100", 1);
        }
        else { propBlock[1].SetFloat("_Over100", 0); }
        rendererShield[1].SetPropertyBlock(propBlock[1]);
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

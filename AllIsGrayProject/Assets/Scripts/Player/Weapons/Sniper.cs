using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public float isCharging;
    public float chargeTime;
    public float accelerationDecharge;

    public bool shoot;

    public Vector3 directionSnip;
    public Vector3 positionSnip;
    public float forceSnip;

    public bool decharging;

    public GameObject sphere;
    public Color finalColor;
    private float sizeSphere;
    private MaterialPropertyBlock propBlock;
    protected virtual void Start()
    {
        propBlock = new MaterialPropertyBlock();
    }
    public override bool Fire(Vector3 direction, Vector3 position, out float force)
    {
        directionSnip = direction;
        positionSnip = position;
        if (!rumbler)
            rumbler = GetComponentInParent<Rumbler>();

        Renderer sphereRenderer = sphere.GetComponent<Renderer>();

        isCharging += Time.deltaTime;
        if (isCharging!= 0 && isCharging<chargeTime)
        {
            sphere.SetActive(true);
            rumbler.RumbleConstant(0.1f*isCharging, 0.5f*isCharging, 0.1f);
            isCharging += Time.deltaTime;
            sizeSphere += (Time.deltaTime / chargeTime)*2; // à remultiply par la size voulue
            sphere.transform.localScale = new Vector3(sizeSphere, sizeSphere, sizeSphere);

            if (isCharging < chargeTime)
            {
                sphereRenderer.GetPropertyBlock(propBlock);
                propBlock.SetColor("_color", new Color(1,1,1));
                propBlock.SetFloat("_offset", 0.5f);
                sphereRenderer.SetPropertyBlock(propBlock);
            }

            if (isCharging >= chargeTime)
            {
                sphereRenderer.GetPropertyBlock(propBlock);
                propBlock.SetColor("_color", finalColor);
                propBlock.SetFloat("_offset", 0f);
                sphereRenderer.SetPropertyBlock(propBlock);
            }


        }
        else if(isCharging>=chargeTime)
            rumbler.RumbleConstant(0.1f * isCharging, 0.5f * isCharging, 0.1f);
        if (decharging)
        {
            StopCoroutine(Decharge());
            decharging = false;
        }

        force = 0.001f;
        /*
        if (!isOnCd)
        {
            isOnCd = true;
            StartCoroutine(CoolDown());
        }
        else
            return true;*/
        if (numberOfBullets == 0)
        {
            force = 0;
            return false;
        }
        if (shoot)
        {
            shoot = false;
            instantiatedProjectile = Instantiate(projectile, position, Quaternion.identity);
            //instantiatedProjectile.GetComponent<Movable>().
            instantiatedProjectile.GetComponent<ThrowObject>().Throw(direction, projectileSpeed);
            instantiatedProjectile.GetComponent<ThrowObject>().damageOnPlayer +=10;
            
            numberOfBullets--;

            force = knockBackForce;
        }
        return true;
    }
    public override float Release()
    {
        if( isCharging >= chargeTime)
        {
            shoot = true;
            float rien;
            Fire(directionSnip, positionSnip, out rien);
            isCharging = 0;

            //METTRE FEEDBACK POUR CHARGE DU SNIPER
            sphere.SetActive(false);

            return knockBackForce;
        }
        else
        {
            StartCoroutine(Decharge());
            return 0.01f;
        }
    }
    IEnumerator Decharge()
    {
        decharging = true;

            while (isCharging > 0)
            {
                isCharging -= accelerationDecharge * Time.deltaTime;
                Debug.Log(isCharging);
            }
        isCharging = 0f;
        decharging = false;
        yield return null;

    }
}

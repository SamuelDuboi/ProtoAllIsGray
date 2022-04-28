using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaBehavior : ThrowObject
{
    Collider passedThrough;

    public GameObject clouds;
    public GameObject clouds2;
    private MaterialPropertyBlock propBlock;
    private Renderer bulletRenderer;

    private void Start()
    {
        bulletRenderer = GetComponent<Renderer>();
        clouds.SetActive(true);
        clouds2.SetActive(true);
        propBlock = new MaterialPropertyBlock();

        bulletRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_noiseOpacity", 1);
        bulletRenderer.SetPropertyBlock(propBlock);
    }

    public override void Throw(Vector3 direction, float force)
    {
        throwForce = force;
        myDirection = direction;
        rgb.velocity = myDirection * throwForce;
        rgb.isKinematic = false;
        GetComponent<Collider>().isTrigger = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<ShieldBehavior>();
        if (player)
        {
            player.TakeDamage(damageOnPlayer, this);
            Destroy(gameObject);
        }
        else
            passedThrough = other;
    }
    private void OnTriggerExit(Collider other)
    {
        if(passedThrough == other)
        {
            GetComponent<Collider>().isTrigger = false;

            bulletRenderer.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_noiseOpacity", 0.5f);
            bulletRenderer.SetPropertyBlock(propBlock);

            clouds.SetActive(false);
            clouds2.SetActive(false);
        }
    }
}

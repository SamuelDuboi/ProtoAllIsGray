using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaBehavior : ThrowObject
{
    Collider passedThrough;
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
        }
    }
}

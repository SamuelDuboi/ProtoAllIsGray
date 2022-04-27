using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : Movable
{
    [Header("ThrowObject")]
    public BulletEffect myEffect;
    public Rigidbody rgb;
    public float throwForce;
    private Vector3 myDirection;
    private void OnCollisionEnter(Collision collision)
    {
       // myEffect.ApplyEffectOnCollision();
    }
    public virtual void Throw(Vector3 direction)
    {
        myDirection = direction;
        Throw();
    }
    public virtual void Throw(Vector3 direction, float force)
    {
        throwForce = force;
        myDirection = direction;
        Throw();
    }
    private void Throw()
    {
        rgb.velocity = myDirection * throwForce;
        rgb.isKinematic = false;
        GetComponent<Collider>().isTrigger = false;
      //  myEffect.ApplyEffectOnThrow();
    }
}

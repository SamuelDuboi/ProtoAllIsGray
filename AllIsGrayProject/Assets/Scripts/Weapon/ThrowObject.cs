using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof( AudioSource))]
public class ThrowObject : Movable
{
    [Header("ThrowObject")]
    public BulletEffect myEffect;
    public Rigidbody rgb;
    public float throwForce;
    protected Vector3 myDirection;
    public bool playerDestroy = true;
    public List<AudioClip> clipsImpact;
    protected AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        // myEffect.ApplyEffectOnCollision();
        OnCollision(collision);
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

    protected virtual void OnCollision(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            source.clip = clipsImpact[Random.Range(0, clipsImpact.Count)];
            source.Play();
        }
        StartCoroutine(WaitToDie());
    }
    protected IEnumerator WaitToDie(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
    protected IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazCylinder : ThrowObject
{
    public float gazSpeed;
    private bool doOnce;
    private bool canTakeDamage;
    protected override void OnCollision(Collision collision)
    {
        if (rgb.isKinematic)
        {
            rgb.isKinematic = false;
            StartCoroutine(WaitToTakeDamage());
        }
        else
        {
            if (!canTakeDamage)
                return;
            if (!doOnce)
            {

                if(collision.gameObject.layer == 7  )
                {
                    doOnce = true;
                    rgb.AddForce((collision.transform.position - transform.position).normalized * gazSpeed, ForceMode.Impulse);
                }
            }
            else
            {
                StartCoroutine(WaitToDie());
            }
        }
    }
    IEnumerator WaitToTakeDamage()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
        StopAllCoroutines();
    }
}

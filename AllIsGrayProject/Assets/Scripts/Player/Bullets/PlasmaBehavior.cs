using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBehavior : ThrowObject
{
    public float range;
    public float maxMovement = 3;
    public float minMovemnt= 1;
    public GameObject particules;
    public MeshRenderer myMesh;
    public Collider myCollider;
    protected override void OnCollision(Collision collision)
    {
        var hit = Physics.OverlapSphere(transform.position, range);
        particules.transform.localScale = Vector3.one*range;
        particules.SetActive(true);
        foreach (var throwObejct in hit)
        {
            if (!throwObejct.GetComponent<ThrowObject>())
                continue;
            if (throwObejct.gameObject == gameObject)
                continue;
            var player = throwObejct.GetComponent<ShieldBehavior>();
            if (player)
            {
                player.TakeDamage(damageOnPlayer, this);
                continue;
            }
            throwObejct.GetComponent<ThrowObject>().Throw((throwObejct.transform.position - transform.position).normalized, Mathf.Lerp(minMovemnt, maxMovement, Vector3.Distance(throwObejct.transform.position, transform.position) / range));

        }
        myMesh.enabled = false;
        myCollider.enabled = false;
        StartCoroutine(WaitToDie(1f));
    }
}

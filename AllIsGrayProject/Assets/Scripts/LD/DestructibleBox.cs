using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : ThrowObject
{
    public int life = 2;
    protected override void OnCollision(Collision collision)
    {
        if (collision.gameObject.layer != 7)
            return;
        life--;
        if (life == 0)
            StartCoroutine(WaitToDie());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileBehavior : ThrowObject
{
    public Transform target;

    private void Update()
    {
        if(target)
        rgb.AddForce(target.position - transform.position);  
    }
}

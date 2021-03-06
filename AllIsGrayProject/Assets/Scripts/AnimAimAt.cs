using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAimAt : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(direction);

        float eulerY = lookRot.eulerAngles.x;
        transform.LookAt(target);
    }
}

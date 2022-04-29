using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float propulsionForce = 1f;
    private void OnTriggerStay(Collider other)
    {
        var rgb = other.GetComponent<Rigidbody>();
        if (rgb)
            rgb.AddForce(new Vector3(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z), 0).normalized * propulsionForce, ForceMode.Acceleration);
    }
}

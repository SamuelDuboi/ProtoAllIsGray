using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AreaEffect : MonoBehaviour
{
    public float propulsionForce = 1f;
    private AudioSource source;
    private bool cantPlaySound;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        var rgb = other.GetComponent<Rigidbody>();
        if (rgb)
        {
            rgb.AddForce(new Vector3(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z), 0).normalized * propulsionForce, ForceMode.Acceleration);
            if (!cantPlaySound)
                StartCoroutine(WaitForSound());
        }
    }
    IEnumerator WaitForSound()
    {
        cantPlaySound = true;
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        cantPlaySound = false;
    }
}

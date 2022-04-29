using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Button : MonoBehaviour
{
    public Door[] doors;
#if UNITY_EDITOR
    public float size = 10;
    public Color color = Color.green;
#endif
    private AudioSource source;
    public AudioClip open;
    public AudioClip close;
    private bool isOn;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Movable>())
        {
            foreach (var door in doors)
                door.Perform();
            if (isOn)
                source.clip = close;
            else
                source.clip = open;
            source.Play();
        }
    }
}

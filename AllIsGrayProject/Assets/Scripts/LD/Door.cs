using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public bool needKey;
    public Transform openPos;
    public float timeToOpen;
    public float timeStayOpen;
    public float timeStayClose;
    private Vector3 initPos;
    private Vector3 endPos;
    public bool isClose;
    public float initTimeStart;
    private AudioSource source;
    public AudioClip openSound;
    public AudioClip closeSound;
    public GameObject light;

    private IEnumerator Start()
    {
        source = GetComponent<AudioSource>();
        endPos = openPos.transform.position;
        initPos = transform.position;
        if (!needKey)
        {
            yield return new WaitForSeconds(timeStayClose+initTimeStart);
            StartCoroutine(Open());
        }
    }

    public void Perform()
    {
        if (isClose)
            OpenDoore();
        else
            CloseDoor();
        isClose = !isClose;
        source.Play();
    }
    public void OpenDoore()
    {
        source.clip = openSound;
        StartCoroutine(Open());
        StopCoroutine(Close());
    }
    public void CloseDoor()
    {
        source.clip = closeSound;

        StartCoroutine(Close());
        StopCoroutine(Open());
    }

    IEnumerator Open()
    {
        for (float i = 0; i < timeToOpen; i+= Time.deltaTime)
        {
            transform.position = LerpVector(initPos, endPos, i / timeToOpen);
            yield return new WaitForEndOfFrame();
        }
        if (!needKey)
        {
            if (light)
            {
                yield return new WaitForSeconds(timeStayOpen - 2);
                light.SetActive(true);
                yield return new WaitForSeconds(2);
                light.SetActive(false);

            }
            else
                yield return new WaitForSeconds(timeStayOpen);

            StartCoroutine(Close());
        }
    }

    IEnumerator Close() 
    {
        for (float i = 0; i < timeToOpen; i += Time.deltaTime)
        {
            transform.position = LerpVector(endPos,initPos, i / timeToOpen);
            yield return new WaitForEndOfFrame();
        }
        if (!needKey)
        {
            if (light)
            {
                yield return new WaitForSeconds(timeStayClose - 2);
                light.SetActive(true);
                yield return new WaitForSeconds(2);
                light.SetActive(false);

            }
            else
                yield return new WaitForSeconds(timeStayClose);
            StartCoroutine(Open());
        }
    }

    private Vector3 LerpVector(Vector3 startpos, Vector3 endPOs, float time)
    {
        return new Vector3(Mathf.Lerp(startpos.x, endPOs.x, time), Mathf.Lerp(startpos.y, endPOs.y, time), Mathf.Lerp(startpos.z, endPOs.z, time));
    }
}

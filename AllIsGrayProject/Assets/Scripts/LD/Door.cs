using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private IEnumerator Start()
    {
        endPos = openPos.transform.position;
        initPos = transform.position;
        if (!needKey)
        {
            yield return new WaitForSeconds(timeStayClose);
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
    }
    public void OpenDoore()
    {

        StartCoroutine(Open());
        StopCoroutine(Close());
    }
    public void CloseDoor()
    {
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
            yield return new WaitForSeconds(timeStayClose);
            StartCoroutine(Open());
        }
    }

    private Vector3 LerpVector(Vector3 startpos, Vector3 endPOs, float time)
    {
        return new Vector3(Mathf.Lerp(startpos.x, endPOs.x, time), Mathf.Lerp(startpos.y, endPOs.y, time), Mathf.Lerp(startpos.z, endPOs.z, time));
    }
}

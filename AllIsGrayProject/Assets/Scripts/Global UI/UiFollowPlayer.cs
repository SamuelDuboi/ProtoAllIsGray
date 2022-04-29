using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform thisTransform;

    void Update()
    {
        thisTransform.position = playerTransform.position;   
    }
}

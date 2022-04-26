using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPoint : MonoBehaviour
{
    Vector2 rotation;
    bool IsRotating;
    [Range(0, 0.5f)]
    public float rotationTreshold;
    public Transform playerTransform;
    public void RotationWeapon(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
        IsRotating = Mathf.Abs(rotation.magnitude) > rotationTreshold;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsRotating)
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg*Mathf.Atan2(rotation.y, rotation.x));

        transform.position = playerTransform.position;
    }
}

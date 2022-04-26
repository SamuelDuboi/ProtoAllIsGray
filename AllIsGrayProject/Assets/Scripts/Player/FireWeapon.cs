using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireWeapon : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public Weapon myWeapon;
    public Transform followPointTransform;
    private Vector3 direction;
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        direction = followPointTransform.position - transform.position;
        direction.Normalize();
       KnockBack( myWeapon.Fire(direction, followPointTransform.position));
    }
    private void KnockBack(float knockBackForce)
    {
        myRigidbody.AddForce(-direction * knockBackForce, ForceMode.Impulse);
    }
}

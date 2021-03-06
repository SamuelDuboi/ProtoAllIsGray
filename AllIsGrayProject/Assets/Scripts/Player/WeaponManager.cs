using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public Rigidbody myRigidbody;
    public Weapon myWeapon;
    public Weapon basicWeapon;
    public Transform followPointTransform;
    private Vector3 direction;
    public float throwWeaponForce=5;
    public Transform weaponHandler;
    private bool isHold;
    private Vector2 rotation;
    public void RotationWeapon(InputAction.CallbackContext context)
    {
        if (!myWeapon)
            return;
        rotation = context.ReadValue<Vector2>();
        myWeapon.HomingDirection(rotation);
    }
    private void Update()
    {
        if (isHold)
        {
            direction = followPointTransform.position - transform.position;
            direction.Normalize();
            var isDrop = false;
            var knockBackForce = 0.0f;
            if (myWeapon)
            {
                isDrop = myWeapon.Fire(direction, followPointTransform.position, out knockBackForce);
                KnockBack(knockBackForce);
            }
            else
            {
                isDrop = basicWeapon.Fire(direction, followPointTransform.position, out knockBackForce);
                KnockBack(knockBackForce);
            }
            if (isDrop)
                basicWeapon.gameObject.SetActive(true);
        }
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
            isHold = true;
        if (context.canceled)
        {
            isHold = false;
            if (myWeapon)
                KnockBack( myWeapon.Release());
        }
    }
    private void KnockBack(float knockBackForce)
    {
        if (knockBackForce == 0)
        {
            if (!myWeapon)
                return ;
            myWeapon.transform.SetParent(null);
            myWeapon.transform.position = followPointTransform.position;
            myWeapon.GetComponent<Rigidbody>().velocity=direction * throwWeaponForce;
            myWeapon.Throw(direction,throwWeaponForce);
            myWeapon.GetComponent<Collider>().enabled = true;
            myWeapon.tag = "Untagged";
            basicWeapon.LunchCD();
            myWeapon = null;
            return ;
        }
        if(knockBackForce >0.1f)
        myRigidbody.AddForce(-direction * knockBackForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "WeaponSpawner")
        {
            myWeapon = other.GetComponent<WeaponSpawner>().Collect();
            if (myWeapon == null)
                return;
            basicWeapon.gameObject.SetActive( false);
            myWeapon.GetComponent<Collider>().enabled = false;
            myWeapon.transform.parent =weaponHandler;
            myWeapon.GetComponent<Rigidbody>().isKinematic = true;
            myWeapon.transform.position = basicWeapon.transform.position;
            myWeapon.transform.localRotation = basicWeapon.transform.localRotation;
            

        }
        else if (other.tag == "Weapon")
        {
            basicWeapon.gameObject.SetActive(false);
            direction = followPointTransform.position - transform.position;
            direction.Normalize();
            KnockBack(0);
            other.GetComponent<Collider>().enabled = false;
            myWeapon = other.GetComponent<Weapon>();
            myWeapon.transform.parent = weaponHandler;
            myWeapon.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = basicWeapon.transform.position;
            myWeapon.transform.localRotation = basicWeapon.transform.localRotation;


        }
    }
}

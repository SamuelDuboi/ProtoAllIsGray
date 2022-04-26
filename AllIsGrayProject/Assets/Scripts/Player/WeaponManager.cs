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
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        direction = followPointTransform.position - transform.position;
        direction.Normalize();
        var isDrop = false;
        if(myWeapon)
            isDrop= KnockBack( myWeapon.Fire(direction, followPointTransform.position));
        else
            isDrop= KnockBack(basicWeapon.Fire(direction, followPointTransform.position));
        if (isDrop)
            basicWeapon.gameObject.SetActive(true);
    }
    /// <summary>
    /// return if the wepon is dropped
    /// </summary>
    /// <param name="knockBackForce"></param>
    /// <returns></returns>
    private bool KnockBack(float knockBackForce)
    {
        if (knockBackForce == 0)
        {
            if (!myWeapon)
                return true;
            myWeapon.GetComponent<Rigidbody>().isKinematic = false;
            myWeapon.GetComponent<Rigidbody>().velocity=direction * throwWeaponForce;
            myWeapon.GetComponent<Collider>().enabled = true;
            myWeapon.tag = "Untagged";
            myWeapon.transform.SetParent(null);
            myWeapon.transform.position = basicWeapon.transform.position;
            myWeapon = null;
            return true;
        }
        myRigidbody.AddForce(-direction * knockBackForce, ForceMode.Impulse);
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            basicWeapon.gameObject.SetActive( false);
            direction = followPointTransform.position - transform.position;
            direction.Normalize();
            KnockBack(0);
            other.GetComponent<Collider>().enabled = false;
            myWeapon = other.GetComponent<Weapon>();
            myWeapon.transform.parent =weaponHandler;
            myWeapon.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = basicWeapon.transform.position;

        }
    }
}

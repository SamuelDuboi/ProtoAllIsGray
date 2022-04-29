using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FlipRenderer : MonoBehaviour
{
    public Transform myTransform;
    public bool isRight;
    private Vector2 rotation;
    public void RotationWeapon(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
        if (rotation.x < 0 && isRight)
        {
            myTransform.localRotation = Quaternion.Euler(0, -90, 0);
            isRight = false;
        }
        else if(rotation.x>0 && !isRight)
        {
            myTransform.localRotation = Quaternion.Euler(0, 90, 0);
            isRight = true;
        }
      
    }
}

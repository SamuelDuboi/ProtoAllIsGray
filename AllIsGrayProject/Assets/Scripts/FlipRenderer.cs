using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FlipRenderer : MonoBehaviour
{
    public Transform myTransform;
    private bool isRight;
    private Vector2 rotation;
    public void RotationWeapon(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
        if (rotation.x < 0 && isRight)
        {
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
            isRight = false;
        }
        else if(rotation.x>0 && !isRight)
        {
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
            isRight = true;
        }
      
    }
}

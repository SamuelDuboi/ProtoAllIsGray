using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door door;
#if UNITY_EDITOR
    public float size = 10;
    public Color color = Color.green;
#endif
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Movable>())
        {
            door.Perform();
        }
    }
}

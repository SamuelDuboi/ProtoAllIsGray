using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door[] doors;
#if UNITY_EDITOR
    public float size = 10;
    public Color color = Color.green;
#endif
   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Movable>())
        {
            foreach (var door in doors)
                door.Perform();
        }
    }
}

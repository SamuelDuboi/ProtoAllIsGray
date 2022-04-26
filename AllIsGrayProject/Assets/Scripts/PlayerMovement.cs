using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : Movable
{
    public Rigidbody rigidbody;
    public Transform myTransform;
    float rotation;
    bool IsRotating;
    [Range(0,0.5f)]
    public float rotationTreshold;

    [Range(0, 0.5f)]
    public float movementTreshold;

    bool isMoving;
    float movementValue;
    public float jetpackForce = 1;
    public float jetpackMaxTime = 1;
    public float jetpackCooldown = 2;
    private float jetpackCurrentUse;
    private bool isCoolingDown;
    private float currentCoolingValue;
    public Image CoolDownImage;
    public bool doCoolDown;
    public void Rotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>().x;
        IsRotating = Mathf.Abs(rotation) > rotationTreshold;
    }
    public void Impulse(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<float>();
        isMoving = Mathf.Abs(movementValue) > movementTreshold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRotating)
            transform.Rotate(0, 0, rotation, Space.Self);
        if (isMoving && jetpackCurrentUse <= jetpackMaxTime)
        {
            if (!CoolDownImage.gameObject.activeSelf)
                CoolDownImage.gameObject.SetActive(true);
            rigidbody.AddForce(myTransform.up * jetpackForce, ForceMode.Impulse);
            jetpackCurrentUse += Time.deltaTime;
            
        }
        else if(jetpackCurrentUse>0 && !doCoolDown)
        {
            jetpackCurrentUse -= Time.deltaTime;
        }
        if (doCoolDown)
        {
            if (jetpackCurrentUse < jetpackMaxTime && jetpackCurrentUse > 0.02f)
                jetpackCurrentUse -= Time.deltaTime;
            else if (isMoving && jetpackCurrentUse > jetpackMaxTime)
            {
                isCoolingDown = true;
                CoolDownImage.color = Color.red;
            }

            if (isCoolingDown)
            {
                currentCoolingValue += Time.deltaTime;
                CoolDownImage.fillAmount = currentCoolingValue / jetpackCooldown;
                if (currentCoolingValue >= jetpackCooldown)
                {
                    isCoolingDown = false;
                    currentCoolingValue = 0;
                    CoolDownImage.color = Color.green;
                    jetpackCurrentUse = 0;
                }
            }
        }
        else
            CoolDownImage.fillAmount = 1 - jetpackCurrentUse / jetpackMaxTime;
        
    }
}
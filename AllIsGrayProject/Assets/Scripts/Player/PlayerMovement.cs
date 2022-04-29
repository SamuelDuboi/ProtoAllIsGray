using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : Movable
{
    public PlayerHandler playerHandler;
    [Header("Movement")]
    public Rigidbody rigidbody;
    public Transform myTransform;
    Vector2 rotation;
    public float rotationSpeed = 1;
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

    public TrailRenderer jetpackTrail;
    public ParticleSystem jetpackCircles;
    public ParticleSystem jetpackFlames;
    public GameObject jetpackBigFlame;

    public TrailRenderer jetpackTrail2;
    public ParticleSystem jetpackCircles2;
    public ParticleSystem jetpackFlames2;
    public GameObject jetpackBigFlame2; 

    public void Rotation(InputAction.CallbackContext context)
    {
        
        rotation = context.ReadValue<Vector2>();
        IsRotating = Mathf.Abs(rotation.x) > rotationTreshold;
    }
    public void Impulse(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<float>();
        isMoving = Mathf.Abs(movementValue) > movementTreshold;

        if (context.started)
        {
            jetpackBigFlame.SetActive(true);
            jetpackFlames.Play();
            jetpackCircles.Play();
            jetpackTrail.emitting = true;

            jetpackBigFlame2.SetActive(true);
            jetpackFlames2.Play();
            jetpackCircles2.Play();
            jetpackTrail2.emitting = true;
        }
        else if (context.canceled)
        {
            jetpackBigFlame.SetActive(false);
            jetpackFlames.Stop();
            jetpackCircles.Stop();
            jetpackTrail.emitting = false;

            jetpackBigFlame2.SetActive(false);
            jetpackFlames2.Stop();
            jetpackCircles2.Stop();
            jetpackTrail2.emitting = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        jetpackBigFlame.SetActive(false);
        jetpackFlames.Stop();
        jetpackCircles.Stop();
        jetpackTrail.emitting = false;

        jetpackBigFlame2.SetActive(false);
        jetpackFlames2.Stop();
        jetpackCircles2.Stop();
        jetpackTrail2.emitting = false; 

    }

    // Update is called once per frame
    void Update()
    {
        if (IsRotating)
        {
            transform.localRotation =  Quaternion.Euler(0, 0,Mathf.Rad2Deg*Mathf.Atan2(rotation.y, rotation.x)-90);
           // transform.Rotate(0, 0, rotation.x*rotationSpeed, Space.Self);
           if(rigidbody.velocity.magnitude<2 ||Mathf.Sign(rotation.x) != Mathf.Sign(rigidbody.velocity.x)|| Mathf.Sign(rotation.y) != Mathf.Sign(rigidbody.velocity.y))
            rigidbody.AddForce(rotation *0.2f, ForceMode.Acceleration);
        }
        if (isMoving && jetpackCurrentUse <= jetpackMaxTime)
        {
            if (!CoolDownImage.gameObject.activeSelf)
                CoolDownImage.gameObject.SetActive(true);
            rigidbody.AddForce(myTransform.up * jetpackForce, ForceMode.Impulse);
            jetpackCurrentUse += Time.deltaTime;
            CoolDownImage.fillAmount = 1 - jetpackCurrentUse / jetpackMaxTime;
        }
        else if(jetpackCurrentUse>0 && !doCoolDown)
        {
            jetpackCurrentUse -= Time.deltaTime;
        }
        if (doCoolDown)
        {
            if (!isMoving && jetpackCurrentUse < jetpackMaxTime && jetpackCurrentUse > 0.02f)
            {
                jetpackCurrentUse -= Time.deltaTime;
                CoolDownImage.fillAmount = 1 - jetpackCurrentUse / jetpackMaxTime;
            }
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

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("KillZone"))
        {
            playerHandler.PlayerDeath();
        }
    }
}

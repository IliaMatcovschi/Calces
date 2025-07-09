using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] GameObject Hands;
    [SerializeField] GameObject[] Particles;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] Transform PickUpPos;
    [SerializeField] Transform PickUpPosOrig;
    [SerializeField] Rigidbody PickUpObj;

    [SerializeField] AudioSource PickUpSound;
    [SerializeField] AudioSource ChargeSound;
    [SerializeField] AudioSource ThrowSound;
    [SerializeField] AudioSource ChangeSound;

    [SerializeField] float scalePower;
    [SerializeField] float massPower;
    [SerializeField] float massLimitation;
    [SerializeField] float changeCooldown;
    [SerializeField] int throwForce;
    [SerializeField] int throwForcePower;
    [SerializeField] float throwForceCooldown;
    [SerializeField] int throwMinForce;
    [SerializeField] int throwMaxForce;
    [SerializeField] bool canCharge;
    [SerializeField] bool canChange;
    [SerializeField] bool canPickUp;
    [SerializeField] float axis;
    RaycastHit Hit;
    Ray ray;
    

    private void Start()
    {
        canChange = true;
        canPickUp = true;
        canCharge = true;
        ray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
    }
    void Update()
    {
        PlayerInput();

        axis = Input.GetAxis("Scale");
        if(PickUpObj != null && PickUpObj.CompareTag("Pickable")) 
            PickUpObj.transform.position = PickUpPos.transform.position;
        if(Physics.Raycast(PlayerCamera.transform.position, (PickUpPos.transform.position - PlayerCamera.transform.position).normalized, out RaycastHit hit, Vector3.Distance(PlayerCamera.transform.position, PickUpPos.transform.position), 7))
        {
            PickUpPos.position = hit.point;
        }
        else
        {
            PickUpPos.position = PickUpPosOrig.position;
        }

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * Mathf.Infinity, Color.red);
    }
    void ChangeScale(float axis)
    {
        ChangeSound.enabled = true;
        Vector3 ScaleChangeValue = new Vector3(scalePower, scalePower, scalePower);
        if (axis == -1 && (Hit.transform.localScale - ScaleChangeValue).magnitude > 0)
        {
            Hit.transform.localScale -= ScaleChangeValue;
            Hit.rigidbody.mass -= massPower;
        }
        else
        {
            Hit.transform.localScale += ScaleChangeValue;
            Hit.rigidbody.mass += massPower;
        }
        canChange = false;

        Invoke(nameof(CanChangeCooldown), changeCooldown);
    }
    void PlayerInput()
    {
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, Mathf.Infinity))
        {
            if (Input.GetButton("Scale") && canChange && (Hit.collider.CompareTag("Pickable") || Hit.collider.CompareTag("Attackable")))
                ChangeScale(Input.GetAxis("Scale"));

            else if (Input.GetButtonDown("Fire1") && canPickUp && Hit.collider.CompareTag("Pickable") && Hit.rigidbody.mass <= massLimitation)
                PickUp();
            else
                ChangeSound.enabled = false;
        }
        if(PickUpObj != null && canCharge && Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            ChargeSound.enabled = true;
            canCharge = false;

            if (throwForce != 0)
                Particles[(throwForce / (throwForce / throwForcePower)) - 1].SetActive(true);
            else
                Particles[throwForce].SetActive(true);

            if (throwForce + throwForcePower <= throwMaxForce)
                throwForce += throwForcePower;

            Invoke(nameof(CanChargeCooldown), throwForceCooldown);
        }
        else if(canCharge)
        {
            ChargeSound.enabled = false;
            canCharge = false;
            if(throwForce != 0)
                Particles[(throwForce / (throwForce / throwForcePower)) - 1].SetActive(false);
            else
                Particles[throwForce].SetActive(false);
            if(throwForce - throwForcePower >= throwMinForce)
                throwForce -= throwForcePower;

            Invoke(nameof(CanChargeCooldown), throwForceCooldown);
        }
        if(PickUpObj != null && !canPickUp && Input.GetButtonUp("Fire1"))
        {
            Throw();
        }
        else if(PickUpObj == null)
        {
            canCharge = false;
            throwForce = throwMinForce;
            canChange = true;
            canPickUp = true;
        }
    }
    void CanChangeCooldown()
    {
        canChange = true;
    }
    void CanChargeCooldown()
    {
        canCharge = true;
    }
    void PickUp()
    {
        PickUpSound.Play();
        PickUpObj = Hit.rigidbody;
        PickUpObj.useGravity = false;
        Hit.transform.position = PickUpPos.transform.position;
        canPickUp = false;
        canChange = false;
    }
    void Throw()
    {
        ThrowSound.Play();
        foreach(GameObject g in Particles)
        {
            g.SetActive(false);
        }
        PickUpObj.useGravity = true;
        PickUpObj.tag = "Attackable";
        PickUpObj.AddForce(PlayerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        PickUpObj = null;
        canCharge = false;
        throwForce = throwMinForce;
        canChange = true;
        canPickUp = true;
    }
}

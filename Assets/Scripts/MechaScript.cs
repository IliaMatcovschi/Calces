using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MechaScript : MonoBehaviour
{
    [SerializeField] float massReqiredMin;
    [SerializeField] float massReqiredMax;
    [SerializeField] DoorScript DS;
    [SerializeField] GearsExchange GE;
    [SerializeField] GameObject Gear;
    [SerializeField] Animator animator;
    public bool canClose;
    private void Start()
    {
        canClose = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && collision.rigidbody.mass > massReqiredMin && collision.rigidbody.mass < massReqiredMax)
        {
            animator.SetTrigger("Close");
            DS.nowReq += 1;
            if(GE != null)
                GE.nowReq += 1;
            DS.Check();
            Gear.SetActive(true);
            canClose = true;
            Destroy(collision.gameObject);
        }
    }
}

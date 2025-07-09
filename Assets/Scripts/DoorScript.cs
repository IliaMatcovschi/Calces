using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AudioSource DoorOpen;
    [SerializeField] AudioSource DoorClose;
    [SerializeField] int req;
    public int nowReq;

    public void Check()
    {
        if(nowReq == req)
        {
            DoorOpen.Play();
            animator.SetTrigger("Open");
        }
    }
    public void DisCheck()
    {
        DoorClose.Play();
        animator.SetTrigger("Close");
    }

}

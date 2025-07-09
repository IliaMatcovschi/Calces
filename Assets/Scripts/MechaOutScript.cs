using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaOutScript : MonoBehaviour
{
    [SerializeField] GameObject GearPrefab;
    [SerializeField] GameObject Gear;
    [SerializeField] Transform SpawnTransform;
    [SerializeField] Animator animator;
    [SerializeField] DoorScript DS;
    bool canOut;
    private void Start()
    {
        canOut = true;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Fire1") && canOut)
        {
            animator.SetTrigger("Close");
            DS.DisCheck();
            Instantiate(GearPrefab, SpawnTransform.position, SpawnTransform.rotation);
            Gear.SetActive(false);
            canOut = false;
        }
    }
}

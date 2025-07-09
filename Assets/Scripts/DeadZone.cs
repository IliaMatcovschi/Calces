using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField] Transform Spawnpoint;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = Spawnpoint.position;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}

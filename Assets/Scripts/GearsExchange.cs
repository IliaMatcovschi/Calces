using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GearsExchange : MonoBehaviour
{
    [SerializeField] GameObject GearPrefab;
    [SerializeField] GameObject Gear;
    [SerializeField] Transform SpawnTransform;
    [SerializeField] int req;
    public int nowReq;
    bool canSpawn;
    private void Start()
    {
        canSpawn = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (canSpawn && other.gameObject.CompareTag("Player"))
        {
            Gear = Instantiate(GearPrefab, SpawnTransform.position, SpawnTransform.rotation);
            canSpawn = false;
        }
    }
    private void Update()
    {
        if (Gear.IsDestroyed() && !canSpawn && nowReq != req)
        {
            Gear = Instantiate(GearPrefab, SpawnTransform.position, SpawnTransform.rotation);
        }
    }
}

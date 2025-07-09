using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class GlassScript : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] float SecondsToDestroy;
    [SerializeField] GameObject MelePrefab;
    [SerializeField] GameObject RangePrefab;
    [SerializeField] GameObject GearPrefab;
    [SerializeField] GameObject Pipe;
    [SerializeField] Transform SpawnPoint;
    [SerializeField] Transform GearSpawnPoint;
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource GlassBr;
    bool canDestroy;
    private void Start()
    {
        canDestroy = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Pickable"))
        {
            if(collision.rigidbody.mass > HP)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Update()
    {
        if(canDestroy)
        {
            SecondsToDestroy -= Time.deltaTime;
            if(SecondsToDestroy <= 0 && SpawnPoint != null)
            {
                GlassBr.Play();
                Music.enabled = true;
                Pipe.SetActive(false);

                Instantiate(GearPrefab, GearSpawnPoint.position, GearSpawnPoint.rotation);
                Instantiate(GearPrefab, GearSpawnPoint.position, GearSpawnPoint.rotation);
                Instantiate(GearPrefab, GearSpawnPoint.position, GearSpawnPoint.rotation);
                Instantiate(GearPrefab, GearSpawnPoint.position, GearSpawnPoint.rotation);
                Instantiate(GearPrefab, GearSpawnPoint.position, GearSpawnPoint.rotation);

                Instantiate(MelePrefab, SpawnPoint.position + Vector3.forward, SpawnPoint.rotation);
                Instantiate(MelePrefab, SpawnPoint.position + Vector3.back, SpawnPoint.rotation);
                Instantiate(RangePrefab, SpawnPoint.position + Vector3.right, SpawnPoint.rotation);
                Instantiate(RangePrefab, SpawnPoint.position + Vector3.left, SpawnPoint.rotation);

                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            canDestroy = true;
    }
}

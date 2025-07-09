using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyVision EV;
    [SerializeField] ÑharacterStats PlayerStats;
    [SerializeField] ÑharacterStats MyStats;
    [SerializeField] float damage;
    [SerializeField] float damageCooldown;
    [SerializeField] float timeRemaning;
    private void Start()
    {
        PlayerStats = FindAnyObjectByType<PlayerAbilities>().GetComponentInChildren<ÑharacterStats>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
            EV.canMove = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeRemaning -= Time.deltaTime;
            if (timeRemaning < 0 && other != null)
            {
                PlayerStats.hp = PlayerStats.HPDown(damage);
                timeRemaning = damageCooldown;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            EV.canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(MyStats.hp < 0)
            Destroy(this.gameObject);
    }
}

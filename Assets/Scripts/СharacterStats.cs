using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СharacterStats : MonoBehaviour
{
    public float hp;
    public float maxHP;
    [SerializeField] AudioSource MyAudioSource;


    public float HPDown(float damage)
    {
        MyAudioSource.Play();
        return hp - damage;
    }
    public float HPUp(float heal)
    {
        if (hp + heal > maxHP)
            return hp = maxHP;
        return hp + heal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Attackable"))
        {
            collision.gameObject.tag = "Pickable";
            hp = HPDown(Mathf.RoundToInt(collision.rigidbody.mass));
        }

    }
}

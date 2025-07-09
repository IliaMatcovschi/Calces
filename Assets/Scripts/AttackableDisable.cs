using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableDisable : MonoBehaviour
{
    [SerializeField] AudioSource MyAudioClip;
    private void OnCollisionEnter(Collision collision)
    {
        MyAudioClip.Play();
        gameObject.tag = "Pickable";
    }
}

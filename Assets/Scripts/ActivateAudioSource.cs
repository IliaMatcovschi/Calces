using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAudioSource : MonoBehaviour
{
    [SerializeField] AudioSource[] DisableSources;
    [SerializeField] AudioSource[] PlaySources;

    private void OnTriggerEnter(Collider other)
    {
        foreach(AudioSource source in DisableSources)
        {
            source.Stop();
        }
        foreach(AudioSource source in PlaySources)
        {
            source.Play();
        }
    }

}

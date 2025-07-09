using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer MyAudioMixer;
    [SerializeField] GameObject Content;
    [SerializeField] Slider[] Sliders;
    bool isAct;
    private void Start()
    {
        isAct = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isAct)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Content.SetActive(true);
            isAct = true;
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isAct)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Content.SetActive(false);
            isAct = false;
            Time.timeScale = 1f;
        }
    }

    public void SetGroupsVolume()
    {
        MyAudioMixer.SetFloat("master", Mathf.Log10(Sliders[0].value)*20);
        MyAudioMixer.SetFloat("music", Mathf.Log10(Sliders[1].value)*20);
        MyAudioMixer.SetFloat("sfx", Mathf.Log10(Sliders[2].value)*20);
    }

    public void Exit()
    {
        Application.Quit();
    }

}

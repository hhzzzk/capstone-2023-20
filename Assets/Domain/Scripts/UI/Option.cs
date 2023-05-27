using System.Collections;
using System.Collections.Generic;
using Photon.Voice;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using StarterAssets;
using Photon.Pun;

public class Option : MonoBehaviour
{
    
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider voice;
    [SerializeField]
    private Slider bgm; 
    [SerializeField]
    private Slider sfx;
    [SerializeField]
    private Slider sensitivity;
    [SerializeField]
    private StarterAssetsInputs Input;

    private GameObject manager;
    private const float sensitivityMin = 0.1f;

    private GameObject warning;
    private GameObject backGround;

    private void Awake()
    {
        manager = GameObject.Find("NetworkManager");
        warning = this.transform.GetChild(0).gameObject;
        backGround = this.transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        this.gameObject.GetComponent<Canvas>().sortingOrder = 50;
    }

    private void OnDisable()
    {
        this.gameObject.GetComponent<Canvas>().sortingOrder = 0;
    }

    public void SetInputSystem(StarterAssetsInputs newinput)
    {
        Input = newinput;
    }
    public void SetSoundVoice()
    {
        mixer.SetFloat("Voice", voice.value);
        int perc = (int)voice.value + 80;
        voice.GetComponentInChildren<TMP_Text>().text = perc.ToString() + '%';
    }
    public void SetSoundBGM()
    {
        mixer.SetFloat("BGM", bgm.value);
        int perc = (int)bgm.value + 80;
        bgm.GetComponentInChildren<TMP_Text>().text = perc.ToString()+'%';
    }
    public void SetSoundSFX()
    {
        mixer.SetFloat("SFX", sfx.value);
        int perc = (int)sfx.value + 80;
        sfx.GetComponentInChildren<TMP_Text>().text = perc.ToString()+'%';
    }

    public void OnClickOut()
    {
        //��׶��� �ɼ�
        backGround.gameObject.SetActive(false);
        //���â
        warning.gameObject.SetActive(true);
        
    }

    public void QuickOption()
    {
        warning.SetActive(false);
        backGround.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSensitivity()
    {
        float arg = sensitivity.value;
        if (arg == 0) arg = sensitivityMin;
        Input.SetSensitivity(arg);
        int perc = (int)sensitivity.value * 10;
        sensitivity.GetComponentInChildren<TMP_Text>().text = perc.ToString() + '%';
    }

    //warning
    public void OnRealOutBtn()
    {
        manager.GetComponent<NetworkManager>().LeaveRoom();
    }

    public void OnBackBtn()
    {
        warning.SetActive(false);
        backGround.SetActive(true);
    }
}

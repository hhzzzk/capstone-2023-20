using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StairSound : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ������Ʈ
    public string TagName = "Taichi";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagName))
        {
            PlayStairSound();
        }
    }

    private void PlayStairSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
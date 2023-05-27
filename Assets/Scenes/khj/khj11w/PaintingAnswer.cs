using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAnswer : MonoBehaviour
{
    public Animation animation; // �ִϸ��̼� ������Ʈ�� ������ ����
    public AudioSource audioSource; // ���带 ����� ����� �ҽ� ������Ʈ


    private void Start()
    {
        animation = GetComponent<Animation>(); // �ִϸ��̼� ������Ʈ ��������
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }
    void EnableAnswer()
    {
        animation.enabled = true; // �ִϸ��̼� ������Ʈ Ȱ��ȭ
        audioSource.enabled = true;
    }
}

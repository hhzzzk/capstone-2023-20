using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;

public class sifidoor : MonoBehaviour
{
    public UnityEvent Event;

    Animator animator;
    AudioSource audiosource;
    public AudioClip DoorOpen;
    public AudioClip DoorLocked;

    [SerializeField]
    private bool LockState = false;

    int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = 0.5f;
        audiosource.playOnAwake = false;
        DoorOpen = audiosource.clip;
    }


    // Update is called once per frame
    void Update()
    {
        //if ( // ���� ȹ���ڵ� ))
        //{
        //    LockState = true;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (LockState == false)
        {
            if (other.tag == "Taichi" || other.tag == "Latifa")
            {
                animator.SetBool("IsOpen", true);
                audiosource.Play();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (LockState == false)
        {
            if ((other.tag == "Taichi" || other.tag == "Latifa") && animator.GetBool("IsOpen") == true)
            {
                animator.SetBool("IsOpen", false);
                audiosource.Play();
            }
        }

    }

    public void UnLockDoor()
    {
        if (LockState == true)
        {
            LockState = false;
        }
    }

    // �ٸ� ������Ʈ�� ��ȣ�ۿ�� �Լ�
}

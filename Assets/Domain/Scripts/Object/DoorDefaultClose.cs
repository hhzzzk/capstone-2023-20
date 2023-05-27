using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class DoorDefaultClose : MonoBehaviour
{
    public UnityEvent Event;

    public Animator animator;
    AudioSource audiosource;
    public AudioClip DoorOpen;
    public AudioClip DoorLocked;

    public PhotonView pv;

    [SerializeField]
    private bool LockState = false;
    [SerializeField]
    private DoorDefaultClose ConnectedDoor;
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
    public void Activate()
    {

        if (LockState == false)
        {
            audiosource.clip = DoorOpen;
            //���� - ��ȣ�ۿ��� �������� �Է��� �� ���� �����ϱ� ���� state�� �ٽ� �����ϴ� ���� ����
            if (animator.GetBool("IsOpen") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Default"))
            {
                Debug.Log("Default");
                animator.SetBool("IsOpen", true);
                audiosource.Play();
            }
            else if (animator.GetBool("IsOpen") == true && animator.GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {
                Debug.Log("Open");
                animator.SetBool("IsOpen", false);
                audiosource.Play();
            }
        }
        else if (LockState == true)
        {
            audiosource.clip = DoorLocked;
            audiosource.Play();

        }

    }



    // �ٸ� ������Ʈ�� ��ȣ�ۿ�� �Լ�
    public void InteractActivate(bool OncePlay)
    {
        // �����°� �����Ǿ��� ����
        if (LockState == false)
        {
            if (OncePlay)
            {
                if (count == 0)
                {
                    Event.Invoke();
                    count++;
                }

            }
            else
            {
                Event.Invoke();
            }
        }
    }

    public void UnLockDoor()
    {
        if (LockState == true)
        {
            LockState = false;
            Debug.Log("UnLock Other too");
            UnLockOther();
            //pv.RPC("UnLockOther", RpcTarget.Others, "broadcastrooomdoor1");
            audiosource.clip = DoorLocked;
            audiosource.Play();
        }
        

    }

    public void UnLockOther()
    {
        LockState = false;
    }
    
    

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        animator.SetBool("IsOpen", true);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        animator.SetBool("IsOpen", false);
    //    }
    //}
}

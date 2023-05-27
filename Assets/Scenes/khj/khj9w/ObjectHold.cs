using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHold : MonoBehaviour
{
    public GameObject heldObject;
    public Transform playerTransform;
    public float range = 3f;
    public float throwForce = 100f;
    public Camera mainCamera;
    public DollsObjectMatching dollsObjectMatching; // �߰�: DollsObjectMatching ��ũ��Ʈ ����

    private Collider objectCollider;
    private Rigidbody objectRigidbody;
    private Vector3 originalPosition;
    private Transform originalParent;

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameObject.Find("HoldPlayerTransform").transform;
        objectCollider = heldObject.GetComponent<Collider>();
        objectRigidbody = heldObject.GetComponent<Rigidbody>();
        originalPosition = heldObject.transform.position;
        originalParent = heldObject.transform.parent;

        // �߰�: DollsObjectMatching ��ũ��Ʈ ��������
        dollsObjectMatching = FindObjectOfType<DollsObjectMatching>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            StartPickUp();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Drop();
        }
    }

    private void StartPickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
        {
            HoldTarget target = hit.transform.GetComponent<HoldTarget>();
            if (target != null)
            {
                Debug.Log("Pick up");
                heldObject = hit.transform.gameObject;
                PickUp();
            }
        }
    }

    private void PickUp()
    {
        heldObject.transform.SetParent(playerTransform);
        objectCollider.enabled = false;
    }

    private void Drop()
    {
        heldObject.transform.SetParent(originalParent);
        objectCollider.enabled = true;

        RaycastHit hit;
        if (Physics.Raycast(heldObject.transform.position, Vector3.down, out hit))
        {
            heldObject.transform.position = hit.point;
        }
        else
        {
            heldObject.transform.position = originalPosition;
        }

        objectRigidbody.velocity = mainCamera.transform.forward * throwForce;
        heldObject = null;

        // �߰�: DollsObjectMatching ��ũ��Ʈ�� OnItemPositionChanged �Լ� ȣ��
        if (dollsObjectMatching != null)
        {
            Debug.Log("onitemchange");
            dollsObjectMatching.OnItemPositionChanged();
        }
    }
}

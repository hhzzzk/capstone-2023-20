using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogEntryController : MonoBehaviour
{
    public string objectToActivateName = "Fog"; // Ȱ��ȭ�� ������Ʈ�� �̸��� �Է��մϴ�.

    [SerializeField] private GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ�� �����ϱ� ���� ����

    //private void Start()
    //{
    //    objectToActivate = GameObject.Find(objectToActivateName); // �̸��� �ش��ϴ� ������Ʈ�� ã�� �����մϴ�.

    //    if (objectToActivate == null)
    //    {
    //        Debug.LogError("Object with the name " + objectToActivateName + " not found!");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Taichi"))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}

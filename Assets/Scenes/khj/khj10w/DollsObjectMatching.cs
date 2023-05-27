using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollsObjectMatching : MonoBehaviour
{
    [SerializeField] private Transform dollPos;
    [SerializeField] private Transform dolls;
    [SerializeField] private GameObject childObject;
    [SerializeField] private string childObjName = "magicCircleView";

    private List<Transform> dollPosChildren = new List<Transform>();
    private Coroutine matchingCoroutine; // ��Ī üũ�� �ڷ�ƾ

    private void Start()
    {
        GameObject dollPosObject = GameObject.Find("DollsPos");
        if (dollPosObject != null)
            dollPos = dollPosObject.transform;

        GameObject dollsObject = GameObject.Find("Dolls");
        if (dollsObject != null)
            dolls = dollsObject.transform;

        FindDollPosChildren();

        StartMatchingCoroutine(); // ��Ī üũ �ڷ�ƾ ����
    }

    private void FindDollPosChildren()
    {
        int childCount = dollPos.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = dollPos.GetChild(i);
            dollPosChildren.Add(child);
        }
    }

    private void StartMatchingCoroutine()
    {
        if (matchingCoroutine != null)
            StopCoroutine(matchingCoroutine);

        matchingCoroutine = StartCoroutine(CheckObjectMatchingCoroutine());
    }

    private IEnumerator CheckObjectMatchingCoroutine()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1.0f);

        while (true)
        {
            yield return waitTime;

            int matchedObjectCount = 0;

            int childCount = dollPos.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform dollPosChild = dollPosChildren[i];
                Transform dollsChild = dolls.GetChild(i);

                float distance = Vector3.Distance(dollPosChild.position, dollsChild.position);

                if (distance <= 0.6f)
                    matchedObjectCount++;
            }

            //Debug.Log("cnt > " + matchedObjectCount);

            if (matchedObjectCount >= 6)
            {
                Debug.Log("Matching Completed!");
                ActivateObjectWithSound();
            }
        }
    }

    private void ActivateObjectWithSound()
    {
        if (childObject != null)
            childObject.SetActive(true);
    }

    // �������� ��ġ�� �������� �� ȣ��Ǿ�� �ϴ� �Լ�
    public void OnItemPositionChanged()
    {
        StartMatchingCoroutine(); // ��Ī üũ �ڷ�ƾ �����
    }
}

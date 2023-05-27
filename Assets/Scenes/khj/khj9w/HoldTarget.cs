using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTarget : MonoBehaviour
{
    public bool isPickedUp = false; // �������� ����� �ִ��� ���θ� ��Ÿ���� ����

    public void PickUp()
    {
        isPickedUp = true;
    }

    public void Drop()
    {
        isPickedUp = false;
    }

    public bool CanBePickedUp()
    {
        return !isPickedUp;
    }

    private void Update()
    {
        // ����: ��� �����۵��� ���ڸ��� ��ġ�ϸ� �ı���
        if (CheckIfInPlace())
        {
            Die();
        }
    }

    private bool CheckIfInPlace()
    {
        // �������� ���ڸ� ��ġ ���θ� Ȯ���ϴ� ������ �ۼ�
        // ����: �������� ��ġ�� ������ üũ�Ͽ� ���ڸ��� ��ġ�ϴ��� �Ǵ�
        // �� �����ۿ� �´� üũ ������ �ۼ��ؾ� �մϴ�.
        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

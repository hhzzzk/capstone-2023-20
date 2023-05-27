using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SkipController : MonoBehaviourPunCallbacks
{
    public string NextSceneName;
    public GameObject SkipBtn;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient) SkipBtn.SetActive(true);
    }

    public void SkipButtonClicked()
    {
        // ���� �÷��� ��ŵ ���� �ۼ�
        // ��: ���� ������ ��ȯ
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
        else Debug.Log("������ �ƴմϴ�. ��ŵ�ȵ˴ϴ�.213����������������");
    }
}

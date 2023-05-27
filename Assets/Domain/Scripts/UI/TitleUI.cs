using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TitleUI : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject lobbyUI;
    [SerializeField]
    private GameObject titleUI;

    private void Awake()
    {
        if (PhotonNetwork.InLobby) OnJoinedLobby();
  
    }
    public void OnClickOnlineBtn()
    {
        Debug.Log("�¶��� ��ư Ŭ��");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        // PhotonNetwork.MaxResendsBeforeDisconnect = 8;
    }

   

    public void ClickExitBtn()
    {
        Debug.Log("Click Exit");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("�¶��� ����");
        PhotonNetwork.JoinLobby();
     
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ���� �Ϸ�");
        titleUI.SetActive(false);
        lobbyUI.SetActive(true);
    }
}

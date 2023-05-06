using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.Cockpit.Forms;

public class NetworkManager : MonoBehaviourPunCallbacks { 
    public PhotonView pv;
    [SerializeField]
    private GameObject titleUI;
    [SerializeField]
    private GameObject lobbyUI;

    private void Awake()
    {
        
    }

    public void OnLevelWasLoaded(int level)
    {
        if(level > 0)
        {
            SetPlayer(PhotonNetwork.LocalPlayer);
        }
    }
    private void SetPlayer(Player player)
    {
        if (GameObject.Find("Player" + player.NickName) != null)
        {
            GameObject.Find("Player" + player.NickName).GetComponent<OwnershipTransfer>().OwnershipTransferLocalPlayer(player);
            GameObject.Find("Player" + player.NickName).GetComponent<ThirdPlayerController>().virtualCamera.Priority += 10;
        }
    }

    public void OnClickOutBtn()
    {
        Debug.Log("���Ӿ�������");
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
        titleUI.SetActive(false);
        lobbyUI.SetActive(true);
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class NetworkManager : MonoBehaviourPunCallbacks { 

    public  PhotonView pv;
    [SerializeField]
    private GameObject CloseGameMsg;

    
    private void Awake()
    {
        var objs = FindObjectsOfType<NetworkManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }


    //���ӿ��������� �̵��ϴ� �ۺ��Լ�(�װų�, �ð��ʰ����� �� �Լ� ȣ��)
    public void GameOver()
    {
        pv.RPC("MoveGameOverScene", RpcTarget.MasterClient);
    }
    [PunRPC]
    private void MoveGameOverScene()
    {
        Hashtable cp = PhotonNetwork.LocalPlayer.CustomProperties;
        if (cp.ContainsKey("GameReady")) cp.Remove("GameReady");
        cp.Add("GameReady", false);
        PhotonNetwork.LocalPlayer.SetCustomProperties(cp);
        if (GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName) == null) return;
        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //���콺 Ŀ�� �ǵ���.
        if (PhotonNetwork.IsMasterClient)
        {
            cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("InGame")) cp.Remove("InGame"); //�浹 ���� Ȯ���ϰ� ������ ������Ʈ �ϱ� ����;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("InGame", false);
            cp.Add("GameOver", true); //���ӿ����������� �ƴ���
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
        }
        Debug.Log("�����÷��̾� �̸� ::" + GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name);
        FadeOut();
    }
    public void OnLevelWasLoaded(int level)
    {
        // ���κ����̰ų�, ���ӿ��� ���¸鼭, ���ӿ������� �ƴ� ��츸 �÷��̾�ĳ���� ����
        if(level == 2 || ((bool)PhotonNetwork.CurrentRoom.CustomProperties["GameOver"] && level != 6) ) 
        {
            pv.RPC("CreatePlayer", RpcTarget.All);
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            if (cp.ContainsKey("GameOver")) cp.Remove("GameOver");
            cp.Add("GameOver", false);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            
        }
        else if(level <= 1 )
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        else if(level == 3 || level == 4) //level == ������
        {
            pv.RPC("SetPlayerPos", RpcTarget.All);
        }
    }
    [PunRPC]
    private void CreatePlayer()
    {
        GameObject chk = GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName);
        if (chk != null) return;
        Transform pos = GameObject.Find("SpwanPoint" + PhotonNetwork.LocalPlayer.NickName).transform;
        PhotonNetwork.Instantiate("Player" + PhotonNetwork.LocalPlayer.NickName, pos.position, pos.rotation);

        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>().virtualCamera.Priority += 10;
        Debug.Log(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name + " �����Ϸ� ũ����Ʈ�÷��̾�");
        
    }
    [PunRPC]
    private void SetPlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName);
        if (player != null)
        {
            Debug.LogError("�÷��̾� ĳ���Ͱ� �����!!!");
            return;
        }
        Transform pos = GameObject.Find("SpwanPoint" + PhotonNetwork.LocalPlayer.NickName).transform;

        player.transform.position = pos.position;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CloseGameMsg.SetActive(true);
        Invoke("LeaveRoom",3f);
    }
    
    public void LeaveRoom()
    {
        GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<StarterAssetsInputs>().PlayerMoveLock(); //���콺 Ŀ�� �ǵ���.
        Debug.Log(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).name + " �ı��ϰ� �� ������");
        PhotonNetwork.Destroy(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName));
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainTitle");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("cause : " + cause.ToString());
    }

    public override void OnLeftRoom()
    {
        Debug.Log("���� �������ϴ�.");
    }
    private void FadeOut()
    {
        if(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>() != null)
        {
            GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName).GetComponent<ThirdPlayerController>().FadingStart();
        }
        PhotonNetwork.Destroy(GameObject.FindGameObjectWithTag(PhotonNetwork.LocalPlayer.NickName));
        GameOverManager.LoadGameOver();
    }
















    //���� �ε��Ҷ� ���Ǵ� �Լ� ==> ���� ������ �������� ������ �����Ǹ� �Ʒ� �Լ��� ȣ�����ָ� �˴ϴ� �����ؼ� ������ּ���
    /*  
    void func(){
            if(!PhotonNetwork.isMasterClient) return;
            Hashtable cp = PhotonNetwork.CurrentRoom.CustomProperties;
            int nextLevel = (int)cp["CurrentLevel"] + 1;
            if (cp.ContainsKey("CurrentLevel")) cp.Remove("CurrentLevel"); //�浹 ���� Ȯ���ϰ� ������ ������Ʈ �ϱ� ����;
            cp.Add("CurrentLevel", nextLevel);
            PhotonNetwork.CurrentRoom.SetCustomProperties(cp);
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("CurrentLevel"))
            {
                LoadingSceneController.LoadScene();
            }
    }
    */
}

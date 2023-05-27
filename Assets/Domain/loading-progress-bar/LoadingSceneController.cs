using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private TMP_Text tip;
    [SerializeField]
    private TMP_Text explain;
    [SerializeField]
    private GameObject[] BGI;



    private int nextLevel;
    //Ÿ��Ʋ, ����, ü����, ������, ���ӿ�����, �ε��� ������ ����
    //�׽�Ʈ �� - PhotonTest-KKB;
    private string[] levels = { "MainTitle", "Mainbuilding", "Gym", "scify_ysh", "" };
    private string[] tips = {
        "2������ �÷��̰� �����մϴ�.",
        "����� �̷��� �б��� ����Ǿ� �ִ� �ǰ�..?",
        "�� �̷��� ü������ ���� �κη��� ����?..",
        ".......",
         };
    private string[] explains = {
        "Ÿ��Ʋ�� �̵� ��...",
        "�������� ���� ��..",
        "ü�������� ���� ��..",
        "�ǹ��� �����Ƿ� ���� ��.."
    };
    public static void LoadScene()
    {
        if (PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel("LoadingScene");
    }
    
    void Awake()
    {
        
        nextLevel = (int)PhotonNetwork.CurrentRoom.CustomProperties["CurrentLevel"];
        Debug.Log("nextlevel == " + levels[nextLevel]);
        tip.text = "Tips : " + tips[nextLevel];
        explain.text = explains[nextLevel];
        BGI[nextLevel].SetActive(true);
        if (PhotonNetwork.AutomaticallySyncScene) Debug.Log("���� �ڵ� ����ȭ�� �˴ϴ�.");
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonNetwork : Trying to Load a level but we are not the master Client at LSC");
            return;
        }
        else pv.RPC("StartLoadSceneProcess", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void StartLoadSceneProcess()
    {
        if (PhotonNetwork.IsMasterClient) StartCoroutine(LoadSceneProcess());
    }
   
    IEnumerator LoadSceneProcess()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levels[nextLevel]); // �񵿱�� �� �ε� ����
        asyncLoad.allowSceneActivation = false; // �� Ȱ��ȭ�� ��� ����

        while (!asyncLoad.isDone) // �� �ε��� �Ϸ�� ������ �ݺ�
        {
            progressBar.fillAmount = asyncLoad.progress; // ���� ��Ȳ�� ���α׷��� �ٿ� �ݿ�

            if (asyncLoad.progress >= 0.9f) // �� �ε��� ���� �Ϸ�Ǿ��� ��
            {
                asyncLoad.allowSceneActivation = true; // �� Ȱ��ȭ ���
            }

            yield return null;
        }

        BGI[nextLevel].SetActive(false); // �ε��� �Ϸ�Ǹ� BGI ��Ȱ��ȭ
    }

}

using UnityEngine;
using UnityEngine.Video;
using Photon.Pun;
using Photon.Realtime;
public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string NextSceneName;

    private void Start()
    {
        // ���� �÷��̾� ������Ʈ ��������
        videoPlayer = GetComponent<VideoPlayer>();

        // Loop Point Reached �̺�Ʈ�� �̺�Ʈ �ڵ鷯 ���
        videoPlayer.loopPointReached += OnLoopPointReached;
    }

    private void OnLoopPointReached(VideoPlayer source)
    {
        // ���� ����� ������ ���� ������ ��ȯ
        if (PhotonNetwork.IsMasterClient) LoadingSceneController.LoadScene();
    }
}

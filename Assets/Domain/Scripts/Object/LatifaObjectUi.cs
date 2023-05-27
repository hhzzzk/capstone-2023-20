using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatifaObjectUi : MonoBehaviour
{
    private GameObject latifa;
    private GuideUiManager UiManager;
    private Timeline[] Timelines;
    private Timeline Both;
    private Timeline Main;
    private Timeline Sub;

    public string maintxt;
    public string subtxt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        latifa = GameObject.FindGameObjectWithTag("Latifa");
        UiManager = latifa.GetComponentInChildren<GuideUiManager>();
        Timelines = latifa.GetComponentsInChildren<Timeline>();
        if (Timelines.Length > 0)
        {
            // ���ϴ� ��ũ��Ʈ�� �����Ͽ� ���
            Both = Timelines[0];
            Main = Timelines[1];
            Sub = Timelines[2];
        }

        //Transform MainContoller = latifa.transform.Find("MainContloller");
        //Transform SubContoller = latifa.transform.Find("SubContloller");
        //MainTimeline = MainContoller.GetComponent<Timeline>();
        //SubTimeline = SubContoller.GetComponent<Timeline>();
    }
    //public void SetMainPanelText(string txt)
    //{
    //    maintxt = txt;
    //}
    //public void SetSubPanelText(string txt)
    //{
    //    subtxt = txt;
    //}
    public void SetGuideUi()
    {
        if (subtxt.Length > 0)
        {
            UiManager.SetMainPanelText(maintxt);
            UiManager.SetSubPanelText(subtxt);
        }
        else
            UiManager.SetMainPanelText(maintxt);

    }
    public void TimeLineActive()
    {
        if (maintxt.Length > 0 && subtxt.Length > 0)
            Both.Play();
        else if (maintxt.Length > 0)
            Main.Play();
        else
            Sub.Play();
    }
    public void MainTimeLineActive()
    {
        Main.Play();
    }
    public void SubTimeLineActive()
    {
        Sub.Play();
    }
}

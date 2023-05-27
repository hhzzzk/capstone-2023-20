using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LeversController : MonoBehaviour
{
    // Start is called before the first frame update
    public lever[] levers;
    public int OrderNumber = 0;
    private int ClearNumber;
    private ObjectManager objectmanager;
    private AudioSource audio;

    void Start()
    {
        //levers = GameObject.Find("Shield Metall");
        ClearNumber = levers.Length - 1;
        objectmanager = GetComponent<ObjectManager>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeverDoorTagChange()
    {
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject lever in cups)
        {
            lever.tag = "EventObj";
        }
    }
        
    public void Initiate()
    {
        OrderNumber = 0;
        //List<int> numbers = new List<int>() { 0, 1, 2, 3 };
        //List<int> pickedNumbers = new List<int>();

        foreach (lever lever in levers)
        {
            lever.SwichOff1s();
            lever.ImageInActive();
        }

        ////�������� �ѹ� �������� �ο�
        //foreach (lever lever in levers)
        //{
        //    int randomIndex = UnityEngine.Random.Range(0, numbers.Count);
        //    int pickedNumber = numbers[randomIndex];
        //    numbers.RemoveAt(randomIndex);
        //    pickedNumbers.Add(pickedNumber);

        //    lever.number = pickedNumber;

        //}
        //// ���� �ѹ������� ������
        //levers = levers.OrderBy(o => o.number).ToArray();;
        foreach ( lever lever in levers )
        {
            lever.number = OrderNumber;
            OrderNumber++;
        }

        levers[0].ImageActive();
        OrderNumber = 0;
    }
    public void NumberCheck(int num)
    {
        if (num != OrderNumber)
        {
            //���� �ʱ�ȭ
            Debug.Log("���� Ʋ��");
            Initiate();

        }
        else if (num == ClearNumber)
        {
            Debug.Log("clear");
            objectmanager.Activate();
        }

        else
        {
            levers[OrderNumber].ImageInActive();
            OrderNumber++;
            levers[OrderNumber].ImageActive();
            audio.Play();
        }
    }
}

using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Puzzles
{
    public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public int snapOffset = 30;
        public JigsawPuzzle puzzle;
        public int piece_no;
        public PhotonView pv;

        public GameObject manager;
        private GameObject latifa;



        void Start()
        {

            //pv = GameObject.Find("NetworkManager").GetComponent<PhotonView>();
            manager = GameObject.Find("PopupManager");

        }

        bool CheckSnapPuzzle()
        {
            for (int i = 0; i < puzzle.puzzlePosSet.transform.childCount; i++)
            {
                //��ġ�� �ڽĿ�����Ʈ�� ������ �̹� ���������� ������ ���Դϴ�.
                if (puzzle.puzzlePosSet.transform.GetChild(i).childCount != 0)
                {
                    continue;
                }
                else if (Vector2.Distance(puzzle.puzzlePosSet.transform.GetChild(i).position, transform.position) < snapOffset)
                {
                    transform.SetParent(puzzle.puzzlePosSet.transform.GetChild(i).transform);
                    transform.localPosition = Vector3.zero;
                    return true;
                }
            }
            return false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            pv = GameObject.FindWithTag("Latifa").GetComponent<PhotonView>();
        }


        [PunRPC]
        public void ObjectFunc(string a)
        {
            manager.GetComponent<ObjectManager>().SyncActivate();
        }



        public void OnEndDrag(PointerEventData eventData)
        {
            //��ġ�ϴ� ��ġ�� ���� ��� �θ��ڽ� ���踦 �����մϴ�.
            if (!CheckSnapPuzzle())
            {
                transform.SetParent(puzzle.puzzlePieceSet.transform);
            }

            // ���� Ŭ����� �ڵ�
            if (puzzle.IsClear())
            {
                Debug.Log("Clear");
                puzzle.GetComponent<ObjectManager>().Activate();
                pv.RPC("SyncFunc", RpcTarget.All, "PopupManager");
            }
        }




    }

}

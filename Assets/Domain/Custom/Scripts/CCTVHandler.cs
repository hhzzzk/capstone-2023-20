using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVHandler : MonoBehaviour
{
    public GameObject CCTV;
    private bool hasEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Latifa" || other.tag == "Taichi") && hasEntered==false)
        {
            Debug.Log("Entered");
            CCTV.SetActive(true);
            hasEntered = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if ((other.tag == "Latifa" || other.tag == "Taichi"))
        {
            CCTV.SetActive(false);
        }
    }
}

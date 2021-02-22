using Kandooz.KVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ReloadHands : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;

    bool run = false;

    void Awake()
    {
        Debug.Log("== hands loading ==");
        if(run != true)
        {
            restart();
        }
    }
    
    void restart()
    {
        rightHand.SetActive(true);
        leftHand.SetActive(true);
        run = true;
    }
}

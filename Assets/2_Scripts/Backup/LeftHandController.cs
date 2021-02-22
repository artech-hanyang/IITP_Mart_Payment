using UnityEngine;
using Valve.VR;

public class LeftHandController : MonoBehaviour
{
    public SteamVR_Input_Sources leftHand;

    public GameObject cellPhoneCanvas;

    bool Pressed = false;

    /**
     * @ Function : Left controller Function
     * 
     * @ Author : Minjung Kim
     * @ Date : 2020.Jun.07
     */
    void Update(){
       /* if (SteamVR_Input.GetStateDown("hint", leftHand)){
            Pressed = true;
            //M_EventLogger.EventLogging("현재 스테이지", GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "ON", "APPEARS");

        }else if (SteamVR_Input.GetStateUp("hint", leftHand)){
            Pressed = false;
            //M_EventLogger.EventLogging("현재 스테이지", GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "OFF", "DISAPPEARS");
        }

        if (Pressed == true){
            cellPhoneCanvas.SetActive(true);
            // 전화 받을 때까지 알림 울리게 할까
        }else{
            cellPhoneCanvas.SetActive(false);
        } */
    }
}
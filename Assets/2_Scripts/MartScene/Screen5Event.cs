using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;
using System.Reflection.Emit;

public class Screen5Event : MonoBehaviour
{
    int lang;
    string gameMode;

    public TMP_Text result_message;
    public Image result_background;

    public Canvas screen1;          // Initial screen for scanning
    public Canvas screen5;          // Current screen
    public Button screen1_btn_cancel;
    public Button screee1_btn_pay;
    public Button currentBtnObj;



    // to chagne the button color
    // @ mjk2072 
    //-------------------------------------------------
    ColorBlock btn;
    Color org_normalColor;
    Color highlightColor;
    //-------------------------------------------------

    protected virtual void Awake(){
        // Set initial button color
        btn = currentBtnObj.gameObject.GetComponent<Button>().colors;
        org_normalColor = btn.normalColor;
        highlightColor  = btn.highlightedColor;
    }

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        gameMode = GameObject.Find("v_gameMode").GetComponent<Text>().text;
    }
    
    /**
     * Change button color when the user touches button
     **/
    private void OnTriggerStay(Collider other){
        org_normalColor = Color.white;
        currentBtnObj.GetComponent<Image>().color = highlightColor;
    }

    /**
    * @ Function : Screen5
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.09
    * @ History :
    **/
    private void OnTriggerExit(Collider other) {

        string key = currentBtnObj.tag;

        // Add Event log
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, key, "screen5");

        // ----------------------------------
        // btn_previous
        // ----------------------------------
        if (key.Equals("btn_previous")){

            screen5.gameObject.SetActive(false);
            screen1.gameObject.SetActive(true);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "screen change", "screen5:screen5(pay) to screen1(home)");

            GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "N";
            GameObject.Find("v_current_canvas").GetComponent<Text>().text = "screen1";
            screen1_btn_cancel.GetComponent<Button>().interactable = true;
            screee1_btn_pay.GetComponent<Button>().interactable = true;
            screen1_btn_cancel.GetComponent<BoxCollider>().enabled = true;
            screee1_btn_pay.GetComponent<BoxCollider>().enabled = true;
        }
        currentBtnObj.GetComponent<Image>().color = org_normalColor;
    }

}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

/**
 * Screen3 is asking Screen for enter the discount code!
 */
public class Screen3Event : MonoBehaviour
{
    int lang;
    string gameMode;

    public Canvas screen1;          // Initial screen for scanning
    public TMP_Text screen1_result_message;
    public Image screen1_result_background;

    public Canvas screen3;          // For asking the discount code
    public Canvas screen4_test;     // Enter a discount code: test
    public Canvas screen4_easy;     // Enter a discount code: Level - easy
    public Canvas screen4_normal;   // Enter a discount code: Level - normal
    public Canvas screen4_hard;     // Enter a discount code: Level - hard
    public Canvas screen5;           // For making payment

    public GameObject currentBtnObj;    // Current button object

    public Text v_current_canvas; // Current Screen

    // To chagne the button color
    // @ mjk2072 
    //-------------------------------------------------
    ColorBlock btn;
    Color org_normalColor;
    Color highlightColor;
    //-------------------------------------------------

    void Awake(){
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
    * @ Function : Screen3(asknig that do you have a discount code?) interaction function
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.21
    * @ History :
    **/
    private void OnTriggerExit(Collider other){

        string tag = currentBtnObj.tag;
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        int item_counting = Int32.Parse(GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text);

        // ----------------------------------
        // BTN_YES
        // ----------------------------------
        if (tag.Equals("btn_yes")){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, tag, "Screen3:btn_yes()");
            Invoke("ChangeScreen3toScreen4After1s", 1f);

        // ----------------------------------
        // BTN_NO
        // ----------------------------------
        }else if (tag.Equals("btn_no")){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, tag, "Screen3:btn_no()");
            Invoke("ChangeScreen3toScreen1After1s", 1f);
        }
        currentBtnObj.GetComponent<Image>().color = org_normalColor;
    }

    /**
    * @ Function : Change Screen3 to Screen4(discount code)
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.21
    * @ History :
    *   2020.09.10 Add normal and test version
    **/
    private void ChangeScreen3toScreen4After1s() {
        v_current_canvas.text = "screen4";

        // Set screen for each levels
        string level = GameObject.Find("v_level").GetComponent<Text>().text;
        screen3.gameObject.SetActive(false);
        if (GlobalEnv.GAMEMODE_START.Equals(gameMode)) { 
            if (GlobalEnv.LEVEL_EASY.Equals(level)){
                screen4_easy.gameObject.SetActive(true);
            }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)){
                screen4_normal.gameObject.SetActive(true);
            }else if (GlobalEnv.LEVEL_HARD.Equals(level)){
                screen4_hard.gameObject.SetActive(true);
            }
        }else{
            screen4_test.gameObject.SetActive(true);
        }

        GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "Y"; // block scanning event when the user trying to pay or entering a discount code
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen3toScreen4After1s()", "Screen3(asking dc) to Screen4(ds)");
    }

    /**
    * @ Function : Change Screen3(asking) to Screen1(home)
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.21
    **/
    private void ChangeScreen3toScreen1After1s(){
        v_current_canvas.text = "screen1";

        // set message 
        screen1_result_message.text = LangText.alert_tryingToPay[lang];
        screen1_result_message.color = Color.blue;
        screen1_result_background.color = Color.white;
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCREEN_UPDATE, "screen1_result_message", "trying to pay");

        screen3.gameObject.SetActive(false);
        screen1.gameObject.SetActive(true);
        screen5.gameObject.SetActive(true);
        GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "Y";
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen3toScreen1After1s()", "Screen3(asking) to Screen1(home)");
    }
}
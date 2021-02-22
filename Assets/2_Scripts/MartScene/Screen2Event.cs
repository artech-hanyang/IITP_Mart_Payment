using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

/**
 * Screen2 is the remove Screen for uncheking an item!
 */
public class Screen2Event : MonoBehaviour
{
    int lang;
    string gameMode;

    public TextMeshProUGUI result_message;
    public Image result_background;

    public Canvas screen1;          // Initial screen for scanning
    public Canvas screen2;          // For unscanning(uncheck) an item

    public GameObject currentBtnObj;    // Current button object

    public Text v_current_canvas;

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
    * @ Function : Screen2(uncheking an tiem) interaction function
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.21
    * @ History :
    **/
    private void OnTriggerExit(Collider other){

        string tag = currentBtnObj.tag;
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        int item_counting = Int32.Parse(GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text);

        if (item_counting > 0) {

            // ----------------------------------
            // BTN_HOME
            // ----------------------------------
            if (tag.Equals("btn_home")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, tag, "Screen1(Home_screen):btn_pay()");
                v_current_canvas.text = "screen1";
                screen2.gameObject.SetActive(false);
                screen1.gameObject.SetActive(true);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "btnHome", "screen2:Screen2()cancel to Screen1(home)");
            }
        }
        else
        {
            // no item
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_DUP_REMOVE, "Removing", "-");
            result_message.text = LangText.screen1_noitem[lang];
            result_message.color = Color.red;
            result_background.color = Color.white;
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_RESULT_MSG, "result_message", "msg:" + LangText.screen1_noitem[lang]);
        }
        currentBtnObj.GetComponent<Image>().color = org_normalColor;
    }
}
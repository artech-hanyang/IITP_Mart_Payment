using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;

/**
 * Screen1 is Initial Screen for scanning!
 */
public class Screen1Event : MonoBehaviour
{
    int lang;
    string gameMode;

    public TextMeshProUGUI result_message;
    public Image result_background;

    public Canvas screen1;          // Initial screen for scanning
    public Canvas screen2;          // For unscanning(uncheck) an item
    public TextMeshProUGUI screen2_result_message;
    public Canvas screen3;          // For asking the discount code
    public Canvas screen5;          // Payment

    public GameObject currentBtnObj;    // Current button object

    public Text v_current_canvas;
    public Text v_discount_auth_yn;

    bool message_update = false;   // Message Update Yn

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
     * Check 
     **/
    void Update(){
        if (message_update == false && v_discount_auth_yn.text.Equals("Y")){
            screen5.gameObject.SetActive(true);
            result_message.text = LangText.alert_tryingToPay[lang];
            result_message.color = Color.blue;
            result_background.color = Color.white;
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCREEN_UPDATE, "result_message", "trying to pay");
            message_update = true;
        }
    }

    /**
     * Change button color when the user touches button
     **/
    private void OnTriggerStay(Collider other){
        org_normalColor = Color.white;
        currentBtnObj.GetComponent<Image>().color = highlightColor;
        string tag = currentBtnObj.tag;

        // M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCREEN_UPDATE, "Home_screen", LangText.alert_tryingToPay[lang]);
    }

    /**
    * @ Function : Screen1 interaction function
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.04.19
    * @ History :
    *   - 2020.04.19 Minjung KIM: Initial commit
    *   - 2020.05.03 Minjugn KIM: Remove checkbox interaction and Add cancel interacton
    *   - 2020.05.23 Minjung KIM: Add Event Log
    *   - 2020.06.01 Minjung KIM: Before payment, check item counting.
    *   - 2020.07.01 Minjung KIM: Add btn_discount_code button
    *   - 2020.07.21 Minjung KIM: Change button interaction 
    **/
    private void OnTriggerExit(Collider other){

        string tag = currentBtnObj.tag;
        int item_counting = Int32.Parse(GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text);
        if (item_counting > 0){

            // ----------------------------------
            // BTN_UNCHECK
            // ----------------------------------
            if (tag.Equals("btn_uncheck")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, tag, "Screen1(Home_screen):btn_uncheck()");

                v_current_canvas.text = "screen2";
                screen1.gameObject.SetActive(false);
                screen2.gameObject.SetActive(true);
                screen2_result_message.text = "-";
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen1toScreen2After1s()", "screen1:Screen1(home) to Screen2(cancel)");

            // ----------------------------------
            // BTN_PAY
            // ----------------------------------
            }else if (tag.Equals("btn_pay")){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, tag, "Screen1(Home_screen):btn_pay()");

                // block pay and uncheck button
                GameObject.Find("btn_pay").GetComponent<Button>().interactable      = false;
                GameObject.Find("btn_uncheck").GetComponent<Button>().interactable  = false;
                GameObject.Find("btn_pay").GetComponent<BoxCollider>().enabled      = false;
                GameObject.Find("btn_uncheck").GetComponent<BoxCollider>().enabled  = false;

                // if the user already check discount code, change the tyring to pay value.
                string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
                if (discount_auth_yn.Equals("Y"))
                {
                    screen5.gameObject.SetActive(true);
                    GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "Y";

                    result_message.text = LangText.alert_tryingToPay[lang];
                    result_message.color = Color.blue;
                    result_background.color = Color.white;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_RESULT_MSG, "result_message", "trying to pay");
                }
                else
                {
                    // Change screen1 to screen3 
                    Invoke("ChangeScreen1toScreen3After1s", 1f);
                }
            }

        }else{
            // no item
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_DUP_ADD, "Scanning", "-");
            result_message.text = LangText.screen1_noitem[lang];
            result_message.color = Color.red;
            result_background.color = Color.white;
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_RESULT_MSG, "screen1:result_message", LangText.screen1_noitem[lang]);
        }
        currentBtnObj.GetComponent<Image>().color = org_normalColor;
    }

    /**
    * @ Function : Change Screen1(initial screen) to Screen3(discount code screen)
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.21
    * @ History :
    **/
    private void ChangeScreen1toScreen3After1s(){
        v_current_canvas.text = "screen3";
        screen1.gameObject.SetActive(false);
        screen3.gameObject.SetActive(true);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen1toScreen3After1s()", "screen1:Screen1(home) to Screen3(asking_dc)");
    }
}
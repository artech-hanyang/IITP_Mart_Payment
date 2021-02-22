using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardEvent : MonoBehaviour
{
    int lang;
    public TMP_Text result_message;
    public Image result_background;

    public Canvas screen1; // Initial screen for scanning
    public Canvas screen5; // Payment Screen
    public Canvas screen6; // End Screen

    public Text trying_to_pay_yn;
    public TMP_Text total_amount;

    bool card_tag = false;

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
    }

    /**
     * @Function: Card Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.25 
     * @History: 
     *  - 2020.05.25 Minjung KIM: initial commit
     *  - 2020.05.29 Minjung KIM: ADD credit exceeded case
     *  - 2020.06.16 Minjung KIM: Block duplicate tagging 
     *  - 2020.07.01 Minjung KIM: Add discount_yn
     *  - 2020.07.03 Minjung KIM: Add sound effect
     */
    void OnTriggerExit(Collider collision){
        int total = Int32.Parse(total_amount.text);
        string tag = collision.gameObject.tag;
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        string trying_to_pay = GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text;

        if (card_tag == false && !tag.Equals("Untagged") && tag.Equals("card") && trying_to_pay.Equals("Y")){
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_PAY, tag, "discount_yn:"+ discount_auth_yn);
            card_tag = true;

            // -----------------------
            // FAIL
            // ----------------------- 
            if (total > GlobalEnv.ReturnBudget(lang.ToString())){
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_EXCEEDED, total.ToString(), "discount_yn:" + discount_auth_yn);

                result_message.text = LangText.alert_err[lang];
                result_message.color = Color.red;
                result_background.color = Color.yellow;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_RESULT_MSG, total.ToString(), "discount_yn:" + discount_auth_yn);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                
                card_tag = false;
                screen5.gameObject.SetActive(false);

                // release button enabled option
                GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "N";
                GameObject.Find("btn_pay").GetComponent<Button>().interactable = true;
                GameObject.Find("btn_uncheck").GetComponent<Button>().interactable = true;
                GameObject.Find("btn_pay").GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("btn_uncheck").GetComponent<BoxCollider>().enabled = true;

            // -----------------------
            // SUCC
            // -----------------------
            }else{
                GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text = "N";
                screen5.gameObject.SetActive(false);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_END, total.ToString(),"discount_yn:" + discount_auth_yn);
                
                result_message.text = LangText.alert_succ[lang];
                result_message.color = Color.blue;
                result_background.color = Color.white;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_RESULT_MSG, total.ToString(), "discount_yn:" + discount_auth_yn);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);

                Invoke("ChangeScreen1toScreen5After2s", 2f);
            }
        }
    }

    private void ChangeScreen1toScreen5After2s(){
        screen1.gameObject.SetActive(false);
        screen5.gameObject.SetActive(false);
        screen6.gameObject.SetActive(true);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "card:CallEndingScneneAfter5f", "(SUCC) End of Game!");
        Invoke("CallEndingScneneAfter5f", 5f);
    }

    private void CallEndingScneneAfter5f() { 
        SceneManager.LoadScene("5_EndScene");
    }
}

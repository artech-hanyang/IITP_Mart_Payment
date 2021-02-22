using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using TMPro;
using System.Reflection.Emit;

public class Screen4Event : MonoBehaviour
{
    int lang;
    string gameMode;

    public TMP_Text result_message;
    public Image result_background;

    public Canvas screen1;          // Initial screen for scanning
    public Canvas screen4;          // Enter a discount code: current discount code canvas
    public Canvas screen5;          // Alert noty - making payment
    public Button currentBtnObj;
    public TMP_InputField input_discount_code;
    public TMP_Text v_discount;
    public TMP_Text screen1_total_amount;

    public Text v_errors;
    int errors;

    float touchDuration = 0.4f;
    float time = 0f;
    Boolean touch = false;

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
        time = touchDuration;
    }

    void Update(){
        
        // Block double touch
        if (time > 0 && touch == false){
            time -= Time.deltaTime;
            if (time <= 0) {
                touch = true;
                time = touchDuration;
            }
        }

    }
    
    /**
     * Change button color when the user touches button
     **/
    private void OnTriggerStay(Collider other){
        org_normalColor = Color.white;
        currentBtnObj.GetComponent<Image>().color = highlightColor;
    }

    /**
    * @ Function : Screen4 Interaction for discount code
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.04.19
    * @ History :
    *   - 2020.04.19 Minjung Kim : Initial commit
    *   - 2020.05.03 Minjugn Kim : Remove checkbox interaction and Add cancel interacton
    *   - 2020.05.23 Minjung Kim : Add Event Log
    *   - 2020.06.16 Minjung Kim : Clear the disocunt code input field
    **/
    private void OnTriggerExit(Collider other) {

        if(touch == true){
            touch = false;

            string key = currentBtnObj.tag;

            // Add Event log
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_BTN_TOUCH, key, "");

            // ----------------------------------
            // BTN_BACK
            // ----------------------------------
            if (key.Equals("btn_back")){

                string org_text = input_discount_code.text;
                if (org_text.Length > 0){
                    string new_text = org_text.Substring(0, org_text.Length - 1);
                    input_discount_code.text = new_text;
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCREEN_UPDATE, "input_discount_code", new_text);
                }

            // ----------------------------------
            // BTN_APPLY
            // ----------------------------------
            }else if (key.Equals("btn_apply")){

                result_message.text = "";
                int v_dc = GlobalEnv.ReturnDiscountPrice(lang);
                v_discount.text = v_dc.ToString();

                int total = Int32.Parse(screen1_total_amount.text);

                // ----------------------------------
                // REAL MODE
                // ----------------------------------
                if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){

                    // ----------------------------------
                    // CHECKING DISCOUNT CODE
                    // ----------------------------------
                    string v_level = GameObject.Find("v_level").GetComponent<Text>().text;
                    if (GlobalEnv.DISCOUNT_CODE_EASY.Equals(input_discount_code.text) 
                        || GlobalEnv.DISCOUNT_CODE_NORMAL.Equals(input_discount_code.text) 
                        || GlobalEnv.DISCOUNT_CODE_HARD.Equals(input_discount_code.text)){

                        screen1_total_amount.text = (total - v_dc).ToString();
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CODE_SUCC, (total - v_dc).ToString(), "screen4");

                        result_message.text = LangText.screen4_succ_dc[lang];
                        result_message.color = Color.blue;
                        result_background.color = Color.white;
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_CODE_SUCC, "result_message", "screen4:"+ LangText.screen4_succ_dc[lang]);
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);

                        Invoke("ChangeScreen4toScreen1After1s", 1f);
                
                    }else{
                        input_discount_code.text = ""; // clear the input field
                        errors += 1;
                        v_errors.text = errors.ToString();
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CODE_FAIL, input_discount_code.text, "errors:" + errors);

                        // If the user does 3 errors, they will receive a full discount code.
                        result_message.text = LangText.screen4_fail_dc[lang];
                        result_message.color = Color.red;
                        result_background.color = Color.yellow;
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_CODE_FAIL, "result_message", "screen4:" + LangText.screen4_fail_dc[lang]);
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);

                    }
                
                // ----------------------------------
                // TEST MODE
                // ----------------------------------
                } else {
                    if (GlobalEnv.DISCOUNT_CODE_TEST.Equals(input_discount_code.text)){
                        screen1_total_amount.text = (total - GlobalEnv.ReturnDiscountPrice(lang)).ToString();
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CODE_SUCC, (total - v_dc).ToString(), "screen4");

                        result_message.text = LangText.screen4_succ_dc[lang];
                        result_message.color = Color.blue;
                        result_background.color = Color.white;
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_CODE_SUCC, "result_message", "screen4: " + LangText.screen4_succ_dc[lang]);
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_SUCC, lang);

                        Invoke("ChangeScreen4toScreen1After1s", 1f);
                
                    }else{
                        input_discount_code.text = ""; // clear the input field
                        errors += 1;
                        v_errors.text = errors.ToString();
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CODE_FAIL, input_discount_code.text, "errors:" + errors);

                        // If the user does 3 errors, they will receive a full discount code.
                        result_message.text = LangText.screen4_fail_dc[lang];
                        result_message.color = Color.red;
                        result_background.color = Color.yellow;
                        SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_CODE_FAIL, "result_message", "screen4:" + LangText.screen4_fail_dc[lang]);
                    }
                }

            // ----------------------------------
            // KEYBOARD BUTTON
            // ----------------------------------
            }else if (key.Equals("Key_1")){
                input_discount_code.text += "1";
            }else if (key.Equals("Key_2")){
                input_discount_code.text += "2";
            }else if (key.Equals("Key_3")){
                input_discount_code.text += "3";
            }else if (key.Equals("Key_4")){
                input_discount_code.text += "4";
            }else if (key.Equals("Key_5")){
                input_discount_code.text += "5";
            }else if (key.Equals("Key_6")){
                input_discount_code.text += "6";
            }else if (key.Equals("Key_7")){
                input_discount_code.text += "7";
            }else if (key.Equals("Key_8")){
                input_discount_code.text += "8";
            }else if (key.Equals("Key_9")){
                input_discount_code.text += "9";
            }else if (key.Equals("Key_0")){
                input_discount_code.text += "0";
            }else if (key.Equals("Key_A")){
                input_discount_code.text += "A";
            }else if (key.Equals("Key_B")){
                input_discount_code.text += "B";
            }else if (key.Equals("Key_C")){
                input_discount_code.text += "C";
            }else if (key.Equals("Key_D")){
                input_discount_code.text += "D";
            }else if (key.Equals("Key_E")){
                input_discount_code.text += "E";
            }else if (key.Equals("Key_F")){
                input_discount_code.text += "F";
            }else if (key.Equals("Key_G")){
                input_discount_code.text += "G";
            }else if (key.Equals("Key_H")){
                input_discount_code.text += "H";
            }else if (key.Equals("Key_I")){
                input_discount_code.text += "I";
            }else if (key.Equals("Key_J")){
                input_discount_code.text += "J";
            }else if (key.Equals("Key_K")){
                input_discount_code.text += "K";
            }else if (key.Equals("Key_L")){
                input_discount_code.text += "L";
            }else if (key.Equals("Key_M")){
                input_discount_code.text += "M";
            }else if (key.Equals("Key_N")){
                input_discount_code.text += "N";
            }else if (key.Equals("Key_O")){
                input_discount_code.text += "O";
            }else if (key.Equals("Key_P")){
                input_discount_code.text += "P";
            }else if (key.Equals("Key_Q")){
                input_discount_code.text += "Q";
            }else if (key.Equals("Key_R")){
                input_discount_code.text += "R";
            }else if (key.Equals("Key_S")){
                input_discount_code.text += "S";
            }else if (key.Equals("Key_T")){
                input_discount_code.text += "T";
            }else if (key.Equals("Key_U")){
                input_discount_code.text += "U";
            }else if (key.Equals("Key_V")){
                input_discount_code.text += "V";
            }else if (key.Equals("Key_W")){
                input_discount_code.text += "W";
            }else if (key.Equals("Key_X")){
                input_discount_code.text += "X";
            }else if (key.Equals("Key_Y")){
                input_discount_code.text += "Y";
            }else if (key.Equals("Key_Z")){
                input_discount_code.text += "Z";
            }
        }
        currentBtnObj.GetComponent<Image>().color = org_normalColor;
    }

    /**
        * @ Function : Change Screen4 to Screen1 
        * 
        * @ Author : Minjung KIM
        * @ Date : 2020.07.21
        * @ History :
        *   - 2020.09.23 Minjung Kim : After discount code, apears payment alert
        **/
    void ChangeScreen4toScreen1After1s(){
        GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text = "Y";
        GameObject.Find("v_current_canvas").GetComponent<Text>().text = "screen5";

        screen4.gameObject.SetActive(false);
        screen1.gameObject.SetActive(true);
        screen5.gameObject.SetActive(true); // 결제를 시도하세요 페이지 보여주기
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen4toScreen1After1s()", "screen4:Screen4(dc) to Screen1(home)");
    }
}
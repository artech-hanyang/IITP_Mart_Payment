using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroEvent : MonoBehaviour
{
    int lang;
    public Text gameMode;
    string user_id;
    public TMP_Text Instruction;
    public TMP_Text btn_txt_trial;
    public TMP_Text btn_text_start;

    void Awake(){
        Screen.SetResolution(2160, 1080, true);
    }

    void Start(){
        try{
            lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
            user_id = GameObject.Find("v_name").GetComponent<Text>().text;
        }catch (Exception e){
            user_id = "TESER";
            lang = 2;
        }

        // set lang
        Instruction.text    = LangText.introText_instruction[lang];
        btn_txt_trial.text  = LangText.introButton_test[lang];
        btn_text_start.text = LangText.introButton_start[lang];

        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "scene_name:IntroEvent.cs");
    }
    
    public void BtnSetting(){
        Debug.Log("Scene Change: AdminScene");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "introEvent:btn_setting", "-");
        SceneManager.LoadScene("0_AdminScene");
    }

    public void BtnTrial(){
        Debug.Log("Scene Change: InstructionScene");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "introEvent:btn_trial", "-");
        if (user_id.Length > 0){
            gameMode.text = GlobalEnv.GAMEMODE_TEST;
            SceneManager.LoadScene("2_InstructionScene");
        }
    }

    public void BtnStart(){
        Debug.Log("Scene Change: GoalScene");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "introEvent:btn_start", "Goal Scene!!");
        if (user_id.Length > 0){
            gameMode.text = GlobalEnv.GAMEMODE_START;
            SceneManager.LoadScene("GoalScene");
        }
    }
}

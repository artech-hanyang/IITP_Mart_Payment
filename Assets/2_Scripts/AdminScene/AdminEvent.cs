using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AdminEvent : MonoBehaviour
{
    int lang;
    bool langUpdate = false;
    public Text v_name;
    public TMP_InputField input_name; 
    public Text v_level;

    // lang setting
    public TextMeshProUGUI text_lang;
    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_level;
    public ToggleGroup toggleGroup;
    public Text text_easy;
    public Text text_normal;
    public Text text_hard;
    public TextMeshProUGUI text_btn_home;

    void Awake(){
        Screen.SetResolution(2160, 1080, true);
    }

    void Update(){

        // Update lang setting
        if(langUpdate == true){
            lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
            text_lang.text      = LangText.adminText_lang[lang];
            text_name.text      = LangText.adminText_name[lang];
            text_level.text     = LangText.adminText_level[lang];
            text_easy.text      = LangText.adminToggle_easy[lang];
            text_normal.text    = LangText.adminToggle_normal[lang];
            text_hard.text      = LangText.adminToggle_hard[lang];
            text_btn_home.text  = LangText.adminButton_home[lang];
            langUpdate = false;
        }
    }

    /**
     * @Function: Set Langauge
     * @Author: Minjung KIM
     */
    public void SetKR(){
        GameObject.Find("v_lang").GetComponent<Text>().text = GlobalEnv.KR;
        langUpdate = true;
    }
    public void SetFR(){
        GameObject.Find("v_lang").GetComponent<Text>().text = GlobalEnv.FR;
        langUpdate = true;
    }
    public void SetEN(){
        GameObject.Find("v_lang").GetComponent<Text>().text = GlobalEnv.EN;
        langUpdate = true;
    }

    /**
     * @Function: Set Levels of difficulty
     * @Author: Minjung KIM
     */
    public void BtnLevels(){
        foreach(Toggle toggle in toggleGroup.ActiveToggles()){
            if(toggle.name.Equals("Easy")){
                v_level.text = "0";
            }else if(toggle.name.Equals("Normal")){
                v_level.text = "1";
            }else if(toggle.name.Equals("Hard")){
                v_level.text = "2";
            }
        }
    }

    /**
     * @Function: Btn Home
     * @Author: Minjung KIM
     */
    public void BtnHome(){
        if (input_name.text.Length > 0){
            v_name.text = input_name.text;
            M_EventLogger.createFile(input_name.text);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "AdminEvent.cs");
            SceneManager.LoadScene("1_IntroScene");
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndEvent : MonoBehaviour
{
    int lang;

    public TMP_Text txt_finish_ment;

    // Start is called before the first frame update
    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        txt_finish_ment.text = LangText.ending_ment[lang];
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "START()", "scene_name:EndEvent.cs");
    }

    public void BtnIntro() {
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_CLICK, "EndEvent:btn_intro", "scene_name:EndEvent.cs");
        SceneManager.LoadScene("0_AdminScene");
    }
}

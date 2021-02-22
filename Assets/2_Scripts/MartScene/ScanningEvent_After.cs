using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/*
 * 사용하지 않음
 */
public class ScanningEvent_After : MonoBehaviour
{
    public TextMeshProUGUI result_message;
    private Dictionary<string, string> after_list = new Dictionary<string, string>();

    int itemCount;
    int lang;
    bool text_update = false;

    void Start(){
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        string gameMode =  GameObject.Find("v_gameMode").GetComponent<Text>().text;
        if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){
            itemCount = 5; 
        }else{
            itemCount = 2;
        }
    }

    void Update(){
        // If All items are placed on the left table, the result message appears.
        if(text_update == false && after_list.Count > itemCount){
            text_update = true;
            result_message.text = LangText.screen1_after[lang];
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_RESULT_MSG, "Message Appears", "[AFTER] :" + LangText.screen1_after[lang]);
        }
    }

    /**
     * @Function: After check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.May.06
     * @History: 
     *  - 2020.05.23 Minjung Kim : Add Event Log
     */
    void OnTriggerEnter(Collider other){
        string o_item_code = other.gameObject.name;  // unique item code: ex) A1
        string o_item_name = other.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && !after_list.ContainsKey(o_item_code)){
                Debug.Log("AfterScanning onEnter()   o_item_name   :" + o_item_name + ",("+ o_item_code + ")");

                text_update = false;
                after_list.Add(o_item_code, o_item_name);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_AFTER_ENTER, o_item_name, "item_code:"+o_item_code);
            }
        }catch{

        }
    }
    
    /**
     * @Function: After check Method
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Jun.25
     */
     void OnTriggerExit(Collider other){
        string o_item_code = other.gameObject.name;  // unique item code: ex) A1
        string o_item_name = other.gameObject.tag;   // item name: ex) Apple

        try{
            if (!o_item_name.Equals("Untagged") && after_list.ContainsKey(o_item_code)){
                Debug.Log("AfterScanning onExit()   o_item_name   :" + o_item_name + ",("+ o_item_code + ")");

                after_list.Remove(o_item_code);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_AFTER_EXIT, o_item_name, "item_code:"+o_item_code);
            }
        }catch{

        }
    }
}

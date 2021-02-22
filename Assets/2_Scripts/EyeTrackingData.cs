using System.Collections;
using UnityEngine;
using Tobii.XR;
using Tobii.G2OM;
using System;
using TMPro;
using System.Data;

public class EyeTrackingData : MonoBehaviour, IGazeFocusable
{
    float timeSpan;
    float checkTime;

    bool focused = false;
    String object_name = "";
    ArrayList list = new ArrayList();
    string dt = "";

    void Awake()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
    }

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //This object either received or lost focused this frame, as indicated by the hasFocus parameter.
        focused = hasFocus;
    }
    void start()
    {
        timeSpan = 0.0f;
        checkTime = 0.3f;
    }


    /**
     * @Function: Eye-Tracking Data
     * 
     * @Author: Minjung KIM
     * @Date: 2020.07.15
     * @History: 
     */
    void Update()
    {
        timeSpan += Time.deltaTime;

        // ----------------
        // hasFocus
        // ----------------
        if (focused){

            // Check whether Tobii XR has any focused objects.
            if (TobiiXR.FocusedObjects.Count > 0){

                // The object being focused by the user, determined by G2OM.
                string focusedObjectName = TobiiXR.FocusedObjects[0].GameObject.name;
                object_name = focusedObjectName;
                dt = DateTime.Now.ToString("hh:mm:ss.ffff");

                if (list.Count == 0){
                    list.Add(dt);
                }else{
                    // 0.3f > 
                    if (timeSpan > checkTime){
                        list.Add(dt);
                    }
                }
            }

        // ----------------
        // loses focus
        // ----------------
        }else{

            int last_idx = list.Count - 1;
            if (list.Count > 1){
                string event_category_type = GlobalEnv.EVENT_TYPE_ACTION_EYE_DETECHTED_ITEM;

                // Add screen item name
                string child_list_item_name = "-";
                if (object_name.Equals("L1") || object_name.Equals("L2") || object_name.Equals("L3") 
                    || object_name.Equals("L4") || object_name.Equals("L5") || object_name.Equals("L6") || object_name.Equals("L7"))
                {
                    string list_idx = object_name.Substring(1);
                    child_list_item_name = GameObject.Find("item_name" + list_idx).GetComponent<TextMeshProUGUI>().text;
                    object_name = child_list_item_name;
                    event_category_type = GlobalEnv.EVENT_TYPE_ACTION_EYE_DECISION;
                }

                /* Add keyboard button name - todo: btn_으로 시작하면서 4자리인 것이면 BUTTON 태그 달기 
                string object_name_str = object_name.Split(0, 2);
                string object_name_cnt = object_name.Length;
                print("object_name:" + object_name);
                if (object_name_str.Equals("btn")&& object_name_cnt == 4)
                {
                    event_category_type = GlobalEnv.EVENT_TYPE_ACTION_EYE_DETECHTED_BTN;
                } */

                // Calculate the time span
                DateTime StartDate = System.Convert.ToDateTime(list[0]);
                DateTime EndDate = System.Convert.ToDateTime(list[last_idx]);
                TimeSpan eyeTracking_timeSpan = EndDate - StartDate;
                double timeCalSec = eyeTracking_timeSpan.TotalSeconds;

                if (timeCalSec >= 0.3)
                {
                    M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_EYE_TRACKING, event_category_type, object_name, timeCalSec.ToString());
                }

                object_name = "";
                timeSpan = 0;
                list.Clear();
            }
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class PrepareEvent : MonoBehaviour
{
    int lang;

    // Start is called before the first frame update
    void Start()
    {
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCENE, GlobalEnv.EVENT_TYPE_START, "Start()", "PrepareEvent.cs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

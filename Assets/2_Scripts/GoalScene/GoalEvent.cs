using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalEvent : MonoBehaviour
{
    void Start()
    {
        Debug.Log("GoalMain Event.cs");
        Invoke("StartMainContent", 35f);
    }

    public void StartMainContent()
    {
        Debug.Log("Scene Change: MartScene");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "introEvent:btn_start", "CONTENS_START!!"); 
        SceneManager.LoadScene("4_MartScene");
    }
}

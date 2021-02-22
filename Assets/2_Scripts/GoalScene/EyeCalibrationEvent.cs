using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EyeCalibrationEvent : MonoBehaviour
{

    public void StartTestMode()
    {
        Debug.Log("Scene Change: MartScene");
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_CLICK, "introEvent:StartTestMode", "TEST_SCART!!");
        SceneManager.LoadScene("4_MartScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValidationChk : MonoBehaviour
{

    // -------------------------
    // Check
    // -------------------------
    public static bool ChkGameMode_isAdmin()
    {
        bool isAdmin = false;
        try
        {
            string v_gameMode = GameObject.Find("v_gameMode").GetComponent<Text>().text;
            if (v_gameMode.Equals(GlobalEnv.GAMEMODE_ADMIIN))
            {
                isAdmin = true;
            }
        }
        catch
        {
            isAdmin = false;
        }

        return isAdmin;
    }
}

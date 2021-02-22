using System;
using System.IO;
using System.Text;
using UnityEngine;

public class M_EventLogger: MonoBehaviour { 
    private static readonly object innerLock = new object();

    // set File name
    private static string fileName;
    
    /**
     * @Function: get file path! for logging 
     * 
     * @Author: Minjung KIM
     * @Date: 2020.03.13
     * @History: 
     *  - 2020.03.13 Minjung KIM: initial commit
     *  - 2020.03.28 Minjung KIM: ADD Platform check (I want to set my log path)
     *  - 2020.05.29 Minjung KiM: Change log path to /Assets/Resources/Log/
     */
    public static string GetFilePath()
    {
        String filePath = Environment.CurrentDirectory + "/Assets/Resources/Log/";

        // String[] orgPath = null;
        // if (Application.platform == RuntimePlatform.OSXEditor)
        // {
        //     orgPath = Application.dataPath.Split('/');
        //     filePath = "/" + orgPath[1] + "/" + orgPath[2] + "/Desktop/IITP_CSV/";
        // }
        // else if(Application.platform == RuntimePlatform.WindowsEditor)
        // {
        //     orgPath = Application.dataPath.Split('/');
        //     filePath = "/" + orgPath[1] + "/" + orgPath[2] + "/Desktop/IITP_CSV/";
        // }

        return filePath;
    }

    /**
     * @Function: Create File by user_name
     * 
     * @Author: Minjung Kim
     * @Date: 2020.05.23
     */
    public static void createFile(
        string user_name
    ){
        Debug.Log(GetFilePath() + fileName);
        if (!File.Exists(GetFilePath()))
        {
            fileName = GetFilePath() + DateTime.Now.ToString("yyMMdd_HHMM") +"_"+ user_name + "_User_Log.csv";
            File.AppendAllText(fileName, "AppTime,Actor,Event_Category,Event_Type,Object,Attributes\n", Encoding.UTF8);
        }
    }
    
    /**
     * @Function: Event Logging 
     * 
     * @Author: Minjung KIM
     * @Date: 2020.05.23
     * @Description: https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
        1. Event_Category
            - ACTION 
            - SYSTEM_MESSAGE
            - SCENE
        2. Event_Type
            - Grab / Release
            - Scan add / remove
            - Notification
            - Code Fail / Succ
            - PAY
     
     * @History: 
     *  - 2020.05.23 Minjung KIM: initial commit
     *  - 2020.07.08 Minjung KIM: Check Admin Mode 
     * 
     */
    public static void EventLogging(
        string actor            // SYSTEM, USER
        , string event_category // GlobalEvn.EVENT_CATE.. ex) ACTION, MESSAGE, SYS_MESSAGE, SCENE_CHAGE
        , string event_type     // GlobalEvn.EVENT_TYPE..
        , string object_name    // unique item code
        , string attributes     // JSON Format or {1};{2};
    ){
        if (!ValidationChk.ChkGameMode_isAdmin())
        {
            try
            {
                lock (innerLock)
                {
                    File.AppendAllText(
                        fileName
                        , string.Format("{0},{1},{2},{3},{4},{5}", Time.realtimeSinceStartup, actor, event_category, event_type, object_name, attributes) + "\n"
                        , Encoding.UTF8
                    );
                }
            }
            catch
            {
                createFile("TEST_FILE_");
            }

        }
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class NotificationEvent : MonoBehaviour
{
    int lang;
    string gameMode;
    string level;

    public SteamVR_Input_Sources leftHand;
    public SteamVR_Action_Vibration hapticAction;

    public Material screen;
    public Sprite basic;

    Sprite screen_calling1;
    Sprite screen_calling2;
    public Sprite calling1_kr;
    public Sprite calling1_fr;
    public Sprite calling1_en;
    public Sprite calling2_kr;
    public Sprite calling2_fr;
    public Sprite calling2_en;

    Sprite screen_message;
    public Sprite message_kr_test;
    public Sprite message_kr_easy;
    public Sprite message_kr_normal;
    public Sprite message_kr_hard;
    public Sprite message_en_test;
    public Sprite message_en_easy;
    public Sprite message_en_normal;
    public Sprite message_en_hard;
    public Sprite message_fr_test;
    public Sprite message_fr_easy;
    public Sprite message_fr_normal;
    public Sprite message_fr_hard;

    public GameObject subtitle;

    /* Types of Messages
    - 1st message: Discount Code (Phone call) and Budget information
    - 2nd message: (If the user failed 3 times) Full discount code
    - Finished: Completed all notification */
    NotificationType notificationType;
    enum NotificationType { First, Second, Finished };
    float hintDuration = 0; // All types of messages are initially shows for few seconds. => Todo. 없애기
    float time = 0;

    void Start(){
        notificationType = NotificationType.First;
        gameMode   = GameObject.Find("v_gameMode").GetComponent<Text>().text;
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        level = GameObject.Find("v_level").GetComponent<Text>().text;

        // Set Sprite Image
        // Todo- 게임 레벨에 따른 사운드 길이 지정을 해주어야 함
        if (GlobalEnv.GAMEMODE_START.Equals(gameMode)){
            if (GlobalEnv.KR.Equals(lang.ToString())){
                screen_calling1 = calling1_kr;
                screen_calling2 = calling2_kr;
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    screen_message = message_kr_easy;
                    time = 20;

                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) { 
                    screen_message = message_kr_normal;
                    time = 22;

                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) { 
                    screen_message = message_kr_hard;
                    time = 24;
                }
            }else if (GlobalEnv.FR.Equals(lang.ToString())){
                time = 10;
                screen_calling1 = calling1_fr;
                screen_calling2 = calling2_fr;
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    screen_message = message_fr_easy;
                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) { 
                    screen_message = message_fr_normal;
                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) { 
                    screen_message = message_fr_hard;
                }
            }else if (GlobalEnv.EN.Equals(lang.ToString())){
                time = 15;
                screen_calling1 = calling1_en;
                screen_calling2 = calling2_en;
                if (GlobalEnv.LEVEL_EASY.Equals(level)) {
                    screen_message = message_en_easy;
                }else if (GlobalEnv.LEVEL_NORMAL.Equals(level)) { 
                    screen_message = message_en_normal;
                }else if (GlobalEnv.LEVEL_HARD.Equals(level)) { 
                    screen_message = message_en_hard;
                }

            }
        }else {
            time = 5;
            if (GlobalEnv.KR.Equals(lang.ToString())){
                screen_calling1 = calling1_kr;
                screen_calling2 = calling2_kr;
                screen_message = message_kr_test;
            }else if (GlobalEnv.FR.Equals(lang.ToString())){
                screen_calling1 = calling1_fr;
                screen_calling2 = calling2_fr;
                screen_message = message_fr_test;

            }else if (GlobalEnv.EN.Equals(lang.ToString())){
                screen_calling1 = calling1_en;
                screen_calling2 = calling2_en;
                screen_message = message_en_test;
            }

        }
    }

    /**
     * @ Function : Notification Function
     * 
     * @ Author : Minjung Kim
     * @ Date : 2020.Jun.07
     * @ History :
     *   - 2020.04.03 Euisung Kim: 최초 작성
     *   - 2020.04.10 Minjung KIM: Modify Function structure and Add disappear message function
     *   - 2020.05.23 Minjung KIM: Add Event Log
     *   - 2020.05.31 Minjung KIM: Add Function Comment 
     *   - 2020.06.17 Minjung Kim: Update screen materials
     *   - 2020.06.27 Minjung Kim: Add gameMode check
     **/
    void Update()
    {
        GameObject phoneObj = null;
        try{
            phoneObj = GameObject.FindGameObjectWithTag("left_cellphone");
        }catch (Exception e){
            Debug.Log("없으면 재시작해야합니다. 로딩까지 좀 걸리지");
        }

        // This function starts when the hint duration is bigger than 0
        if (hintDuration > 0){
            hintDuration -= Time.deltaTime;
            if (hintDuration <= 0){
                Debug.Log("[Notification: Finished]");
                // cellPhoneCanvas.SetActive(false);
                screen.SetTexture("_EmissionMap", basic.texture);
                subtitle.SetActive(false);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "Call_subtitle", "DISAPPEARS");
            }
        }

        // ----------------------------------
        // 1st phone calling, when the user scans first item 
        // Content: Seding a Discount Code and budget infomation.
        // ----------------------------------
        string v_first_scan_yn = GameObject.Find("v_first_scan_yn").GetComponent<Text>().text;
        if (notificationType.Equals(NotificationType.First) & v_first_scan_yn.Equals("Y")){

            if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){
                Debug.Log("[Notification: Phone Calling], [" + gameMode + "]");
                CallHapticAction(0, 2, 150, 75, leftHand);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_CALL, lang);
                screen.SetTexture("_EmissionMap", screen_calling1.texture);
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "Phone Calling", "Calling");
                Invoke("ChangePhoneScreen", 1.5f);
                notificationType = NotificationType.Second;
                hintDuration = time;
            }
        }

        // ----------------------------------
        // 2nd text message that when the user does 3 erros to enter a discont code
        // Content: Sending a full discount code.
        // ----------------------------------
        int v_discount_errors = Int32.Parse(GameObject.Find("v_discount_errors").GetComponent<Text>().text);
        if (notificationType.Equals(NotificationType.Second) && v_discount_errors >= 3){

            Debug.Log("[Notification: Message], [" + gameMode + "]");
            CallHapticAction(0, 2, 150, 75, leftHand);
            SoundManager.instance.PlaySound(GlobalEnv.SOUND_MESSAGE, lang);
            screen.SetTexture("_EmissionMap", screen_message.texture);
            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "Phone Message", "Sent a discount code (User failed to enter the discount code)");
            notificationType = NotificationType.Finished;
        }
    }

    void ChangePhoneScreen(){
        screen.SetTexture("_EmissionMap", screen_calling2.texture);
        SoundManager.instance.PlaySound(GlobalEnv.SOUND_VOCAL, lang);
        subtitle.SetActive(true);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_NOTI, "Call_subtitle", "APPEARS");
    }

    /**
     * @ Function : HapticAction Method For Test
     * 
     * @ Author : Minjung Kim
     * @ Date : 2020.Jun.27
     * @ History :
     *  - To ignore the error message(Without VR Controller)
     */
    void CallHapticAction(
        float secondsFromNow
        , float durationSeconds
        , float frequency
        , float amplitude
        , SteamVR_Input_Sources inputSource
    ){
        try{
            hapticAction.Execute(0, 2, 150, 75, leftHand);
        }catch(Exception e){
            Debug.Log("Haption Error");
        }
    }
}
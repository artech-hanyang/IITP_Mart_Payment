using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEnv : MonoBehaviour
{
    // -------------------------
    // LANG
    // -------------------------
    public static string GAMEMODE_TEST = "TEST";
    public static string GAMEMODE_START = "START";
    public static string GAMEMODE_ADMIIN = "ADMIN";

    // -------------------------
    // LANG
    // check: LangText.ccs
    // -------------------------
    public static string KR = "0";
    public static string FR = "1";
    public static string EN = "2";

    // -------------------------
    // Levels
    // -------------------------
    public static string LEVEL_TEST = "TEST";
    public static string LEVEL_EASY = "0";
    public static string LEVEL_NORMAL = "1";
    public static string LEVEL_HARD = "2";

    // -------------------------
    // For Discount Code
    // -------------------------
    public static string DISCOUNT_CODE_TEST = "AA1";
    public static string DISCOUNT_CODE_EASY = "KA3";
    public static string DISCOUNT_CODE_NORMAL = "QX07";
    public static string DISCOUNT_CODE_HARD = "ZKJ50";

    public static string ReturnDiscountCode(
        string level
    )
    {
        string discount_code = DISCOUNT_CODE_TEST;
        if (LEVEL_EASY.Equals(level)){
            discount_code =  DISCOUNT_CODE_EASY;
        }else if (LEVEL_NORMAL.Equals(level)){
            discount_code = DISCOUNT_CODE_NORMAL;
        }else if (LEVEL_HARD.Equals(level)){
            discount_code = DISCOUNT_CODE_HARD;
        }
        return discount_code;
    }

    // -------------------------
    // Item price
    // -------------------------
    public static string item_tomato_price = "2";
    public static string item_baguette_price = "10";
    public static string item_cheese_price = "4";
    public static string item_cabbage_price = "8";
    public static string item_coffee_price = "6";
    public static string item_pumpkin_price = "6";
    public static string item_juice_price = "2";
    public static string item_apple_price = "10";
    public static string item_chip_price = "50";

    public static string ReturnItemPrice(
        string item_name
        , int lang
    )
    {
        string o_item_price = "0";
        string p_lang = lang.ToString();

        switch (item_name)
        {
            case "Tomato":
                if (p_lang.Equals(KR)) { o_item_price = item_tomato_price + "000"; } else { o_item_price = item_tomato_price; }
                break;
            case "Baguette":
                if (p_lang.Equals(KR)) { o_item_price = item_baguette_price + "000"; } else { o_item_price = item_baguette_price; }
                break;
            case "Cheese":
                if (p_lang.Equals(KR)) { o_item_price = item_cheese_price + "000"; } else { o_item_price = item_cheese_price; }
                break;
            case "Cabbage":
                if (p_lang.Equals(KR)) { o_item_price = item_cabbage_price + "000"; } else { o_item_price = item_cabbage_price; }
                break;
            case "Coffee":
                if (p_lang.Equals(KR)) { o_item_price = item_coffee_price + "000"; } else { o_item_price = item_coffee_price; }
                break;
            case "Pumpkin":
                if (p_lang.Equals(KR)) { o_item_price = item_pumpkin_price + "000"; } else { o_item_price = item_pumpkin_price; }
                break;
            case "Juice":
                if (p_lang.Equals(KR)) { o_item_price = item_juice_price + "000"; } else { o_item_price = item_juice_price; }
                break;
            case "Apple":
                if (p_lang.Equals(KR)) { o_item_price = item_apple_price + "000"; } else { o_item_price = item_apple_price; }
                break;
            case "Chip":
                if (p_lang.Equals(KR)) { o_item_price = item_chip_price + "000"; } else { o_item_price = item_chip_price; }
                break;
        }
        return o_item_price;
    }


    // -------------------------
    // SCAN LOG FILE NAME
    // -------------------------
    public static string SCAN_LOG = "SCAN_LOG";
    public static string CONTROLLER_LOG = "CONTROLLER_LOG";
    public static string CART_LOG = "CART_LOG";

    // -------------------------
    // STAGE INFO
    // -------------------------
    public static string STAGE_INTRO = "INTRO";
    public static string STAGE_ADMIN = "ADMIN";
    public static string STAGE_INSTRUC = "INSTRUCTION";
    public static string STAGE_PREPARE = "PREPARE";
    public static string STAGE_END = "END";

    public static string STAGE_1 = "PREPARING";
    public static string STAGE_2 = "SCANNING";
    public static string STAGE_3 = "DISCOUNT";
    public static string STAGE_4 = "DECISION";
    public static string STAGE_5 = "PAY";


    // -------------------------
    // ACTOR
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string ACTOR_USER = "USER";
    public static string ACTOR_ADMIN = "ADMIN";
    public static string ACTOR_SYSTEM = "SYSTEM";
    
    // -------------------------
    // EVNET CATEGORY
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string EVENT_CATE_SCENE  = "SCENE";
    public static string EVENT_CATE_ACT = "ACTION";
    public static string EVENT_CATE_SYS_MSG = "SYSTEM_MESSAGE";
    public static string EVENT_CATE_SCREEN = "SCREEN";
    public static string EVENT_CATE_EYE_TRACKING = "EYE-TRACKING";

    // -------------------------
    // EVNET LOG TY
    // https://docs.google.com/document/d/1O8x6Ucjl2uaBGZVmuFXd42IyIYtvcLUe6-ElejRPQHM/edit
    // -------------------------
    public static string EVENT_TYPE_START   = "START";
    public static string EVENT_TYPE_END     = "END";

    public static string EVENT_TYPE_CLICK = "CLICK";    // Scene
    public static string EVENT_TYPE_BTN_TOUCH = "BTN_TOUCH";

    public static string EVENT_TYPE_SCREEN_CHANGE = "SCREEN_CHANGE";
    public static string EVENT_TYPE_SCREEN_UPDATE = "SCREEN_UPDATE";

    public static string EVENT_TYPE_NOTI = "NOTIFICATION";
    public static string EVENT_TYPE_RESULT_MSG = "RESULT_MSG"; // Screen Message
    // public static string EVENT_TYPE_STAGE_CHANGE= "STAGE_CHANGE";
    
    public static string EVENT_TYPE_ACTION_GRAB = "GRAB";
    public static string EVENT_TYPE_ACTION_RELEASE = "RELEASE";

    public static string EVENT_TYPE_BEFORE_ENTER = "BEFORE_SCANNING_ENTER";
    public static string EVENT_TYPE_BEFORE_EXIT = "BEFORE_SCANNING_EXIT";

    public static string EVENT_TYPE_AFTER_ENTER = "AFTER_SCANNING_ENTER";
    public static string EVENT_TYPE_AFTER_EXIT = "AFTER_SCANNING_EXIT";
    
    public static string EVENT_TYPE_SCAN_ADD = "ITEM_ADD";
    public static string EVENT_TYPE_SCAN_DUP_ADD = "ITEM_DUP_ADD";
    public static string EVENT_TYPE_SCAN_REMOVE = "ITEM_REMOVE";
    public static string EVENT_TYPE_SCAN_DUP_REMOVE = "ITEM_DUP_REMOVE";

    public static string EVENT_TYPE_CODE_FAIL = "CODE_FAIL";
    public static string EVENT_TYPE_CODE_SUCC = "CODE_SUCC";

    public static string EVENT_TYPE_PAY = "PAY";
    public static string EVENT_TYPE_EXCEEDED = "LIMITS_EXCEEDED";

    public static string EVENT_TYPE_ACTION_EYE_DETECHTED_ITEM = "DETECTED_ITEM";
    public static string EVENT_TYPE_ACTION_EYE_DETECHTED_BTN = "DETECTED_BUTTON";
    public static string EVENT_TYPE_ACTION_EYE_DECISION = "DECISION_MAKING";
    // public static string EVENT_TYPE_ACTION_EYE_RAY_ORIGIN = "EYE_RAY_ORIGIN"; // the origin of the gaze ray is a 3D point
    // public static string EVENT_TYPE_ACTION_EYE_RAY_DIRECTION = "EYE_RAY_DIRECTION"; // the direction of the gaze ray is a normalized direction vector

    // -------------------------
    // NOTICE MSG
    // -------------------------
    public static string del_msg = "Please scan what you want to cancel then put on the left table then try again to make a payment.";
    
    // -------------------------
    // SOUND NAME
    // -------------------------
    public static string SOUND_SUCC     = "SYS_SUCC";
    public static string SOUND_ERROR    = "SYS_ERROR";
    public static string SOUND_SCANNED  = "SCANNED";
    public static string SOUND_MESSAGE  = "MESSAGE";
    public static string SOUND_CALL     = "CALL";
    public static string SOUND_VOCAL    = "VACAL";

    // -------------------------
    // BUTTON INFO
    // -------------------------
    public static string BTN_NEXT       = "btn_next";
    public static string BTN_CONFIRM    = "btn_confirm";
    public static string BTN_APPLY      = "btn_apply";
    public static string BTN_PAY        = "btn_pay";
    public static string BTN_BACKSPACE  = "btn_back";
    public static string BTN_SKIP       = "btn_skip";

    // -------------------------
    // Screen Message
    // -------------------------
    public static string MSG_PAYMENT = "";

    // -------------------------
    // BUDGET INFO
    // -------------------------
    public static int BUDGET = 26;

    public static int ReturnBudget(
        string lang
    )
    {
        if (lang.Equals(KR))
        {
            return System.Int32.Parse(BUDGET + "000");
        }
        else
        {
            return BUDGET;
        }
    }

    public static int ReturnDiscountPrice(
        int lang
    )
    {
        int en_discount_price = 3;
        int kr_disoucnt_price = 3000;

        string v_lang = lang.ToString();
        if (v_lang.Equals(KR))
        {
            return kr_disoucnt_price;
        }
        else
        {
            return en_discount_price;
        }
    }
}
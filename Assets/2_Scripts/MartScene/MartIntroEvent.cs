using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MartIntroEvent : MonoBehaviour
{
    int lang;
    string gameMode;
    public GameObject btn_home;

    public GameObject trolley_test; 
    public GameObject trolley_real; 
    
    public TMP_Text text_before;
    public TMP_Text text_scan;
    public TMP_Text text_after;
    public TMP_Text text_card;

    public TMP_Text text_subtitle;

    public TMP_Text text_screen1_result;
    public TMP_Text text_screen1_name;
    public TMP_Text text_screen1_qty;
    public TMP_Text text_screen1_price;
    public TMP_Text text_screen1_total;
    public TMP_Text text_screen1_discount;
    public TMP_Text text_screen1_cancel;
    public TMP_Text text_screen1_pay;
    public TMP_Text text_screen1_currency1;
    public TMP_Text text_screen1_currency2;
    public TMP_Text text_screen1_currency3;

    public TMP_Text text_screen2_instruction;
    public TMP_Text text_screen2_home;

    public TMP_Text text_screen3_result;
    public TMP_Text text_screen3_yes;
    public TMP_Text text_screen3_no;

    public TMP_Text text_screen4_e_result;
    public TMP_Text text_screen4_e_apply;
    public TMP_Text text_screen4_e_backspace;
    public TMP_Text text_screen4_n_result;
    public TMP_Text text_screen4_n_apply;
    public TMP_Text text_screen4_n_backspace;
    public TMP_Text text_screen4_h_result;
    public TMP_Text text_screen4_h_apply;
    public TMP_Text text_screen4_h_backspace;

    public TMP_Text text_screen5_btn_back;
    public TMP_Text text_screen5_tyring_to_pay_msg;

    public TMP_Text text_screen6_result;

    public Material screen;
    public Sprite basic;

    private void Awake()
    {
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);
        gameMode =  GameObject.Find("v_gameMode").GetComponent<Text>().text;
        if (gameMode.Equals(GlobalEnv.GAMEMODE_START)){
            trolley_real.SetActive(true);
            trolley_test.SetActive(false);
        }
        Debug.Log("==== LOADING MART ENV SETTING ====");
        Debug.Log("Lang:"+lang);
        Debug.Log("gameMode:" + gameMode);
    }

    public void Start()
    {
        text_before.text    = LangText.martText_self_before[lang];
        text_after.text = LangText.martText_self_after[lang];
        text_scan.text  = LangText.martText_self_scan[lang];
        text_card.text  = LangText.martText_self_card[lang];

        // level
        string level = GameObject.Find("v_level").GetComponent<Text>().text;
        string discount_code = GlobalEnv.ReturnDiscountCode(level);
        text_subtitle.text = LangText.martNoti_msg2(lang, gameMode, discount_code);

        // screen1
        if (GlobalEnv.KR.Equals(lang.ToString())){
            text_screen1_currency1.text = "W";
            text_screen1_currency2.text = "W";
            text_screen1_currency3.text = "W";
        }
        text_screen1_result.text    = LangText.screen1_scanning[lang];
        text_screen1_name.text  = LangText.screen1_text_itemName[lang];
        text_screen1_qty.text   = LangText.screen1_text_itemQty[lang];
        text_screen1_price.text = LangText.screen1_text_itemPrice[lang];
        text_screen1_total.text = LangText.screen1_text_total[lang];
        text_screen1_discount.text  = LangText.screen1_text_discount[lang];
        text_screen1_cancel.text    = LangText.screen1_button_cancel[lang];
        text_screen1_pay.text   = LangText.screen1_button_pay[lang];

        // screen2
        text_screen2_instruction.text   = LangText.screen2_instruction[lang];
        text_screen2_home.text     = LangText.screen2_btn_home[lang];

        // screen3
        text_screen3_result.text    = LangText.screen3_asking_discount_code[lang];
        text_screen3_yes.text   =  LangText.screen3_btn_yes[lang];
        text_screen3_no.text    = LangText.screen3_btn_no[lang];

        // screen4
        text_screen4_e_result.text    = LangText.screen4_enter_the_dc[lang];
        text_screen4_e_apply.text     = LangText.screen4_button_apply[lang];
        text_screen4_e_backspace.text = LangText.screen4_button_backspace[lang];
        text_screen4_n_result.text = LangText.screen4_enter_the_dc[lang];
        text_screen4_n_apply.text = LangText.screen4_button_apply[lang];
        text_screen4_n_backspace.text = LangText.screen4_button_backspace[lang];
        text_screen4_h_result.text = LangText.screen4_enter_the_dc[lang];
        text_screen4_h_apply.text = LangText.screen4_button_apply[lang];
        text_screen4_h_backspace.text = LangText.screen4_button_backspace[lang];

        // screen5
        text_screen5_btn_back.text = LangText.screen2_btn_home[lang];
        text_screen5_tyring_to_pay_msg.text = LangText.alert_tryingToPay[lang];

        // screen6
        text_screen6_result.text = LangText.screen5_result[lang];

        // Material rollbacks
        screen.SetTexture("_EmissionMap", basic.texture);

        // Set Home button
        if (gameMode.Equals(GlobalEnv.GAMEMODE_START)) {
            btn_home.SetActive(false);
        }
    }
}

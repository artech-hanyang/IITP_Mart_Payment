using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScannerEvent : MonoBehaviour
{
    int lang;

    public Image result_background; // Result_background
    public TMP_Text screen1_result_message; // Screen1 > Result_message
    public TMP_Text screen2_result_message; // Screen2 > Result_message

    public GameObject screen1;
    public GameObject screen2;
    public Text v_current_canvas;
    public GameObject screen2_btn_home;

    public TMP_Text v_total;
    public TMP_Text v_discount;

    public Text v_firstItem_yn;
    bool firstItem_tf = false;

    public TMP_Text total_amount;
    public static bool update_tf = false;

    /**
     * scanned_list: This list for Update screen information
     * dupcheck_scanned_list: This list for blocking the duplicate scanning interaction 
     * scanned_list: This list for blocking the duplicate removing interaction
     */
    Dictionary<string, string> item_list = new Dictionary<string, string>(); // key(item_code), value(item_name)
    Dictionary<string, string> scanned_list = new Dictionary<string, string>(); // key(item_name), value(qty)
    public Dictionary<string, string> dupcheck_scanned_list = new Dictionary<string, string>(); // key(item_code), value(item_name)
    Dictionary<string, string> canceled_list = new Dictionary<string, string>(); // key(item_code), value(item_name)

    void Awake() {
        lang = Int32.Parse(GameObject.Find("v_lang").GetComponent<Text>().text);

        // Initial shopping list total 39, budget 28
        // enssential 28 - disocunt 3 = 25
        // ------------------------------------------------
        // Real
        // ------------------------------------------------
        item_list.Add("Tomato1", "Tomato");
        item_list.Add("Baguette1", "Baguette");
        item_list.Add("Cheese1", "Cheese");
        item_list.Add("Cheese2", "Cheese");
        item_list.Add("Cabbage1", "Cabbage");
        item_list.Add("Coffee1", "Coffee");
        item_list.Add("Pumpkin1", "Pumpkin");

        // ------------------------------------------------
        // test
        // ------------------------------------------------
        item_list.Add("Juice1", "Juice");
        item_list.Add("Apple1", "Apple");
        item_list.Add("Chip1", "Chip");
    }

    void Start() {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Scanner"), true);
        // Physics.IgnoreCollision(leftHands.GetComponent<Collider>(), scanner, true);
        // Physics.IgnoreCollision(rightHand.GetComponent<Collider>(), scanner, true);
    }

    /**
     * Update Screen information
     */
    void Update() {
        int p_discount = 0;

        // Applying discount code
        string discount_auth_yn = GameObject.Find("v_discount_auth_yn").GetComponent<Text>().text;
        if (discount_auth_yn.Equals("Y")) {
            p_discount = GlobalEnv.ReturnDiscountPrice(lang);
            // v_discount.color = Color.red;
        }

        // Update counting 
        GameObject.Find("v_scanned_item_cnt").GetComponent<Text>().text = scanned_list.Count.ToString();
        string v_current_canvas = GameObject.Find("v_current_canvas").GetComponent<Text>().text;

        // Update item info
        if (update_tf == true && (v_current_canvas.Equals("screen1") || v_current_canvas.Equals("screen2"))){
        
            // 1. Reset all information on the self-checkout screen
            ClearAllInformation();

            // 3. Update items list
            int i = 1;
            int p_total = 0;
            foreach (KeyValuePair<string, string> kv in scanned_list){
                string o_item_name  = kv.Key;
                string o_item_qty   = kv.Value;
                string o_item_price = GlobalEnv.ReturnItemPrice(o_item_name, lang);

                // Notify the first calling
                if(firstItem_tf == false && i == 1){
                    v_firstItem_yn.text = "Y";
                    firstItem_tf = true;
                }

                // GameObject.Find("checkbox"+i).GetComponent<Toggle>().interactable = true;
                GameObject.Find("checkbox"+i).GetComponent<Toggle>().isOn = true;
                GameObject.Find("item_name"+i.ToString()).GetComponent<TextMeshProUGUI>().text      = LangText.ReturnItemName(o_item_name, lang);
                GameObject.Find("item_qty"+ i.ToString()).GetComponent<TextMeshProUGUI>().text      = o_item_qty;
                GameObject.Find("item_price"+ i.ToString()).GetComponent<TextMeshProUGUI>().text    = o_item_price;
                p_total += Int32.Parse(o_item_qty) * Int32.Parse(o_item_price);
                i++;
            }
            v_total.text = p_total.ToString();

            // 4. Update total price Info
            int total_price = (p_total - p_discount);
            if(total_price < 0){
                total_price = 0;
            }
            total_amount.text = total_price.ToString();

            M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_UPDATE, "UPDATE", "Screen information Updated");
        }

        // Update
        update_tf = false;
    }

    /**
     * @Function: Initialize the self-checkout screen
     * @UI Format: L1{checkbox1, item_name1, item_qty1, item_price1}
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.25
     * @History: 
     *  - 2020.03.25 Minjung KIM: Initial commit
     */
    void ClearAllInformation(){
        for(int i=1; i<7; i++){
            GameObject.Find("checkbox"+i).GetComponent<Toggle>().interactable = false;
            GameObject.Find("checkbox"+i).GetComponent<Toggle>().isOn = false;
            GameObject.Find("item_name"+i).GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("item_qty"+i).GetComponent<TextMeshProUGUI>().text = "";
            GameObject.Find("item_price"+i).GetComponent<TextMeshProUGUI>().text = "";
        }
        GameObject.Find("total_amount").GetComponent<TextMeshProUGUI>().text = "0";
    }
    
    /**
     * @Function: Item Scanner Function
     * 
     * @Author: Minjung KIM
     * @Date: 2020.Mar.17 
     * @History: 
     *  - 2020.03.17 Minjung KIM: 최초 작성 (1개 타입의 종류만 되도록 개발)
     *  - 2020.03.18 Minjung KIM: 여러 개의 오브젝트가 스캔되도록 개발
     *  - 2020.04.19 Minjung KIM: Block scan interaction when the canvas = screen2 and discount_auth_yn = Y
     *  - 2020.05.03 Minjugn KIM: Allow scan interaction when the user touched cancel buttton
     *  - 2020.05.23 Minjung KIM: Add Event Log
     *  - 2020.06.01 Minjung KIM: Bugfix qty Counting Error
     *  - 2020.07.01 Minjung KIM: Allow the scan anytime
     *  - 2020.07.08 Minjung KIM: Add scanned item name to result_message
     */
     void OnTriggerEnter(Collider collision) {
        string o_item_code = collision.gameObject.name;  // unique item code: ex) A1
        string o_item_name = collision.gameObject.tag;   // item name: ex) Apple

        string current_canvas = GameObject.Find("v_current_canvas").GetComponent<Text>().text;
        string trying_to_pay = GameObject.Find("v_trying_to_pay_yn").GetComponent<Text>().text;

        // ----------------------------------
        // 1. Scan to Add items
        // ----------------------------------
        if (!o_item_name.Equals("Untagged") && !o_item_name.Equals("card") && !o_item_name.Equals("left_cellphone") && current_canvas.Equals("screen1") && !trying_to_pay.Equals("Y")){
            //Debug.Log("Add ScannerEvent() o_item_code:" + o_item_code);
            //Debug.Log("Add ScannerEvent() o_item_name:" + o_item_name);

            // 1-1 Allow only unscanned item! 
            // This means that firstly we check the item unique code from duplicate_scanned_list for block the duplicate scan.
            if (dupcheck_scanned_list.ContainsKey(o_item_code) == false){

                // 1. Update scanned_list quantity then Add item code in duplicate_scanned_list (for blocking the duplicate scans)
                int qty = 1;
                if (scanned_list.ContainsKey(o_item_name)){
                    qty = Int32.Parse(scanned_list[o_item_name]) + 1;
                }
                scanned_list[o_item_name] = qty.ToString();
                dupcheck_scanned_list.Add(o_item_code, o_item_name);
                canceled_list.Remove(o_item_code);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_SCANNED, 99);

                // 2. Add Event log
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_ADD, o_item_name, o_item_code);
                update_tf = true;

                // 3. Update user message
                screen1_result_message.text = LangText.ReturnItemName(o_item_name, lang) + LangText.scan_added[lang];
                screen1_result_message.color = Color.blue;
                result_background.color = Color.white;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCAN_ADD, "screen1_result_message", LangText.ReturnItemName(o_item_name, lang) + LangText.scan_added[lang]);

            } else {
                // Todo : 이미있는 아이템을 또 태그하려고했을 때
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_DUP_ADD, o_item_name, o_item_code);
                // screen1_result_message.text = LangText.ReturnItemName(o_item_name, lang) + "는 이미 스캔된 아이템입니다.";
                // screen1_result_message.color = Color.red;
                // result_background.color = Color.white;
                // SOUND
                // ADMIN LOG
            }

        // ----------------------------------
        // 2. Scan to remove items
        // ----------------------------------
        } else if(!o_item_name.Equals("Untagged") && !o_item_name.Equals("card") && !o_item_name.Equals("left_cellphone") && current_canvas.Equals("screen2")){
            //Debug.Log("Remove ScannerEvent() o_item_code:" + o_item_code);
            //Debug.Log("Remove ScannerEvent() o_item_name:" + o_item_name);

            screen2_result_message.text = "-"; //initializing

            // 2-1. Allow only scanned items & Block that already canceled items
            Debug.Log("CHECK scanned list:"+ scanned_list.ContainsKey(o_item_name));
            Debug.Log("CHECK canceled list:" + !canceled_list.ContainsKey(o_item_code));
            if (scanned_list.ContainsKey(o_item_name) == true && canceled_list.ContainsKey(o_item_code) == false){

                // 1. initializing the result message
                screen1_result_message.text = "-";
                screen1_result_message.color = Color.black;

                // 2. Update scanned_list quantity then Remove item code in duplicate_scanned_list (for allow the scans again)
                int o_qty = Int32.Parse(scanned_list[o_item_name]) - 1;
                if (o_qty <= 0){
                    scanned_list.Remove(o_item_name);
                }else{
                    scanned_list[o_item_name] = o_qty.ToString();
                }
                dupcheck_scanned_list.Remove(o_item_code);
                canceled_list.Add(o_item_code, o_item_name);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_SCANNED, 99);

                // 3. Add Event log
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_REMOVE, o_item_name, o_item_code);

                // 4. Update removed item information with item name and price.
                screen2_result_message.text = LangText.ReturnItemName(o_item_name, lang) + " ( " + GlobalEnv.ReturnItemPrice(o_item_name, lang) + " " + LangText.returnCurrency[lang] + " )" + LangText.scan_removed[lang];
                screen2_result_message.color = Color.blue;
                result_background.color = Color.white;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCAN_REMOVE, "screen2_result_message", LangText.ReturnItemName(o_item_name, lang) + " ( " + GlobalEnv.ReturnItemPrice(o_item_name, lang) + " " + LangText.returnCurrency[lang] + " )" + LangText.scan_removed[lang]);

                // 6. Automatically change the screen
                screen2_btn_home.GetComponent<Button>().interactable = false;
                screen2_btn_home.GetComponent<BoxCollider>().enabled = false;
                Invoke("ChangeScreen2toScreen1After3s", 3f);

            } else{
                // Todo - 이미 제거하거나 없는 아이템을 제거하려고 했을 때
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_USER, GlobalEnv.EVENT_CATE_ACT, GlobalEnv.EVENT_TYPE_SCAN_DUP_REMOVE, o_item_name, o_item_code);
                SoundManager.instance.PlaySound(GlobalEnv.SOUND_ERROR, lang);
                screen2_result_message.text = LangText.ReturnItemName(o_item_name, lang) + "는 없는 아이템입니다.";
                screen2_result_message.color = Color.red;
                result_background.color = Color.white;
                M_EventLogger.EventLogging(GlobalEnv.ACTOR_ADMIN, GlobalEnv.EVENT_CATE_SYS_MSG, GlobalEnv.EVENT_TYPE_SCAN_DUP_REMOVE, "screen2_result_message", LangText.ReturnItemName(o_item_name, lang) + "는 없는 아이템입니다.");
            }
        }
    }

    /**
    * @ Function : Go back to home
    * 
    * @ Author : Minjung KIM
    * @ Date : 2020.07.23
    * @ History :
    **/
    void ChangeScreen2toScreen1After3s()
    {
        v_current_canvas.text = "screen1";
        screen2.gameObject.SetActive(false);
        screen1.gameObject.SetActive(true);
        M_EventLogger.EventLogging(GlobalEnv.ACTOR_SYSTEM, GlobalEnv.EVENT_CATE_SCREEN, GlobalEnv.EVENT_TYPE_SCREEN_CHANGE, "ChangeScreen2toScreen1After3s", "Screen2(cancel) to Screen1(home)");

        update_tf = true;
        screen2_btn_home.GetComponent<Button>().interactable = true;
        screen2_btn_home.GetComponent<BoxCollider>().enabled = true;
    }
}
using System.Collections.Generic;
using UnityEngine;

public class ObjectCounter : MonoBehaviour
{
    Dictionary<string, string> trolleyProducts = new Dictionary<string, string>();

    // This variables are list to buy.
    // int jelly = 0;
    int sugar = 0;
    int chips = 0;
    // int tuna = 0;
    // int corn = 0;
    int coffee = 0;
    // int suasage = 0;
    // int water = 0;
    // int juice = 0;
    int milk = 0;
    int potato = 0;
    // int orange = 0;
    // int pear = 0;
    int cucumber = 0;
    // int pineapple = 0;
    int tomato = 0;
    int cabbage = 0;
    int cheese = 0;
    int bread = 0;
    int otherstuffs = 0;

    public int total = 0;

    /**
     * @Function: ObjectCounter Function, It automataically counted when the object is inside trolley
     * 
     * @Author: Euisung KIM
     * @Date: 2020.Mar.3 
     * @History: 
     *  - 2020.04.03 Euisung KIM: initial commit
     *  - 2020.04.07 MInjung KIM: Add object category types and Function comment
     *  - 2020.04.11 Minjung KIM: Block Untagged object counting 
     *  - 2020.04.16 Minjung KIM: Fix Duplicate couting issue
     *  
     *  @Todo
     *  - Take out counting issue: 바구니에서 물건 뺄 때, 총 개수에서 빼줘야하는 이슈
     *  - Coㅣlider issue: 장바구니에 담긴 물건끼리 안겹치게..
     */
    void OnTriggerEnter(Collider coll)
    {
        Quaternion targetRotation = Quaternion.LookRotation(coll.transform.position - this.transform.position, this.transform.up);

        if (targetRotation.x > 0) Debug.Log("Forward");
        if (targetRotation.x < 0) Debug.Log("Backward");

        string productCode = coll.name;
        string productName = coll.tag;

        if ( !productName.Equals("Untagged") && !trolleyProducts.ContainsKey(productCode)){
            Debug.Log("Item name:" + productName);

            // by the tag name of object, stuffs and total number are counted
            switch (productName)
            {
                case "Sugar":
                    sugar++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Sugar = " + sugar);
                    break;
                case "Milk":
                    milk++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Milk = " + milk);
                    break;
                case "Cabbage":
                    cabbage++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Cabbage = " + cabbage);
                    break;
                case "Cheese":
                    cheese++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Cheese = " + cheese);
                    break;
                case "Chips":
                    chips++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Chips = " + chips);
                    break;
                case "Coffee":
                    coffee++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Coffee = " + coffee);
                    break;
                case "Cucumber":
                    cucumber++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Cucumber = " + cucumber);
                    break;
                case "Tomato":
                    tomato++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Tomato = " + tomato);
                    break;
                default:
                    otherstuffs++;
                    total++;
                    trolleyProducts.Add(productCode, productName);
                    // Debug.Log("Other stuffs = " + otherstuffs);
                    break;
            }
            Debug.Log("Total = " + total);
        }
    }
}

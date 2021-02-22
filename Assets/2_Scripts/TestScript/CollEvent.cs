using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Collider 컴포넌트의 is Trigger가 false인 상태로 충돌을 시작했을 때

    void OnCollisionEnter(Collision collision){
        Debug.Log("충돌 시작!");  
    }

    void OnCollisionExit(Collision collision){
        Debug.Log("ㅜ_ㅠ");
    }
}

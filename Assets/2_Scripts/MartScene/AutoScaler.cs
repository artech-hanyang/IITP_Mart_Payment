using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    [SerializeField]
    private float defaultHeight = 1.8f;
    [SerializeField]
    private Camera camera;

    private void Resize()
    {
        float headHeight = camera.transform.localPosition.y;
        Debug.Log("headHeight:" + headHeight);
        // float scale = defaultHeight / headHeight;
        // transform.localScale = Vector3.one * scale;

        float scale = 0.4f;
        if (defaultHeight > 1.8f){
            scale = 0.7f;
        } else if (defaultHeight > 1.7f && defaultHeight < 1.79f){
            scale = 0.85f;
        } else if (defaultHeight > 1.6f && defaultHeight < 1.69f){
            scale = 0.9f;
        } else if (defaultHeight > 1.5f && defaultHeight < 1.59f){
            scale = 0.95f;
        }else {
            scale = 0.99f;
        }

        Debug.Log("scale    :"+ scale);
        transform.localScale = Vector3.one * scale;
    }

    void OnEnable()
    {
       Resize();
    } 
}

/**
 * using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    [SerializeField]
    private float defaultHeight = 1.8f;
    [SerializeField]
    private Camera camera;

    private void Resize()
    {
        float headHeight = camera.transform.localPosition.y;
        float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
    }
    
    void OnEnable()
    {
        Resize();
    }
}
    */
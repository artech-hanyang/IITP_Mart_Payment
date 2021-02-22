/*
using UnityEngine;
using System;
using System.Collections;
using Valve.VR;

public class ViveController : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    //
    // 비전공자가 쉽게 바이브 컨트롤러를 조작하게 하기 위한 스크립트 입니다. 
    // - Lee hun
     

    private static int _leftDeviceNumber = -1;      // -1일 경우 디바이스가 인식되지 않음을 의미.
    private static int _rightDeviceNumber = -1;

    [SerializeField]
    private SteamVR_TrackedObject _leftDevice = null;
    [SerializeField]
    private SteamVR_TrackedObject _rightDevice = null;
    [SerializeField]
    private ReticlePoser _leftPoser = null;
    [SerializeField]
    private ReticlePoser _rightPoser = null;

    //디바이스가 향하고 있는 오브젝트
    private static GameObject _leftTargetObj = null;
    private static GameObject _rightTargetObj = null;

    void Update()
    {
       // Debug.Log(OpenVR.k_unTrackedDeviceIndexInvalid);
        if (_leftDevice != null)
            _leftDeviceNumber = (int)_leftDevice.index;
        if (_rightDevice != null)
            _rightDeviceNumber = (int)_rightDevice.index;
        _leftTargetObj = ((_leftPoser.target.gameObject == null ? false : _leftPoser.target.gameObject.activeSelf)) ? _leftPoser.hitTarget.gameObject : null;
        _rightTargetObj = ((_rightPoser.target.gameObject == null ? false : _rightPoser.target.gameObject.activeSelf)) ? _rightPoser.hitTarget.gameObject : null;
    }

    public static GameObject LEFT_DEVICE_TARGET_OBJ
    {
        get { return _leftTargetObj; }
    }

    public static GameObject RIGHT_DEVICE_TARGET_OBJ
    {
        get { return _rightTargetObj; }
    }

    public static SteamVR_Controller.Device LEFT_DEVICE
    {
        get{ return GetDevice(true);}
    }

    public static SteamVR_Controller.Device RIGHT_DEVICE
    {
        get { return GetDevice(false); }
    }
    static SteamVR_Controller.Device GetDevice(bool left)
    {
        SteamVR_Controller.Device device = null;
        try
        {
            device = SteamVR_Controller.Input((left) ? _leftDeviceNumber : _rightDeviceNumber);
            return device;
        }
        catch(Exception e)
        {
            Debug.Log("디바이스가 인식되지 않습니다. 혹은 컨트롤러 prefab에 연결이 안되어있습니다.");
        }
        return null;
    }
}
*/
//Saves an instanteneous log of data containing information such as the current altitude, the number of falls, the framerate, etc.
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Tobii.XR;
using Tobii.G2OM;
using Valve.VR;
//using System.Diagnostics;

public class CommonDataLogger : MonoBehaviour, IGazeFocusable
{

    public GameObject main_camera;
    public GameObject left_tracker;
    public GameObject right_tracker;

    public SteamVR_Action_Pose poseAction = SteamVR_Input.GetAction<SteamVR_Action_Pose>("Pose");

    [Tooltip("The device this action should apply to. Any if the action is not device specific.")]
    public SteamVR_Input_Sources left_inputSource;
    public SteamVR_Input_Sources right_inputSource;

    private string path;
    bool focused = false;

    string LocalPosition_to_string(GameObject _gameObject)
    {
        string result = "";
        Vector3 vector = _gameObject.transform.localPosition;
        result += vector.x;
        result += ",";
        result += vector.y;
        result += ",";
        result += vector.z;

        return result;
    }
    string LocalRotation_to_string(GameObject _gameObject)
    {
        string result = "";
        Quaternion vector = _gameObject.transform.localRotation;
        result += vector.x;
        result += ",";
        result += vector.y;
        result += ",";
        result += vector.z;
        result += ",";
        result += vector.z;

        return result;
    }

    string VIVE_localPosition_to_string(Vector3 vive_controller)
    {
        string result = "";
        result += vive_controller.x;
        result += ",";
        result += vive_controller.y;
        result += ",";
        result += vive_controller.z;

        return result;
    }
    string VIVE_localRotation_to_string(Quaternion vive_controller)
    {
        string result = "";
        result += vive_controller.x;
        result += ",";
        result += vive_controller.y;
        result += ",";
        result += vive_controller.z;
        result += ",";
        result += vive_controller.z;

        return result;
    }

    string EyeLocalPosition_to_string(Vector3 eyeInfo)
    {
        string result = "";
        result += eyeInfo.x;
        result += ",";
        result += eyeInfo.y;
        result += ",";
        result += eyeInfo.z;

        return result;
    }

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //This object either received or lost focused this frame, as indicated by the hasFocus parameter.
        // focused = hasFocus;
    }

    void Awake()
    {
        var settings = new TobiiXR_Settings();
        TobiiXR.Start(settings);
    }

    // Use this for initialization
    void Start()
    {
        if (main_camera == null || left_tracker == null || right_tracker == null)
        {
            throw new NullReferenceException("Please check the tracker object you want to receord. some objects are empty.");
        }
        else
        {
            string p_name = GameObject.Find("v_name").GetComponent<Text>().text;
            if (p_name.Length < 0)
            {
                p_name = "TEST_USER";
            }

            // path = "Log-" + DateTime.Now + ".csv";
            path = M_EventLogger.GetFilePath() + DateTime.Now.ToString("yyMMdd_HHMM") + "_" + p_name + "_Common_Log.csv";

            string create_text = "AppTime,"
                + "HMDPos_x,HMDPos_y,HMDPos_z,"  // HMD Position
                + "HMDRot_x,HMDRot_y,HMDRot_z,HMDRot_w," // HMD Rotation
                    + "EyeBlink_left,EyeBlink_right,"           // Eye Blinking
                    + "EyePos_org_x,EyePos_org_y,EyePos_org_z," // Eye origin
                    + "EyePos_dir_x,EyePos_dir_y,EyePos_dir_z,"// Eye direction	
                    + "Eye_time_stamp,"  
                + "LeftPos_x,LeftPos_y,LeftPos_z,LeftVelocity," // Left Position	
                + "LeftRot_x,LeftRot_y,LeftRot_z,LeftRot_w," // Left Rotation	
                + "RightPos_x,RightPos_y,RightPos_z,RightVelocity," // Right Position	
                + "RightRot_x,RightRot_y,RightRot_z,RightRot_w"
                + Environment.NewLine;
            File.WriteAllText(path, create_text);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // AppTime
        string new_line = Time.realtimeSinceStartup.ToString() + ",";

        // HMD	
        if (main_camera != null)
        {
            new_line += LocalPosition_to_string(main_camera);
            new_line += ",";
            new_line += LocalRotation_to_string(main_camera);
            new_line += ",";
        }
        else
        {
            new_line += ",,,,,,,";
        }
        
        // Eye 
        var eyeTrackingData = TobiiXR.GetEyeTrackingData(TobiiXR_TrackingSpace.World);
        // The validity of ConvergenceDistance. We recommended not using the value of ConvergenceDistance if ConvergenceDistanceIsValid is false.
        if (eyeTrackingData != null)
        {
            new_line += eyeTrackingData.IsLeftEyeBlinking + "," + eyeTrackingData.IsRightEyeBlinking;  // Value indicating if the left/right eye is closed (true) or open (false)
            new_line += ",";
        }
        else
        {
            new_line += ",,";
        }

        if (eyeTrackingData != null && eyeTrackingData.ConvergenceDistanceIsValid == true && eyeTrackingData.GazeRay.IsValid == true)
        {
            new_line += EyeLocalPosition_to_string(eyeTrackingData.GazeRay.Origin); // The point in world space for the origin of the gaze ray.
            new_line += ",";
            new_line += EyeLocalPosition_to_string(eyeTrackingData.GazeRay.Direction); //	The normalized direction vector in world space for the direction of the gaze ray.
            new_line += ",";
            new_line += eyeTrackingData.Timestamp; // The timestamp for when the data was received, measured in seconds since application start.
            new_line += ",";
        }
        else
        {
            new_line += ",,,,,,,";
        }

        // left Tracker 	
        if (left_tracker != null && left_tracker.activeInHierarchy)
        {
            new_line += VIVE_localPosition_to_string(poseAction[left_inputSource].localPosition);
            new_line += ",";
            new_line += poseAction[left_inputSource].velocity.magnitude; // left controller velocity
            new_line += ",";
            new_line += VIVE_localRotation_to_string(poseAction[left_inputSource].localRotation);
            new_line += ",";

            
        }
        else
        {
            new_line += ",,,,,,,,";
        }

        // right Tracker	
        if (right_tracker != null && right_tracker.activeInHierarchy)
        {
            new_line += VIVE_localPosition_to_string(poseAction[right_inputSource].localPosition);
            new_line += ",";
            new_line += poseAction[right_inputSource].velocity.magnitude; // right controller velocity
            new_line += ",";
            new_line += VIVE_localRotation_to_string(poseAction[right_inputSource].localRotation);
            new_line += ",";
        }
        else
        {
            new_line += ",,,,,,,,";
        }

        new_line += Environment.NewLine;
        File.AppendAllText(path, new_line);
    }
}

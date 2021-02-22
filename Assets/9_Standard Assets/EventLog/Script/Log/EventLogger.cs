using System;
using System.IO;
using System.Text;
using UnityEngine;

public class EventLogger : MonoBehaviour
{

    private static readonly object innerLock = new object();
    private static string path = "EventLog-" + DateTime.Now + ".csv";

    // Use this for initialization
    void OnEnable()
    {
        File.AppendAllText(path, "AppTime;Actor;Verb;Action\n", Encoding.UTF8);
    }

    public static void Log(EventLog e)
    {
        Debug.Log(e.ToString());
        lock (innerLock)
        {
            File.AppendAllText(path, String.Format("{0};{1};{2};\n", DateTime.Now, Time.realtimeSinceStartup, e.ToString()), Encoding.UTF8);
        }
    }
}

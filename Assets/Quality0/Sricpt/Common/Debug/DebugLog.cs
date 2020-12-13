using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public enum LOG_TYPE
    {
        SCENE,
        UI,
        DATA,
        GAME,
        TEST,
    }

    public static void Log(LOG_TYPE type, string message)
    {
        Debug.Log(CreateDebugMessage(type,message));
    }

    public static void Warning(LOG_TYPE type, string message)
    {
        Debug.LogWarning(CreateDebugMessage(type, message));
    }

    public static void Error(LOG_TYPE type, string message)
    {
        Debug.LogError(CreateDebugMessage(type, message));
    }

    public static string CreateDebugMessage(LOG_TYPE type, string message)
    {
        string color = "<color=blue>";
        if(type == LOG_TYPE.TEST)
        {
            color = "<color=red>";
        }
        string debugMessage = string.Format("{0}[{1}]</color>{2}",color, type.ToString(), message); ;
        return debugMessage;
    }
}

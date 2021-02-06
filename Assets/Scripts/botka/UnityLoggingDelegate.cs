using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityLoggingDelegate
{
    public enum LogType
    {
        General, Warning, Error
    }
    public static void Log(LogType logType, string logMessage)
    {
        switch(logType)
        {
            case LogType.General:
                Debug.Log(logMessage);
                break;
            case LogType.Warning:
                Debug.LogWarning(logMessage);
                break;
            case LogType.Error:
                Debug.LogError(logMessage);
                break;
            default:
                break;
        }
    }
    public static void LogIfTrue(bool condition, LogType logType, string logMessaage)
    {
        if (condition)
        {
            Log(logType, logMessaage);
        }
    }
}

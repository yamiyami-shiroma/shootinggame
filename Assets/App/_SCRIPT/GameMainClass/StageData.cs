using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "DATA/Create StageData")]
public class StageData : ScriptableObject
{
    public List<StageScript> StageScripts;
}

[Serializable]
public class StageScript
{
    public enum SCRIPT_COMMAND
    {
        NONE,
        WAIT,
        CREATE_ENEMY,
        SCROLL_START,
    }
    public SCRIPT_COMMAND ScriptCommand = SCRIPT_COMMAND.NONE;
    public List<ScriptParameter> ScriptParameters = new List<ScriptParameter>();
    public string GetParameterValue(int no, int index = 0)
    {
        var param = GetParamter(no);
        if (param.Count > index)
        {
            return param[index];
        }
        return "";
    }

    public List<string> GetParamter(int no)
    {
        if (no < ScriptParameters.Count)
        {
            return ScriptParameters[no].value;
        }
        return new List<string>();
    }
}

[Serializable]
public class ScriptParameter
{
    public List<string> value;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMgr : SingletonMonoBehaviour<BoostMgr>
{
    protected override void Awake()
    {
        if (SceneMgr.Instance == null)
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(transform);
            obj.name = "SceneMgr";
        }
        SceneMgr.Instance.Initilize();
    }
}

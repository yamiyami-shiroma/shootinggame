using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneConst;

public class SceneBase : MonoBehaviour
{
    protected GameObject tapBtn;
    private bool IsInitialize = false;
    protected bool isTouchEnable = false;

    virtual public void Initilize()
    {

    }

    public virtual IEnumerator DoInitialize()
    {
        yield return null;
        StartCoroutine(UpdateStart());
    }

    protected IEnumerator UpdateStart()
    {
        while (true)
        {
            yield return null;
            SceneUpdate();
        }
    }

    protected virtual void SceneUpdate()
    {
    }

    public virtual void OnCompleteInitialize()
    {
        IsInitialize = true;
        TouchEnable(true);
    }

    protected void TouchEnable(bool isEnable)
    {
        isTouchEnable = isEnable;
    }

    virtual public void ActionBtn(GameObject btn)
    {
        if (!IsInitialize || !isTouchEnable)
        {
            return;
        }
        tapBtn = btn;
        string actionName = BtnCtrl.CreateActionName(btn);
        DebugLog.Log(DebugLog.LOG_TYPE.TEST, actionName);
        gameObject.SendMessage(actionName, SendMessageOptions.DontRequireReceiver);
    }

    protected virtual void ActionBtnTwitter()
    {

    }

    protected virtual void MoveScene(SCENE scene)
    {
        TouchEnable(false);
        SceneMgr.Instance.SceneLoad(scene);
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected virtual void AdPlayMovie()
    {

    }

    /// <summary>
    /// TODO 広告動画再生ボタン btnctrlで設定して必ずここに来るように修正
    /// </summary>
    protected virtual void ActionBtnMoview()
    {

    }
}

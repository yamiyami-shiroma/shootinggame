using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainScene : SceneBase
{
    [SerializeField]
    private ScrollCtrl stageScroll = null;
    private bool isScriptEnd = false;
    public void ActionBtnGoToTitle()
    {
        MoveScene(SceneConst.SCENE.TITLE);
    }

    public override void Initilize()
    {
        base.Initilize();
        this.isScriptEnd = false;
        int stageNo = 1;
        string stageFileName = string.Format("Stage-{0:D3}", stageNo);
        StageManager.Instance.LoadStageData("Stage-001");
    }

    public override IEnumerator DoInitialize()
    {
        yield return base.DoInitialize();
        ScriptProcess();
    }

    protected override void SceneUpdate()
    {
        base.SceneUpdate();
    }

    private void ScriptProcess()
    {
        this.isScriptEnd = !StageManager.Instance.NextStageCommnad(() => ScriptProcess());
    }
}

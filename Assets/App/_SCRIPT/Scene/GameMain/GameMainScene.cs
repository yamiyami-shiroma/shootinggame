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
        StageManager.Instance.CreatePlayer(new List<string>() { "BulletPatternData002" }, new Vector3(0, -450));
    }

    public override void OnCompleteInitialize()
    {
        base.OnCompleteInitialize();
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

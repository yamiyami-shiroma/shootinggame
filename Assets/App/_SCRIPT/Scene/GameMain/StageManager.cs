using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    public StageData NowStageData = null;
    private int StageCommandIndex = 0;
    public override void Initilize()
    {
        base.Initilize();
    }
    public void CreateEnemy(string enemyName, List<string> bulletFiles, List<string> moveFiles, Vector2 pos)
    {
        var enemyPrefab = Resources.Load<GameObject>(string.Format("Enemy/{0}", enemyName));
        if (enemyPrefab != null)
        {
            var enemy = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity, transform).GetComponent<CharacterBase>();
            if (enemy != null)
            {
                enemy.SetUp(bulletFiles, moveFiles, pos);
            }
        }
    }

    public void LoadStageData(string stageName)
    {
        NowStageData = Resources.Load<StageData>("Stage/" + stageName);
    }

    public bool NextStageCommnad(UnityAction callback)
    {
        if (this.NowStageData.StageScripts.Count <= this.StageCommandIndex)
        {
            return false;
        }
        var sctiptCommand = this.NowStageData.StageScripts[this.StageCommandIndex];
        StartCoroutine(ScriptCommandProcess(sctiptCommand, callback));
        return true;
    }

    private IEnumerator ScriptCommandProcess(StageScript script, UnityAction callback)
    {
        Debug.LogWarning(script.ScriptCommand + ":" + script.GetParameterValue(0));
        switch (script.ScriptCommand)
        {
            case StageScript.SCRIPT_COMMAND.CREATE_ENEMY:
                float x = 0;
                float y = 0;
                float.TryParse(script.GetParameterValue(3, 0), out x);
                float.TryParse(script.GetParameterValue(3, 1), out y);
                Vector2 pos = new Vector2(x, y);
                CreateEnemy(script.GetParameterValue(0), script.GetParamter(1), script.GetParamter(2), pos);
                break;
            case StageScript.SCRIPT_COMMAND.SCROLL_START:
                break;
            case StageScript.SCRIPT_COMMAND.WAIT:
                int waitFrame = 0;
                int.TryParse(script.GetParameterValue(0), out waitFrame);
                yield return WaitFrame(waitFrame);
                break;
        }
        this.StageCommandIndex++;
        callback();
    }

    private IEnumerator WaitFrame(int frame)
    {
        int count = 0;
        while (count++ < frame)
        {
            yield return null;
        }
    }
}

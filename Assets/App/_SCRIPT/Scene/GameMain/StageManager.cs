using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    public StageData NowStageData = null;
    private int StageCommandIndex = 0;
    private List<CharacterBase> CreateCharacterList = new List<CharacterBase>();
    private PlayerCtrl player = null;
    private BossEnemy boss = null;
    public override void Initilize()
    {
        base.Initilize();
    }
    public void CreateEnemy(string enemyName, List<string> bulletFiles, List<string> moveFiles, Vector2 pos, uint alive = 0)
    {
        var enemyPrefab = Resources.Load<GameObject>(string.Format("Enemy/{0}", enemyName));
        if (enemyPrefab != null)
        {
            var enemy = Instantiate(enemyPrefab, transform, false).GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.type = CharacterBase.CHARACTER_TYPE.ENEMY;
                enemy.SetUp(bulletFiles, moveFiles, pos, alive);
            }
            this.CreateCharacterList.Add(enemy);
        }
    }

    public void CreateBoss(string enemyName, List<string> bulletFiles, List<string> moveFiles, Vector2 pos, uint alive = 0)
    {
        var bossPrefab = Resources.Load<GameObject>(string.Format("Enemy/{0}", enemyName));
        if (bossPrefab != null)
        {
            var boss = Instantiate(bossPrefab, transform, false).GetComponent<BossEnemy>();
            if (boss != null)
            {
                boss.type = CharacterBase.CHARACTER_TYPE.BOSS;
                boss.SetUp(bulletFiles, moveFiles, pos, alive);
            }
        }
    }

    public void CreatePlayer(List<string> bulletFiles, Vector2 pos)
    {
        var playerPrefab = Resources.Load<GameObject>("Player/Player");
        if (playerPrefab != null)
        {
            this.player = Instantiate(playerPrefab, transform, false).GetComponent<PlayerCtrl>();
            if (this.player != null)
            {
                player.type = CharacterBase.CHARACTER_TYPE.PLAYER;
                player.SetUp(bulletFiles, null, pos);
            }
        }
    }

    public void RemoveCharacter(CharacterBase character)
    {
        this.CreateCharacterList.Remove(character);
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
        float x = 0;
        float y = 0;
        uint alive = 0;
        switch (script.ScriptCommand)
        {
            case StageScript.SCRIPT_COMMAND.CREATE_ENEMY:
                float.TryParse(script.GetParameterValue(3, 0), out x);
                float.TryParse(script.GetParameterValue(3, 1), out y);
                uint.TryParse(script.GetParameterValue(4), out alive);
                CreateEnemy(script.GetParameterValue(0), script.GetParamter(1), script.GetParamter(2), new Vector2(x, y), alive);
                break;
            case StageScript.SCRIPT_COMMAND.SCROLL_START:
                break;
            case StageScript.SCRIPT_COMMAND.WAIT:
                int waitFrame = 0;
                int.TryParse(script.GetParameterValue(0), out waitFrame);
                yield return WaitFrame(waitFrame);
                break;
            case StageScript.SCRIPT_COMMAND.CREATE_BOSS:

                float.TryParse(script.GetParameterValue(3, 0), out x);
                float.TryParse(script.GetParameterValue(3, 1), out y);
                uint.TryParse(script.GetParameterValue(4), out alive);
                CreateBoss(script.GetParameterValue(0), script.GetParamter(1), script.GetParamter(2), new Vector2(x, y), alive);
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

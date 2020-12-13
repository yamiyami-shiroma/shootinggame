using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : SingletonMonoBehaviour<DebugManager>
{
    private readonly string EnemyResourcePath = "Enemy{0:D3}";
    private readonly string MovePatternName = "EnemyMovePattern{0:D3}";
    private readonly string BulletPatterName = "BulletPatternData{0:D3}";
    [SerializeField]
    private Text enemyIdInputField;
    [SerializeField]
    private Text bulletPatternIdInputField;
    [SerializeField]
    private Text movePatterIdInputField;
    [SerializeField]
    private Text posXInputField;
    [SerializeField]
    private Text posYInputField;

    public void OnClickCreateEnemy()
    {
        int enemyId = 0;
        int moveId = 0;
        int bulletId = 0;
        float x = 0;
        float y = 0;
        int.TryParse(enemyIdInputField.text, out enemyId);
        int.TryParse(movePatterIdInputField.text, out moveId);
        int.TryParse(bulletPatternIdInputField.text, out bulletId);
        float.TryParse(posXInputField.text, out x);
        float.TryParse(posYInputField.text, out y);
        Vector2 pos = new Vector2(x, y);
        string bulletName = string.Format(BulletPatterName, bulletId);
        string enemyName = string.Format(EnemyResourcePath, enemyId);
        string moveName = string.Format(MovePatternName, moveId);
        List<string> bulletNames = new List<string>() { bulletName };
        if (bulletId == 0)
        {
            bulletNames = new List<string>();
        }
        List<string> moveNames = new List<string>() { moveName };
        StageManager.Instance.CreateEnemy(enemyName, bulletNames, moveNames, pos);
    }
}

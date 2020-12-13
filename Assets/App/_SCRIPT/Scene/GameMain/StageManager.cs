using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    public void CreateEnemy(string enemyName, List<string> bulletFiles, List<string> moveFiles, Vector3 pos)
    {
        var enemyPrefab = Resources.Load<GameObject>(enemyName);
        if (enemyPrefab != null)
        {
            var enemy = Instantiate(enemyPrefab, Vector2.zero, Quaternion.identity, transform).GetComponent<CharacterBase>();
            if (enemy != null)
            {
                enemy.SetUp(bulletFiles, moveFiles, pos);
            }
        }

    }
}

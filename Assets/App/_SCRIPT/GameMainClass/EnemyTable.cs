using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "DATA/Create EnemyTable")]
public class EnemyTable : ScriptableObject
{
    [Serializable]
    public struct EnemyTableData
    {
        public int enemyObjId;
        public int dropItemId;
        public int defeatScore;
    }
    public List<EnemyTableData> enemyTableDatas;
}
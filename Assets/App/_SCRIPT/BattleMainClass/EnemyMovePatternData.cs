using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EnemyMoveData
{
    [SerializeField]
    private AnimationCurve xMoveCurve = AnimationCurve.Constant(0, 1, 1f);
    [SerializeField]
    private AnimationCurve yMoveCurve = AnimationCurve.Constant(0, 1, 1f);

    [SerializeField]
    private Vector2 targetPosition;
    [SerializeField]
    private uint moveFrame;
    public Vector2 GetMovePosition(uint frame)
    {
        float rate = (float)frame / (float)moveFrame;
        if (rate < 0)
        {
            rate = 0;
        }
        else if (rate > 1)
        {
            rate = 1;
        }
        float y = targetPosition.y;
        float x = targetPosition.x;
        y = y * yMoveCurve.Evaluate(rate);
        x = x * yMoveCurve.Evaluate(rate);
        return new Vector2(x, y);
    }
    public bool IsMove(uint frame)
    {
        return frame < this.moveFrame;
    }
}

[CreateAssetMenu(menuName = "DATA/Create EnemyMovePattern")]
public class EnemyMovePatternData : ScriptableObject
{
    [SerializeField]
    private string comment;
    [SerializeField]
    private Vector2 startPosition;
    public Vector2 StartPosition { get { return this.startPosition; } }
    [SerializeField]
    private Vector2 resetPosition;
    public Vector2 ResetPosition { get { return this.resetPosition; } }
    [SerializeField]
    private List<EnemyMoveData> moveDataList;
    public EnemyMoveData Get(int index)
    {
        return moveDataList[index];
    }
    public void Index(ref int index)
    {
        index = index + 1;
        if (moveDataList.Count <= index)
        {
            index = 0;
        }
    }
    public List<EnemyMoveData> Load()
    {
#if UNITY_EDITOR
        Debug.LogWarning(comment);
#endif
        return moveDataList;
    }
}
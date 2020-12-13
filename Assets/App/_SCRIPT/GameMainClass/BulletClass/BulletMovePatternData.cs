using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DATA/Create BulletPattern")]
public class BulletMovePatternData : ScriptableObject
{
    [SerializeField]
    private string comment;
    [SerializeField]
    BulletObject bullet;
    public BulletObject Bullet { get { return this.bullet; } }
    [SerializeField]
    private int cooltimeFrame;
    public bool IsCreate(int frame)
    {
        return frame >= this.cooltimeFrame;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "DATA/Create Bullet")]
public class BulletDatas : ScriptableObject
{
    public List<BulletMoveData> datas;
}

[Serializable]
public class BulletMoveData
{
    public enum BULLE_TYPE
    {
        STRAIGHT,
        HORMING,
        REFLECT,
        TARGET,
    }
    [SerializeField]
    private uint id = 0;
    public uint Id { get { return this.id; } }
    [SerializeField]
    private Vector2 position = Vector3.zero;
    public Vector2 Position { get { return this.position; } }
    [SerializeField]
    private Vector3 vectol = new Vector3(0, 1, 0);
    public Vector3 Vectol { get { return this.vectol; } }
    [SerializeField]
    private float rotation = 0;
    public float Rotation { get { return this.rotation; } }
    [SerializeField]
    private float baseSpeed = 1f;
    public float BaseSpeed { get { return this.baseSpeed; } }
    [SerializeField]
    private AnimationCurve speedRate = AnimationCurve.Constant(0, 1, 1f);
    [SerializeField]
    private int frame;
    public int Frame { get { return this.frame; } }
    [SerializeField]
    private BULLE_TYPE bulletType = BULLE_TYPE.STRAIGHT;
    public BULLE_TYPE BulletType { get { return this.bulletType; } }
    [SerializeField]
    private int destroyDelay = 0;
    public int DestroyDelay { get { return this.destroyDelay; } }
    public float GetSpeed(float rate = 0)
    {
        if (rate < 0)
        {
            rate = 0;
        }
        else if (rate > 1)
        {
            rate = 1;
        }
        return speedRate.Evaluate(rate) * this.baseSpeed;
    }
    [SerializeField]
    private string comment;
    public string Comment { get { return this.comment; } }
}

[Serializable]
public class BulletObject
{
    [SerializeField]
    private uint id;
    public uint ID { get { return this.id; } }
    [SerializeField]
    private uint objId;
    public uint ObjId { get { return this.objId; } }
    [SerializeField]
    private int delay;
    public int Delay { get { return this.delay; } }
    [SerializeField]
    private List<BulletObject> nextBullets;
    public List<BulletObject> NextBullets { get { return this.nextBullets; } }

}
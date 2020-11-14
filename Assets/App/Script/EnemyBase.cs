using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private List<string> enemyBulletPatternFileNames;
    [SerializeField]
    protected List<string> enemyMovePatternFileNames;
    [SerializeField]
    private bool isTest = false;
    [SerializeField]
    private uint aliveFrame = 1000;
    private List<EnemyMovePatternData> movePattern;
    private List<BulletMovePatternData> bulletPattern;
    private int currentBulletNo = 0;
    private int bulletCountFrame = 0;
    private uint countMoveFrame = 0;
    private uint countFrame = 0;
    private int nowMoveIndex = 0;
    private int nowMovePattern = 0;
    protected Vector2 position;

    private bool isInit = false;
    private RectTransform rectTransform;
    private RectTransform rect
    {
        get
        {
            if (rectTransform == null)
            {
                rectTransform = transform as RectTransform;
            }
            return rectTransform;
        }
    }
    private Vector3 localPosition
    {
        get
        {
            return rect.anchoredPosition;
        }
    }

    private void Awake()
    {
        if (isTest)
        {
            SetUp(enemyBulletPatternFileNames, enemyMovePatternFileNames, localPosition);
        }
    }

    public virtual void SetUp(List<string> bulletNames, List<string> moveNames, Vector3 pos)
    {
        rect.anchoredPosition = pos;
        movePattern = new List<EnemyMovePatternData>();
        bulletPattern = new List<BulletMovePatternData>();
        for (int i = 0; i < moveNames.Count; i++)
        {
            var path = "EnemyMove/" + moveNames[i];
            var pattern = Resources.Load<EnemyMovePatternData>("EnemyMove/" + moveNames[i]);
            Debug.LogWarning("Load :" + path);
            movePattern.Add(pattern);
        }
        for (int i = 0; i < bulletNames.Count; i++)
        {
            var path = "Bullet/" + bulletNames[i];
            var pattern = Resources.Load<BulletMovePatternData>("Bullet/" + bulletNames[i]);
            Debug.LogWarning("Load :" + path);
            bulletPattern.Add(pattern);
        }
        nowMoveIndex = 0;
        nowMovePattern = 0;
        position = pos;
        isInit = true;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (isInit)
        {
            this.countFrame++;
            MoveUpdate();
            BulletUpdate();
            if (aliveFrame < this.countFrame)
            {
                Die();
            }
        }
    }

    private float time = 0;
    protected void Die()
    {
        Debug.LogWarning(time.ToString());
        Destroy(gameObject);
    }

    protected virtual void MoveUpdate()
    {
        this.countMoveFrame++;
        var moveData = this.movePattern[nowMovePattern].Get(nowMoveIndex);
        if (moveData.IsMove(this.countMoveFrame))
        {
            rect.anchoredPosition = position + moveData.GetMovePosition(this.countMoveFrame);
        }
        else
        {
            rect.anchoredPosition = position + moveData.GetMovePosition(this.countMoveFrame);
            this.countMoveFrame = 0;
            position = rect.anchoredPosition;
            this.movePattern[nowMovePattern].Index(ref nowMoveIndex);
        }
    }

    protected virtual void BulletUpdate()
    {
        this.bulletCountFrame++;
        var nowBullet = GetBullet();
        if (nowBullet != null)
        {
            if (nowBullet.IsCreate(this.bulletCountFrame))
            {
                var bullet = nowBullet.Bullet;
                BulletManager.Instance.CreateToBullet(bullet, rect);
                this.bulletCountFrame = 0;
            }
        }
    }

    protected virtual BulletMovePatternData GetBullet()
    {
        if (bulletPattern.Count > currentBulletNo)
        {
            return bulletPattern[currentBulletNo];
        }
        return null;
    }
}

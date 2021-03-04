using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterBase : MonoBehaviour
{
    public enum CHARACTER_TYPE
    {
        NONE,
        PLAYER,
        ENEMY,
        BOSS,
        ITEM
    }
    [SerializeField]
    private List<string> enemyBulletPatternFileNames;
    [SerializeField]
    protected List<string> enemyMovePatternFileNames;
    [SerializeField]
    private bool isTest = false;

    [SerializeField]
    private uint aliveFrame = 1000;
    [SerializeField]
    public CHARACTER_TYPE type;
    [SerializeField]
    protected GameObject deadEffect = null;
    protected List<EnemyMovePatternData> movePattern;
    protected List<BulletMovePatternData> bulletPattern;
    private int currentBulletNo = 0;
    private int bulletCountFrame = 0;
    private uint countMoveFrame = 0;
    private uint countFrame = 0;
    private int nowMoveIndex = 0;
    private int nowMovePattern = 0;
    protected Vector2 position;

    protected bool isInit = false;
    private RectTransform rectTransform;
    protected RectTransform rect
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

    public virtual void SetUp(List<string> bulletNames, List<string> moveNames, Vector2 pos, uint alive = 0)
    {
        rect.anchoredPosition = pos;
        movePattern = new List<EnemyMovePatternData>();
        bulletPattern = new List<BulletMovePatternData>();
        for (int i = 0; i < moveNames.Count; i++)
        {
            var path = "EnemyMove/" + moveNames[i];
            var pattern = Resources.Load<EnemyMovePatternData>("EnemyMove/" + moveNames[i]);
            movePattern.Add(pattern);
        }
        for (int i = 0; i < bulletNames.Count; i++)
        {
            var path = "Bullet/" + bulletNames[i];
            var pattern = Resources.Load<BulletMovePatternData>("Bullet/" + bulletNames[i]);
            bulletPattern.Add(pattern);
        }
        if (alive > 0)
        {
            aliveFrame = alive;
        }
        nowMoveIndex = 0;
        nowMovePattern = 0;
        position = pos;
        isInit = true;
    }

    private void Update()
    {
        CharacterUpdate();
    }

    public void CharacterUpdate()
    {
        time += Time.deltaTime;
        if (isInit)
        {
            this.countFrame++;
            MoveUpdate();
            BulletUpdate();
            if (IsAliveFrame())
            {
                Die();
            }
        }
    }

    protected virtual bool IsAliveFrame()
    {
        return this.aliveFrame < this.countFrame;
    }

    private float time = 0;
    protected virtual void Die(bool isPlayEffect = false)
    {
        if (isPlayEffect)
        {
            var effect = Instantiate(deadEffect, transform.parent, false) as GameObject;
            effect.transform.position = transform.position;
            StageManager.Instance.RemoveCharacter(this);
        }
        Destroy(gameObject);
    }

    protected virtual void MoveUpdate()
    {
        this.countMoveFrame++;
        var moveData = this.movePattern[nowMovePattern].Get(nowMoveIndex);
        if (moveData == null)
        {
            return;
        }
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
                BulletManager.Instance.CreateToBullet(bullet, rect, type.ToString());
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

    protected virtual void HitObj(CHARACTER_TYPE hitType, bool isBullet)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitObj = collision.collider.gameObject.transform.parent.gameObject;
        var character = hitObj.GetComponent<CharacterBase>();
        var bullet = hitObj.GetComponent<BulletBase>();
        if (character != null)
        {
            HitObj(character.type, false);
        }
        else if (bullet != null && !type.ToString().Equals(hitObj.name))
        {
            var hitType = (CHARACTER_TYPE)Enum.Parse(typeof(CHARACTER_TYPE), hitObj.name);
            HitObj(hitType, true);
            bullet.DestroyBullet(true);
        }
    }

}

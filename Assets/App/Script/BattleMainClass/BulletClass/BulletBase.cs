using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletBase : MonoBehaviour
{
    protected RectTransform rectTransform;
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
    protected Vector3 vectol
    {
        get
        {
            if (bulletData != null)
            {
                return bulletData.Vectol;
            }
            return Vector3.zero;
        }
    }
    protected int frame
    {
        get
        {
            if (bulletData != null)
            {
                return bulletData.Frame;
            }
            return 0;
        }
    }
    protected int destroyDelay
    {
        get
        {
            return this.bulletData.DestroyDelay + this.frame + this.delay;
        }
    }


    protected List<BulletObject> next;
    protected int delay;
    protected int countFrame = 0;
    protected bool isDestroy = false;
    private BulletMoveData bulletData;
    private GameObject obj;

    // Update is called once per frame
    void Update()
    {
        if (this.obj != null)
        {
            this.obj.SetActive(countFrame >= delay);
        }
        if (countFrame >= delay && isDestroy == false)
        {
            MoveUpdate();
        }
        if (countFrame >= frame + delay)
        {
            DestroyBullet();
        }
        countFrame++;
    }

    public virtual void Setup(BulletMoveData data, List<BulletObject> nextBulletes, int createDelay)
    {
        isDestroy = false;
        this.obj = transform.Find("Bullet").gameObject;
        this.bulletData = data;
        this.next = nextBulletes;
        this.delay = createDelay;
    }

    protected virtual void MoveUpdate()
    {

    }

    protected void DestroyBullet()
    {
        if (isDestroy == false)
        {
            isDestroy = true;
            for (int i = 0; i < this.next.Count; i++)
            {
                BulletManager.Instance.CreateToBullet(this.next[i], rect);
            }
        }
        if (this.countFrame >= this.destroyDelay)
        {
            Destroy(gameObject);
        }
    }

    protected virtual Vector2 GetMovePoint()
    {
        if (frame == 0)
        {
            return Vector2.zero;
        }
        float frameProgress = (float)countFrame / (float)frame;
        if (frameProgress < 0)
        {
            frameProgress = 0;
        }
        if (frameProgress > 1)
        {
            frameProgress = 1;
        }
        var move = -bulletData.GetSpeed(frameProgress);
        return new Vector2(move * vectol.x, move * vectol.y);
    }

    protected Vector2 GetPosition()
    {
        return new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y);
    }
}

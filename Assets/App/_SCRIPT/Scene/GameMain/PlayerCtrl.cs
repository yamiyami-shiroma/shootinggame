using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : CharacterBase
{
    public Vector2 downPosition;

    public override void SetUp(List<string> bulletNames, List<string> moveNames, Vector2 pos, uint alive = 0)
    {
        bulletPattern = new List<BulletMovePatternData>();
        for (int i = 0; i < bulletNames.Count; i++)
        {
            var path = "Bullet/" + bulletNames[i];
            var pattern = Resources.Load<BulletMovePatternData>("Bullet/" + bulletNames[i]);
            bulletPattern.Add(pattern);
        }
        position = pos;
        isInit = true;
    }

    protected override bool IsAliveFrame()
    {
        return false;
    }
    protected override void MoveUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            downPosition = mousePosition;//Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 pos = mousePosition;//Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 movePosition = pos - downPosition;
            // movePosition *= 30;
            Vector2 playerPosition = rect.anchoredPosition += movePosition;
            if (playerPosition.x < -225)
            {
                playerPosition.x = -225;
            }
            else if (playerPosition.x > 225)
            {
                playerPosition.x = 225;
            }
            if (playerPosition.y < -600)
            {
                playerPosition.y = -600;
            }
            else if (playerPosition.y > 600)
            {
                playerPosition.y = 600;
            }
            rect.anchoredPosition = playerPosition;
            downPosition = pos;
            // gameObject.transform.localPosition += movePosition;
        }
    }

    protected override void BulletUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            base.BulletUpdate();
        }
    }

    protected override void HitObj(CHARACTER_TYPE hitType, bool isBullet)
    {
        switch (hitType)
        {
            case CHARACTER_TYPE.BOSS:
                break;
            case CHARACTER_TYPE.ENEMY:
                Die(true);
                break;
            case CHARACTER_TYPE.ITEM:
                break;
        }
    }
}

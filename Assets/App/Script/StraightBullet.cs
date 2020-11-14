using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : BulletBase
{
    protected override void MoveUpdate()
    {
        base.MoveUpdate();
        rect.anchoredPosition = GetPosition() + GetMovePoint();
    }
}

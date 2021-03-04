using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    protected override void Die(bool isPlayEffect)
    {
        base.Die(isPlayEffect);
    }

    protected override void HitObj(CHARACTER_TYPE hitType, bool isBullet)
    {
        switch (hitType)
        {
            case CHARACTER_TYPE.PLAYER:
                Die(true);
                break;
        }
    }
}

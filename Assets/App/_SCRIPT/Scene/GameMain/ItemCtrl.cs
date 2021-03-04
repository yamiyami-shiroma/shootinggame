using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : CharacterBase
{
    public override void SetUp(List<string> bulletNames, List<string> moveNames, Vector2 pos, uint alive = 0)
    {
        position = pos;
        isInit = true;
    }

    protected override bool IsAliveFrame()
    {
        return false;
    }

    protected override void MoveUpdate()
    {
        Vector2 movePosition = new Vector2(0, -30);
        rect.anchoredPosition += movePosition;
    }
}

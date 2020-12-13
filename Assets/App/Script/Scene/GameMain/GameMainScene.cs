using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainScene : SceneBase
{
    public void ActionBtnGoToTitle()
    {
        MoveScene(SceneConst.SCENE.TITLE);
    }
}

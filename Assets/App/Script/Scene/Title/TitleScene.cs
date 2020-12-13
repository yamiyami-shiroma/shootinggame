using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SceneBase
{
    public void ActionBtnStart()
    {
        MoveScene(SceneConst.SCENE.GAME);
    }

    public void ActionBtnScore()
    {
        MoveScene(SceneConst.SCENE.MEMORY);
    }
}

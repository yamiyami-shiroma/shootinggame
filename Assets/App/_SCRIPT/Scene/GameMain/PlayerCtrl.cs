using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : CharacterBase
{
    public Vector2 downPosition;
    private void Awake()
    {
        SetUp(new List<string>() { "BulletPatternData002" }, null, new Vector3(0, -450, 0));
    }

    public override void SetUp(List<string> bulletNames, List<string> moveNames, Vector3 pos)
    {
        bulletPattern = new List<BulletMovePatternData>();
        for (int i = 0; i < bulletNames.Count; i++)
        {
            var path = "Bullet/" + bulletNames[i];
            var pattern = Resources.Load<BulletMovePatternData>("Bullet/" + bulletNames[i]);
            Debug.LogWarning("Load :" + path);
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
            var mousePosition = Input.mousePosition;
            Vector2 pos = mousePosition;//Camera.main.ScreenToWorldPoint(mousePosition);
            var movePosition = pos - downPosition;
            // movePosition *= 30;
            rect.anchoredPosition += movePosition;
            downPosition = pos;
            // gameObject.transform.localPosition += movePosition;
        }

        if (Input.GetMouseButtonDown(0))
            Debug.Log("Pressed primary button.");

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
    }
}

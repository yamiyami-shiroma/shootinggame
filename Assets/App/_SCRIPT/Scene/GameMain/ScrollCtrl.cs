using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCtrl : MonoBehaviour
{
    [SerializeField]
    private int frame;
    [SerializeField]
    private float height;
    [SerializeField]
    private List<GameObject> screens;
    public void Move()
    {
        float move = height / (float)frame;
        for (int i = 0; i < screens.Count; i++)
        {
            screens[i].transform.localPosition -= new Vector3(0, move, 0);
            if (screens[i].transform.localPosition.y <= -height)
            {
                screens[i].transform.localPosition += new Vector3(0, height * 2, 0);
            }
        }
    }

    public void Update()
    {
        Move();
    }
}

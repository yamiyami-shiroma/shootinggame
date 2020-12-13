using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnCtrl : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private GameObject _target;
    private void Awake()
    {
        if (_btn == null)
        {
            _btn = gameObject.GetComponent<Button>();
        }
        _btn.onClick.AddListener(ActionBtn);
    }

    void HandleUnityAction()
    {
    }


    public void ActionBtn()
    {
        if(_target == null){
            SceneMgr.Instance.ActionBtn(gameObject);
        }
        else{
            _target.SendMessage(CreateActionName(gameObject),SendMessageOptions.DontRequireReceiver);
        }
    }

    public static string CreateActionName(GameObject btn){
        return "ActionBtn" + btn.name;
    }
}

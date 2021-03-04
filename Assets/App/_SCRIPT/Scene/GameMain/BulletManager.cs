using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletManager : SingletonMonoBehaviour<BulletManager>
{
    private BulletDatas bulletes;
    Dictionary<uint, GameObject> bulletObjDic = new Dictionary<uint, GameObject>();
    private GameObject ObjLoad(uint id)
    {
        if (!bulletObjDic.ContainsKey(id))
        {
            bulletObjDic.Add(id, Resources.Load<GameObject>(string.Format("Bullet/Bullet{0:D3}", id)));
        }
        return bulletObjDic[id];
    }
    public BulletMoveData GetBullet(uint id)
    {
        if (bulletes == null)
        {
            bulletes = Resources.Load<BulletDatas>("Bullet/BulletData");
        }
        var bullet = bulletes.datas.Where(data => data.Id == id).FirstOrDefault();
        if (bullet == default(BulletMoveData))
        {
            Debug.LogError("not bullet id");
        }
        return bullet;
    }

    public void CreateToBullet(BulletObject bulletObj, RectTransform root, string name)
    {
        var bullet = Instantiate(ObjLoad(bulletObj.ObjId), root.parent, false) as GameObject;
        var rect = bullet.transform as RectTransform;
        bullet.name = name;
        rect.anchoredPosition = root.anchoredPosition;
        SetPostion(GetBullet(bulletObj.ID), bullet, bulletObj);
    }

    private void SetPostion(BulletMoveData data, GameObject obj, BulletObject bulletObj)
    {
        var rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition += data.Position;
        rect.localRotation = Quaternion.Euler(new Vector3(0, 0, data.Rotation));
        switch (data.BulletType)
        {
            case BulletMoveData.BULLE_TYPE.STRAIGHT:
                var bullet = obj.AddComponent<StraightBullet>();
                bullet.Setup(data, bulletObj.NextBullets, bulletObj.Delay);
                break;
        }
    }
}

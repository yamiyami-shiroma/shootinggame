using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneConst;
using System.Linq;
public class SceneMgr : SingletonMonoBehaviour<SceneMgr>
{
	private static readonly float FadeSpaceTime = 0.5f;
	protected List<GameObject> SceneLoadList;
	protected GameObject SceneRoot;
	public SCENE StartScene;
	public SceneBase NowScene;
	public override void Initilize()
	{
		SceneLoadList = new List<GameObject>();
		SceneInitilize();
		SceneRoot = GameObject.Find("SceneCanvas");
		ClearScneObj();
		base.Initilize();
	}

	private void ClearScneObj(){
		var sceneBases = SceneRoot.GetComponentsInChildren<SceneBase>();
		for(int i=0;i<sceneBases.Length;i++){
			sceneBases[i].Destroy();
		}
	}

	protected void SceneInitilize()
	{
		SceneLoad(StartScene);
	}

	public void SceneLoad(SCENE scene)
	{
		GameObject CreateScene = null;
		for(int i=0;i<SceneLoadList.Count;i++)
		{
			if (SceneLoadList[i].name.Equals(scene.ToString()))
			{
				CreateScene = SceneLoadList[i];
				break;
			}
		}
		if(CreateScene == null)
		{
			CreateScene = Resources.Load("Scene/Scene"+scene.ToString()) as GameObject;
			if(CreateScene == null)
			{
				DebugLog.Error(DebugLog.LOG_TYPE.SCENE, scene.ToString()+" not find");
				return;
			}
			SceneLoadList.Add(CreateScene);
		}

		StartCoroutine(SceneLoadCorountine(CreateScene));
	}

	protected IEnumerator SceneLoadCorountine(GameObject sceneObj)
	{
		bool isFade = true;
		StartCoroutine(FadeScene.Instance.FadeOut(()=>isFade =false));
		while (isFade)
		{
			yield return null;
		}
		if (NowScene != null)
		{
			NowScene.Destroy();
		}
		float elapsedTime = Time.time;
		NowScene = Instantiate(sceneObj,SceneRoot.transform).GetComponent<SceneBase>();
		NowScene.Initilize();
		yield return StartCoroutine(NowScene.DoInitialize());
		elapsedTime = Time.time - elapsedTime;

		// フェードアニメーションの間隔を最低限あける
		if(elapsedTime < FadeSpaceTime)
		{
			yield return new WaitForSeconds(FadeSpaceTime - elapsedTime);
		}
		StartCoroutine(FadeScene.Instance.FadeIn(() => isFade = false));
		while (isFade)
		{
			yield return null;
		}
		NowScene.OnCompleteInitialize();
	}



	public void ActionBtn(GameObject btnObj)
	{
		NowScene.ActionBtn(btnObj);
	}
}

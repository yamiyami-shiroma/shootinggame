using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeScene : SingletonMonoBehaviour<FadeScene>
{
    private static readonly float FadeMin = 0.5f;
    private static readonly float FadeTime = 2f;
    public FadeUI FadeUiView;

    public void Awake()
    {
        //StartCoroutine(FadeIn(null));
    }

    public IEnumerator FadeIn(UnityAction callback)
    {
        FadeUiView.gameObject.SetActive(true);
        FadeUiView.Range = FadeMin;
        while (FadeUiView.Range < 1)
        {
            FadeUiView.Range += Time.deltaTime / FadeTime;
            yield return null;
        }
        FadeUiView.Range = 1;
        FadeUiView.gameObject.SetActive(false);
        if (callback != null)
        {
            callback();
        }
    }

    public IEnumerator FadeOut(UnityAction callback)
    {
        FadeUiView.gameObject.SetActive(true);
        FadeUiView.Range = 1;
        while (FadeUiView.Range > FadeMin)
        {
            FadeUiView.Range -= Time.deltaTime / FadeTime;
            yield return null;
        }
        FadeUiView.Range = FadeMin;
        if (callback != null)
        {
            callback();
        }
    }

}

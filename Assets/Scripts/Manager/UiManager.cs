using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : Singleton<UiManager>
{
    public float fadeTime = 0.5f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public List<GameObject> items=new List<GameObject>();


    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        StartCoroutine("ItemsAnimation");
    }

    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, -2000f), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
    }
    IEnumerator ItemsAnimation()
    {
        foreach (var item in items)
        {
            item.transform.localScale = Vector3.zero;

        }
        foreach (var item in items)
        {
            item.transform.DOScale(1,fadeTime).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);

        }
    }


}

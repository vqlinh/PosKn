using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UiManager : MonoBehaviour
{
    public float fadeTime = 1f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;
    public Player player;
    public void PanelFadeIn()
    {
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition=new Vector3(0,1000f,0);
        rectTransform.DOAnchorPos(new Vector2(0,0),fadeTime,false).SetEase(Ease.InQuad);
        canvasGroup.DOFade(1,fadeTime);
    }
    public void PanelFadeOut()
    {
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0,0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 2000f), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
    }
    // Start is called before the first frame update
    public void Pause()
    {
        player.canMove=false;
    }
    public void Resume()
    {
        player.canMove = true;


    }
}

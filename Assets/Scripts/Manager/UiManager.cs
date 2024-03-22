using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public float fadeTime = 0.5f;
    public CanvasGroup canvasGroup;
    public RectTransform rectTransform;

    public Sprite[] avataCharacterSprites;
    public GameObject avtCharacter;
    int characterIndex;
    public List<GameObject> items=new List<GameObject>();

    private void Start()
    {
            characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);

    }
    private void Update()
    {
            avtCharacter.GetComponent<Image>().sprite = avataCharacterSprites[characterIndex];

    }
    public void PanelFadeIn()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.OutElastic);
        canvasGroup.DOFade(1, fadeTime);
        StartCoroutine("ItemsAnimation");
    }

    public void PanelFadeOut()
    {
        AudioManager.Instance.PlaySfx(SoundName.SfxButton);
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

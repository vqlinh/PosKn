using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;
    public Animator animator;
    public UiManager uiManager;
    // Start is called before the first frame update
    private void Awake()
    {
        sentences = new Queue<string>();
        uiManager=GameObject.Find("PanelStageComplete").GetComponent<UiManager>();
    }

    // Update is called once per frame
    public void StartDial(Dialogue dial)
    {
        animator.SetBool("isOpen",true);
        nameText.text = dial.nameTalk;
        Debug.Log("Starting Conversation with : " + dial.nameTalk);
        sentences.Clear();
        foreach (string sentence in dial.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();

    }
    public void Next()
    {
        if (sentences.Count == 0)
        {
            End();
            return;
        }
        string sentence=sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(SmoothText(sentence));
    }
    IEnumerator SmoothText(string  sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text+=letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void End()
    {
        Chief.Instance.isMove = true;
        animator.SetBool("isOpen", false);
        Loading.Instance.LoadingClose();
        Invoke("PanelUi",1f);
    }
    public void PanelUi()
    {

        uiManager.PanelFadeIn();
    }

}

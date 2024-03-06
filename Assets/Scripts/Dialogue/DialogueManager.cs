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
    // Start is called before the first frame update
    private void Awake()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen",true);
        nameText.text = dialogue.name;
        Debug.Log("Starting Conversation with : " + dialogue.name);
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentences();

    }
    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence=sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string  sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text+=letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void EndDialogue()
    {
        Chief.Instance.isMove = true;
        animator.SetBool("isOpen", false);
        Loading.Instance.LoadingClose();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject panel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("y a plus d'une instance de DialogueManager dans la scène");
            return;
        }

        instance = this;

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        panel.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string setence in dialogue.text)
        {
            sentences.Enqueue(setence);
        }

        DisplayNextSentece();
    }

    public void DisplayNextSentece()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentece(sentence));
    }

    public IEnumerator TypeSentece(string sentece)
    {
        dialogueText.text = "";
        foreach (char lettre in sentece.ToCharArray())
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void EndDialogue()
    {
        panel.SetActive(false);
    }
}
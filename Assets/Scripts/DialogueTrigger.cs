using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isInRange;

    private Text interacrUI;

    private void Awake()
    {
        interacrUI = GameObject.FindGameObjectWithTag("intUI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue(interacrUI);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interacrUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            interacrUI.enabled = false;
        }
    }

    public void TriggerDialogue(Text interacrUI)
    {
        interacrUI.enabled = false;
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
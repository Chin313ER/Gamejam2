using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Rendering.MaterialUpgrader;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public DialogueManager dialogueManager;

    bool playerNear = false;
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public GameObject dialoguePanel;

    private int currentLine = 0;
    void Start()
    {
        dialogueText.gameObject.SetActive(false);
    }

    string[] dialogueLines =
    {
        "สวัสดี นักเดินทาง...",
        "ข้างหน้ามีประตูแห่งเงาอยู่",
        "จงใช้พลังเงาให้ระวัง"
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Trigger");

        if (collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }

    void Update()
    {
        if (playerNear &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialoguePanel.SetActive(true);

            dialogueText.text = lines[currentLine];

            currentLine++;

            if (currentLine >= lines.Length)
            {
                dialoguePanel.SetActive(false);

                currentLine = 0;
            }
        }
    }
}
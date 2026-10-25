using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TMP_Text npcNameText;
    public TMP_Text dialogueText;
    public Player player;

    private string[] lines;
    private int currentLine;

    private bool dialogueActive = false;
    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);

        player.canMove = false;

        currentLine = 0;

        dialogueText.text = lines[currentLine];
    }

    void Update()
    {
        if (!dialogueActive)
            return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            NextLine();
        }
    }

    public void StartDialogue(string npcName, string[] dialogLines)
    {
        dialoguePanel.SetActive(true);

        npcNameText.text = npcName;

        lines = dialogLines;

        currentLine = 0;

        dialogueText.text = lines[currentLine];

        dialogueActive = true;
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= lines.Length)
        {
            CloseDialogue();
            return;
        }

        dialogueText.text = lines[currentLine];
    }

    void CloseDialogue()
    {
        dialoguePanel.SetActive(false);

        dialogueActive = false;
        player.canMove = true;
    }
}
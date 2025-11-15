using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;

    public Image leftPortrait;
    public Image rightPortrait;

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private DialogueData currentDialogue;
    private int index = 0;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        currentDialogue = data;
        index = 0;
        dialoguePanel.SetActive(true);
        ShowLine();
    }

    void ShowLine()
    {
        var line = currentDialogue.lines[index];

        nameText.text = line.speakerName;
        dialogueText.text = line.text;

        if (line.isPlayer)
        {
            leftPortrait.gameObject.SetActive(false);
            rightPortrait.gameObject.SetActive(true);
            rightPortrait.sprite = line.speakerSprite;
        }
        else
        {
            rightPortrait.gameObject.SetActive(false);
            leftPortrait.gameObject.SetActive(true);
            leftPortrait.sprite = line.speakerSprite;
        }
    }

    public void NextLine()
    {
        index++;

        if (index >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}

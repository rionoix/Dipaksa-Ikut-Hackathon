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
        // Singleton safe-guard
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Ada lebih dari 1 DialogueManager di scene. Menghancurkan instance yang lama.");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (dialoguePanel == null)
            Debug.LogError("DialogueManager: dialoguePanel belum diassign di Inspector!");

        // Pastikan panel nonaktif di awal
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel != null && dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    public void StartDialogue(DialogueData data)
    {
        Debug.Log("=== START DIALOGUE DIPANGGIL ===");
        Debug.Log("DialogueData: " + (data == null ? "NULL" : data.name));

        if (data == null)
        {
            Debug.LogError("StartDialogue diberi null DialogueData!");
            return;
        }

        currentDialogue = data;
        index = 0;

        if (dialoguePanel == null)
        {
            Debug.LogError("dialoguePanel is null! Cek assignment di Inspector.");
            return;
        }

        // Force aktikan dan bawa ke depan
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.SetAsLastSibling();
        Canvas.ForceUpdateCanvases();

        ShowLine();
    }

    void ShowLine()
    {
        if (currentDialogue == null || currentDialogue.lines == null || currentDialogue.lines.Length == 0)
        {
            Debug.LogWarning("ShowLine: tidak ada line di currentDialogue.");
            EndDialogue();
            return;
        }

        if (index < 0 || index >= currentDialogue.lines.Length)
        {
            Debug.LogWarning("ShowLine: index out of range.");
            EndDialogue();
            return;
        }

        var line = currentDialogue.lines[index];

        if (nameText == null || dialogueText == null)
        {
            Debug.LogError("Text components belum diassign (nameText/dialogueText).");
            return;
        }

        nameText.text = line.speakerName;
        dialogueText.text = line.text;

        if (leftPortrait != null && rightPortrait != null)
        {
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
    }

    public void NextLine()
    {
        index++;

        if (currentDialogue == null || currentDialogue.lines == null || index >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }

    void EndDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}

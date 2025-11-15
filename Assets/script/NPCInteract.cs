using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData dialogueFile;   // file dialog khusus NPC ini

    [Header("UI")]
    public GameObject pressEIcon;       // icon 'Press E'

    private bool playerInRange = false;

    void Start()
    {
        if (pressEIcon != null)
            pressEIcon.SetActive(false);
    }

    void Update()
    {
        // Ketika player dalam jangkauan dan menekan E → mulai dialog
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact(); // memakai fungsi baru yang digabung
        }
    }

    // === FUNGSI BARU DARI SCRIPT InteractNPC ===
    public void Interact()
    {
        if (dialogueFile != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueFile);
        }
        else
        {
            Debug.LogWarning("NPCInteract: DialogueFile belum diassign pada " + gameObject.name);
        }
    }
    // === END ===

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (pressEIcon != null)
                pressEIcon.SetActive(true);
        }

        Debug.Log("Player masuk area NPC: " + gameObject.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (pressEIcon != null)
                pressEIcon.SetActive(false);
        }
    }
}

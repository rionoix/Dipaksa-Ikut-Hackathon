using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public DialogueData dialogueFile;
    public GameObject pressEIcon;

    private bool playerInRange = false;

    void Start()
    {
        pressEIcon.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(dialogueFile);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            pressEIcon.SetActive(true);

        }

        Debug.Log("Player masuk area NPC");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            pressEIcon.SetActive(false);
        }
    }

}

using UnityEngine;

public class ShowButtonOnTrigger : MonoBehaviour
{
    public GameObject interactButton;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }
}

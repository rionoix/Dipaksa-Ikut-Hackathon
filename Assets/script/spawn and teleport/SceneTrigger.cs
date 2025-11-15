using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPopup : MonoBehaviour
{
    public GameObject confirmPanel;
    public string sceneName;

    private bool playerInside = false;

    void Update()
    {
        // Jika player di dalam area & menekan tombol
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            confirmPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            confirmPanel.SetActive(false);
        }
    }

    // Dipanggil oleh tombol UI
    public void ConfirmYes()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ConfirmNo()
    {
        confirmPanel.SetActive(false);
    }
}

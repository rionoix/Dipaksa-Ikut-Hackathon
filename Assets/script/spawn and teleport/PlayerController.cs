using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        // Jika GameManager persistent dan ingin memastikan player ditempatkan,
        // bisa panggil:
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadPlayerAtSpawn(gameObject);
        }
    }

    // movement dll...
}

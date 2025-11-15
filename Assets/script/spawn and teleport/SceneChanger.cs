using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // contoh memanggil ChangeScene via UI Button
    public void GoToSceneB()
    {
        Vector2 spawnPos = new Vector2(5f, 2f); // lokasi spawn di scene B
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeScene("SceneB", spawnPos);
        }
        else
        {
            Debug.LogError("GameManager.Instance == null. Pastikan GameManager ada dan sudah aktif.");
        }
    }
}

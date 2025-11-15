using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    // Simpan posisi spawn untuk player
    public static void SaveSpawnPosition(Vector2 position)
    {
        // Menyimpan posisi spawn ke PlayerPrefs
        PlayerPrefs.SetFloat("SpawnPosX", position.x);
        PlayerPrefs.SetFloat("SpawnPosY", position.y);
    }

    // Ambil posisi spawn yang disimpan
    public static Vector2 GetSpawnPosition()
    {
        // Mengambil posisi spawn dari PlayerPrefs, dengan default 0,0 jika belum ada data
        float x = PlayerPrefs.GetFloat("SpawnPosX", 0f);
        float y = PlayerPrefs.GetFloat("SpawnPosY", 0f);
        return new Vector2(x, y);
    }

    // Pindahkan player ke posisi spawn di scene berikutnya
    public static void LoadSpawnPosition(GameObject player)
    {
        Vector2 spawnPosition = GetSpawnPosition();
        player.transform.position = spawnPosition;
    }

    // Fungsi untuk berpindah ke scene dan simpan posisi spawn
    public void ChangeScene(string sceneName, Vector2 spawnPosition)
    {
        SaveSpawnPosition(spawnPosition); // Simpan posisi spawn sebelum berpindah scene
        SceneManager.LoadScene(sceneName); // Berpindah scene
    }
}

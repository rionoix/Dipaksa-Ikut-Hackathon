using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : MonoBehaviour
{
    [Header("Nama Scene Tujuan")]
    public string targetScene;

    [Header("Posisi Spawn di Scene Tujuan")]
    public Vector2 spawnPosition;

    public void GoToScene()
    {
        // Simpan posisi spawn secara absolut
        PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
        PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);

        // Force save agar tidak tertimpa atau menunggu commit Unity
        PlayerPrefs.Save();

        // Beri tanda bahwa ini spawn absolute
        PlayerPrefs.SetInt("UseAbsoluteSpawn", 1);
        PlayerPrefs.Save();

        // Pindah ke scene tujuan
        SceneManager.LoadScene(targetScene);
    }
}

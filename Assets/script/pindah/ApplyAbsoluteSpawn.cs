using UnityEngine;

public class ApplyAbsoluteSpawn : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("UseAbsoluteSpawn", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("SpawnX");
            float y = PlayerPrefs.GetFloat("SpawnY");

            transform.position = new Vector2(x, y);

            // Matikan flag agar tidak override lagi
            PlayerPrefs.SetInt("UseAbsoluteSpawn", 0);
        }
    }
}

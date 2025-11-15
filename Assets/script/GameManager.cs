using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Timer")]
    public float startTime = 60f;
    private float currentTime;
    public TextMeshProUGUI timerText;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    private bool isGameOver = false;

    // posisi spawn yang di-set sebelum pindah scene
    private Vector2 spawnPosition;
    private bool hasSpawnPosition = false; // indikator kalau spawnPosition valid

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // register callback saat scene selesai dimuat
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        currentTime = startTime;
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        UpdateScoreUI();
    }

    private void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame();
        }

        if (timerText != null)
            timerText.text = "Time: " + Mathf.CeilToInt(currentTime).ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null) scoreText.text = "Sampah: " + score;
    }

    void EndGame()
    {
        isGameOver = true;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (finalScoreText != null && scoreText != null)
            finalScoreText.text = "Total sampah yang anda kumpulkan adalah " + scoreText.text;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("menu");
    }

    #region Spawn and Scene Change

    // Simpan posisi spawn player (dipanggil sebelum ganti scene)
    public void SaveSpawnPosition(Vector2 position)
    {
        spawnPosition = position;
        hasSpawnPosition = true;

        // opsional: juga simpan ke PlayerPrefs jika mau persisten antar run
        PlayerPrefs.SetFloat("SpawnPosX", position.x);
        PlayerPrefs.SetFloat("SpawnPosY", position.y);
        PlayerPrefs.Save();
    }

    // Ambil posisi spawn dari PlayerPrefs (fallback)
    public Vector2 GetSavedSpawnPositionFromPrefs()
    {
        float x = PlayerPrefs.GetFloat("SpawnPosX", 0f);
        float y = PlayerPrefs.GetFloat("SpawnPosY", 0f);
        return new Vector2(x, y);
    }

    // Pindah ke scene dan simpan posisi spawn
    public void ChangeScene(string sceneName, Vector2 spawnPos)
    {
        SaveSpawnPosition(spawnPos);
        SceneManager.LoadScene(sceneName);
    }

    // Callback ketika scene selesai dimuat
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cari objek Player pada scene baru (menggunakan tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 posToUse;
            if (hasSpawnPosition)
            {
                posToUse = spawnPosition;
                // reset agar tidak selalu spawn di posisi yang sama secara tak sengaja
                hasSpawnPosition = false;
            }
            else
            {
                // fallback: coba dari PlayerPrefs (berguna jika player menjadi prefab baru)
                posToUse = GetSavedSpawnPositionFromPrefs();
            }

            // set posisi player
            player.transform.position = posToUse;
        }
        else
        {
            // tidak menemukan player — mungkin player akan di-instantiate nanti.
            // Jika kamu instantiate player lewat script, panggil method LoadPlayerAtSpawn manual.
        }
    }

    // Jika diperlukan: method manual untuk memindahkan objek player tertentu
    public void LoadPlayerAtSpawn(GameObject playerObject)
    {
        Vector2 pos = hasSpawnPosition ? spawnPosition : GetSavedSpawnPositionFromPrefs();
        playerObject.transform.position = pos;
        hasSpawnPosition = false;
    }

    private void OnDestroy()
    {
        // unregister
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion
}

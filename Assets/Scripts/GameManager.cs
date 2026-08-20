using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PulpitManager pulpitManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;

    private GameConfig gameConfig;
    private bool isGameOver;

    public GameConfig Config => gameConfig;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        LoadGameConfig();

        if (gameConfig == null)
            return;

        InitializeGame();
    }

    private void LoadGameConfig()
    {
        TextAsset jsonFile =
            Resources.Load<TextAsset>("doofus_diary");

        if (jsonFile == null)
        {
            Debug.LogError("doofus_diary.json could not be found!");
            return;
        }

        gameConfig =
            JsonUtility.FromJson<GameConfig>(jsonFile.text);

        if (gameConfig == null)
        {
            Debug.LogError("Failed to parse doofus_diary.json!");
            return;
        }

        Debug.Log("JSON LOADED SUCCESSFULLY!");
        Debug.Log("Player Speed: " + gameConfig.player_data.speed);
        Debug.Log("Min Pulpit Destroy Time: " +
                  gameConfig.pulpit_data.min_pulpit_destroy_time);
        Debug.Log("Max Pulpit Destroy Time: " +
                  gameConfig.pulpit_data.max_pulpit_destroy_time);
        Debug.Log("Pulpit Spawn Time: " +
                  gameConfig.pulpit_data.pulpit_spawn_time);
    }

    private void InitializeGame()
    {
        if (player == null)
        {
            Debug.LogError("PlayerController reference is missing!");
            return;
        }

        if (pulpitManager == null)
        {
            Debug.LogError("PulpitManager reference is missing!");
            return;
        }

        if (scoreManager != null)
            scoreManager.Initialize();

        player.Initialize(gameConfig.player_data.speed);

        pulpitManager.Initialize(gameConfig);

        Debug.Log("Game initialized successfully!");
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        Debug.Log("GAME OVER");

        // Stop player
        if (player != null)
            player.enabled = false;

        // Stop pulpit spawning
        if (pulpitManager != null)
            pulpitManager.StopSpawning();

        // Show Game Over UI
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Stop the game
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        // Reset time before loading the scene
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
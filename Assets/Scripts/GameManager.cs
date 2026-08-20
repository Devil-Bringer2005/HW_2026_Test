using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PulpitManager pulpitManager;
    [SerializeField] private ScoreManager scoreManager;

    [Header("Start Screen UI")]
    [SerializeField] private GameObject startPanel;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;

    private GameConfig gameConfig;

    private bool gameStarted;
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

        // Make sure time is running when scene loads
        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (startPanel != null)
            startPanel.SetActive(true);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        gameStarted = false;
        isGameOver = false;

        LoadGameConfig();

        if (gameConfig == null)
            return;

        // Hide player before Start is clicked
        if (player != null)
            player.gameObject.SetActive(false);

        // Disable pulpit manager
        if (pulpitManager != null)
            pulpitManager.enabled = false;
    }

    private void LoadGameConfig()
    {
        TextAsset jsonFile =
            Resources.Load<TextAsset>("doofus_diary");

        if (jsonFile == null)
        {
            Debug.LogError(
                "doofus_diary.json could not be found!"
            );

            return;
        }

        Debug.Log("JSON FOUND!");
        Debug.Log("JSON Content: " + jsonFile.text);

        gameConfig =
            JsonUtility.FromJson<GameConfig>(jsonFile.text);

        if (gameConfig == null)
        {
            Debug.LogError(
                "Failed to parse doofus_diary.json!"
            );

            return;
        }

        Debug.Log("JSON LOADED SUCCESSFULLY!");

        Debug.Log(
            "Player Speed: " +
            gameConfig.player_data.speed
        );

        Debug.Log(
            "Min Pulpit Destroy Time: " +
            gameConfig.pulpit_data.min_pulpit_destroy_time
        );

        Debug.Log(
            "Max Pulpit Destroy Time: " +
            gameConfig.pulpit_data.max_pulpit_destroy_time
        );

        Debug.Log(
            "Pulpit Spawn Time: " +
            gameConfig.pulpit_data.pulpit_spawn_time
        );
    }

    // Called by the START button
    public void StartGame()
    {
        if (gameStarted || isGameOver)
            return;

        if (gameConfig == null)
        {
            Debug.LogError("Cannot start game because GameConfig is missing.");
            return;
        }

        gameStarted = true;

        Debug.Log("GAME STARTED!");

        // Hide start screen
        if (startPanel != null)
            startPanel.SetActive(false);

        // Spawn player at (0, 2, 0)
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 2f, 0f);

            player.gameObject.SetActive(true);

            player.Initialize(
                gameConfig.player_data.speed
            );

            player.enabled = true;
        }

        // Start Pulpit Manager
        if (pulpitManager != null)
        {
            pulpitManager.enabled = true;
            pulpitManager.Initialize(gameConfig);
        }

        // Start score
        if (scoreManager != null)
        {
            scoreManager.Initialize();
        }
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        gameStarted = false;

        Debug.Log("GAME OVER");

        // Hide player
        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

        // Stop pulpit spawning
        if (pulpitManager != null)
        {
            pulpitManager.StopSpawning();
            pulpitManager.enabled = false;
        }

        // Show Game Over UI
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Pause game
        Time.timeScale = 0f;
    }
    public void AddPulpitScore()
    {
        if (scoreManager != null)
        {
            scoreManager.PulpitCompleted();
        }
        else
        {
            Debug.LogError("ScoreManager reference is missing in GameManager!");
        }
    }
    public void Retry()
    {
        // Reset time
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
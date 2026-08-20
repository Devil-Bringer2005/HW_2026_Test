using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PulpitManager pulpitManager;
    [SerializeField] private ScoreManager scoreManager;

    private GameConfig gameConfig;

    public GameConfig Config => gameConfig;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        LoadGameConfig();
        InitializeGame();
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

    private void InitializeGame()
    {
        if (gameConfig == null)
        {
            Debug.LogError(
                "Game cannot start because JSON failed to load."
            );

            return;
        }

        if (player == null)
        {
            Debug.LogError(
                "PlayerController reference is missing!"
            );

            return;
        }

        if (pulpitManager == null)
        {
            Debug.LogError(
                "PulpitManager reference is missing!"
            );

            return;
        }

        // Initialize player using speed from JSON
        player.Initialize(
            gameConfig.player_data.speed
        );

        // Initialize pulpit system using JSON
        pulpitManager.Initialize(
            gameConfig
        );

        // Initialize score system
        if (scoreManager != null)
        {
            scoreManager.Initialize();
        }

        Debug.Log("Game initialized successfully!");
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        if (player != null)
        {
            player.enabled = false;
        }

        if (pulpitManager != null)
        {
            pulpitManager.StopSpawning();
        }
    }
}
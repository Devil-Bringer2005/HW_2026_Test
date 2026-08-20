using UnityEngine;
using TMPro;

public class Pulpit : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    private float remainingTime;
    private bool playerHasEntered;

    public void Initialize(float destroyTime)
    {
        remainingTime = destroyTime;
        playerHasEntered = false;

        UpdateTimerText();

        Debug.Log(
            gameObject.name +
            " initialized. Lifetime = " +
            destroyTime
        );
    }

    private void Update()
    {
        if (remainingTime <= 0f)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime < 0f)
            remainingTime = 0f;

        UpdateTimerText();

        if (remainingTime <= 0f)
        {
            DestroyPulpit();
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = remainingTime.ToString("0.0");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(
            "PULPIT COLLISION WITH: " +
            collision.gameObject.name +
            " | TAG: " +
            collision.gameObject.tag
        );

        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision object is NOT Player.");
            return;
        }

        if (playerHasEntered)
        {
            Debug.Log("Player already counted for this Pulpit.");
            return;
        }

        playerHasEntered = true;

        Debug.Log("PLAYER ENTERED PULPIT!");

        // Directly find ScoreManager
        ScoreManager scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("SCORE MANAGER NOT FOUND!");
            return;
        }

        Debug.Log("ScoreManager found: " + scoreManager.gameObject.name);

        scoreManager.PulpitCompleted();

        Debug.Log("PulpitCompleted() CALLED!");
    }

    private void DestroyPulpit()
    {
        Destroy(gameObject);
    }
}
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

        //Debug.Log(
        //    gameObject.name +
        //    " initialized. Lifetime = " +
        //    destroyTime
        //);
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
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("Collision object is NOT Player.");
            return;
        }

        if (playerHasEntered)
        {
            return;
        }

        playerHasEntered = true;

        // Directly find ScoreManager
        ScoreManager scoreManager =
            FindFirstObjectByType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("SCORE MANAGER NOT FOUND!");
            return;
        }

        scoreManager.PulpitCompleted();
    }

    private void DestroyPulpit()
    {
        Destroy(gameObject);
    }
}
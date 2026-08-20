using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int score;

    public void Initialize()
    {
        score = 0;

        Debug.Log("ScoreManager.Initialize()");

        UpdateScoreUI();
    }

    public void PulpitCompleted()
    {
        score++;

        Debug.Log("SCORE = " + score);

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText is NULL!");
            return;
        }

        scoreText.text = "Score: " + score;

        Debug.Log(
            "UI UPDATED: " + scoreText.text
        );
    }
}
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private float scoreInterval = 1f;

    private int score;
    private float timer;

    public void Initialize()
    {
        score = 0;
        timer = 0f;
        UpdateScoreUI();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scoreInterval)
        {
            timer -= scoreInterval;

            score++;
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private int score;

    public void Initialize()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void PulpitCompleted()
    {
        score++;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText == null)
        {          
            return;
        }

        scoreText.text = "Score: " + score;    
    }
}
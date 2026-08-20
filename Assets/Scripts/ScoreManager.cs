using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;

    public int Score => score;

    public void Initialize()
    {
        score = 0;
    }

    public void AddScore()
    {
        score++;

        Debug.Log("Score: " + score);
    }
}
using UnityEngine;
using TMPro;

public class Pulpit : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    private float lifetime;
    private float remainingTime;

    public void Initialize(float destroyTime)
    {
        lifetime = destroyTime;
        remainingTime = lifetime;

        UpdateTimerText();
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
        if (timerText == null)
            return;

        timerText.text = remainingTime.ToString("0.0");
    }

    private void DestroyPulpit()
    {
        Destroy(gameObject);
    }
}
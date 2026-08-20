using UnityEngine;

public class Pulpit : MonoBehaviour
{
    private float lifetime;

    public void Initialize(float destroyTime)
    {
        lifetime = destroyTime;

        Invoke(nameof(DestroyPulpit), lifetime);
    }

    private void DestroyPulpit()
    {
        Destroy(gameObject);
    }
}
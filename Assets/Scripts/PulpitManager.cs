using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    [SerializeField] private GameObject pulpitPrefab;
    [SerializeField] private float pulpitSize = 9f;

    private GameConfig config;

    private readonly List<Pulpit> activePulpits =
        new List<Pulpit>();

    private Pulpit currentPulpit;

    private Coroutine spawnCoroutine;

    public void Initialize(GameConfig gameConfig)
    {
        config = gameConfig;

        SpawnInitialPulpit();

        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void SpawnInitialPulpit()
    {
        Vector3 spawnPosition = Vector3.zero;

        Pulpit pulpit = CreatePulpit(spawnPosition);

        currentPulpit = pulpit;
        activePulpits.Add(pulpit);
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                config.pulpit_data.pulpit_spawn_time
            );

            CleanupDestroyedPulpits();

            // Only two Pulpits are allowed.
            if (activePulpits.Count >= 2)
                continue;

            SpawnNextPulpit();
        }
    }

    private void SpawnNextPulpit()
    {
        if (currentPulpit == null)
            return;

        Vector3 spawnPosition =
            GetAdjacentPosition(currentPulpit.transform.position);

        Pulpit newPulpit = CreatePulpit(spawnPosition);

        activePulpits.Add(newPulpit);

        currentPulpit = newPulpit;
    }

    private Pulpit CreatePulpit(Vector3 position)
    {
        GameObject pulpitObject = Instantiate(
            pulpitPrefab,
            position,
            Quaternion.identity
        );

        Pulpit pulpit =
            pulpitObject.GetComponent<Pulpit>();

        if (pulpit == null)
        {
            Debug.LogError(
                "Pulpit prefab does not have a Pulpit component."
            );

            return null;
        }

        float lifetime = Random.Range(
            config.pulpit_data.min_pulpit_destroy_time,
            config.pulpit_data.max_pulpit_destroy_time
        );

        pulpit.Initialize(lifetime);

        return pulpit;
    }

    private Vector3 GetAdjacentPosition(
        Vector3 currentPosition)
    {
        Vector3[] directions =
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        // Randomize direction.
        int randomIndex =
            Random.Range(0, directions.Length);

        Vector3 direction =
            directions[randomIndex];

        return currentPosition +
               direction * pulpitSize;
    }

    private void CleanupDestroyedPulpits()
    {
        activePulpits.RemoveAll(
            pulpit => pulpit == null
        );
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
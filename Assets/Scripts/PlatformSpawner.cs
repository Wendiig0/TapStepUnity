using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float distanceBetweenPlatforms = 3f;
    [SerializeField] private int maxPlatforms = 8;

    private Vector3 nextSpawnPosition;
    private readonly Queue<GameObject> spawnedPlatforms = new Queue<GameObject>();

    private void Start()
    {
        nextSpawnPosition = new Vector3(0, 0, distanceBetweenPlatforms);
    }

    public Vector3 SpawnNextPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);
        spawnedPlatforms.Enqueue(newPlatform);

        Vector3 targetPosition = newPlatform.transform.position + Vector3.up * 1.2f;

        nextSpawnPosition += new Vector3(0, 0, distanceBetweenPlatforms);

        if (spawnedPlatforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = spawnedPlatforms.Dequeue();
            Destroy(oldPlatform);
        }

        return targetPosition;
    }
}
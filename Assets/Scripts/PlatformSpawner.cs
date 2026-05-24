using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float distanceBetweenPlatforms = 3f;
    [SerializeField] private int maxPlatforms = 8;

    private Vector3 nextSpawnPosition;
    private readonly Queue<GameObject> spawnedPlatforms = new Queue<GameObject>();

    public struct SpawnResult
    {
        public Vector3 TargetPosition;
        public PlatformShake Platform;
    }

    private void Start()
    {
        nextSpawnPosition = new Vector3(0, 0, distanceBetweenPlatforms);
    }

    public SpawnResult SpawnNextPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);
        spawnedPlatforms.Enqueue(newPlatform);

        PlatformShake platformShake = newPlatform.GetComponent<PlatformShake>();

        Vector3 targetPosition = newPlatform.transform.position + Vector3.up * 1.25f;

        nextSpawnPosition += new Vector3(0, 0, distanceBetweenPlatforms);

        if (spawnedPlatforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = spawnedPlatforms.Dequeue();
            Destroy(oldPlatform);
        }

        return new SpawnResult
        {
            TargetPosition = targetPosition,
            Platform = platformShake
        };
    }
}
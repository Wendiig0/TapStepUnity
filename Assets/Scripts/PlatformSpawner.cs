using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private float distanceBetweenPlatforms = 3f;

    private Vector3 nextSpawnPosition;

    private void Start()
    {
        nextSpawnPosition = new Vector3(0, 0, distanceBetweenPlatforms);
    }

    public Vector3 SpawnNextPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);

        Vector3 targetPosition = newPlatform.transform.position + Vector3.up * 1.2f;

        nextSpawnPosition += new Vector3(0, 0, distanceBetweenPlatforms);

        return targetPosition;
    }
}

using System.Collections;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float strength = 0.08f;

    private Vector3 startPosition;
    private Collider platformCollider;

    private void Awake()
    {
        platformCollider = GetComponent<Collider>();
    }

    public void DisableCollider()
    {
        if (platformCollider != null)
            platformCollider.enabled = false;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        startPosition = transform.position;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            Vector3 randomOffset = new Vector3(
                Random.Range(-strength, strength),
                0f,
                Random.Range(-strength, strength)
            );

            transform.position = startPosition + randomOffset;

            yield return null;
        }

        transform.position = startPosition;
    }
}
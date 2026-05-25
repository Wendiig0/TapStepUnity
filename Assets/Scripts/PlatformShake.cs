using System.Collections;
using UnityEngine;

public class PlatformShake : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float strength = 0.08f;
    [SerializeField] private GameObject[] crackVisuals;

    [SerializeField] private Renderer platformRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color crackedColor = Color.red;

    private Vector3 startPosition;
    private Collider platformCollider;

    private void Awake()
    {
        platformCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        HideCracks();

        if (platformRenderer != null)
        {
            platformRenderer.material.color = normalColor;
        }
    }

    public void DisableCollider()
    {
        if (platformCollider != null)
            platformCollider.enabled = false;
    }

    public void Shake()
    {
        foreach (GameObject crack in crackVisuals)
        {
            if (crack != null)
                crack.SetActive(true);
        }

        if (platformRenderer != null)
        {
            platformRenderer.material.color = crackedColor;
        }

        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    public void HideCracks()
    {
        foreach (GameObject crack in crackVisuals)
        {
            if (crack != null)
                crack.SetActive(false);
        }
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
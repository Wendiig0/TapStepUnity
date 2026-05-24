using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveDuration = 0.25f;
    [SerializeField] private PlatformShake currentPlatform;

    private bool isMoving;

    public bool IsMoving => isMoving;
    public PlatformShake CurrentPlatform => currentPlatform;

    public void MoveTo(Vector3 targetPosition, PlatformShake targetPlatform)
    {
        if (isMoving)
            return;

        StartCoroutine(MoveRoutine(targetPosition, targetPlatform));
    }

    private IEnumerator MoveRoutine(Vector3 targetPosition, PlatformShake targetPlatform)
    {
        isMoving = true;

        Vector3 startPosition = transform.position;
        float timer = 0f;

        while (timer < moveDuration)
        {
            timer += Time.deltaTime;
            float t = timer / moveDuration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;
        currentPlatform = targetPlatform;

        isMoving = false;
    }
}
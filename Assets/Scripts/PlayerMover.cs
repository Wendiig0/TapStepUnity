using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveDirection = 0.25f;

    private bool isMoving;

    public bool IsMoving => isMoving;

    public void MoveTo(Vector3 targetPosition)
    {
        if (!isMoving)
            StartCoroutine(MoveRoutine(targetPosition));
    }

    private IEnumerator MoveRoutine(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPosoition = transform.position;
        float timer = 0f;

        while (timer < moveDirection)
        {
            timer += Time.deltaTime;
            float t = timer / moveDirection;

            transform.position = Vector3.Lerp(startPosoition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }
}

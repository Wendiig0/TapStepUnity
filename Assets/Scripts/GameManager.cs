using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TapInput tapInput;
    [SerializeField] private TimingBar timingBar;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private PlayerMover playerMover;

    private void Awake()
    {
        tapInput = GetComponent<TapInput>();
    }

    private void OnEnable()
    {
        tapInput.OnTap += HandleTap;
    }

    private void OnDisable()
    {
        tapInput.OnTap -= HandleTap;
    }

    private void HandleTap()
    {
        if (playerMover.IsMoving)
            return;

        float v = timingBar.Value;

        if (v > 0.45f && v < 0.55f)
        {
            Debug.Log("PERFECT");

            Vector3 targetPosition = platformSpawner.SpawnNextPlatform();
            playerMover.MoveTo(targetPosition);
        }
        else
        {
            Debug.Log("MISS");
        }
    }
}

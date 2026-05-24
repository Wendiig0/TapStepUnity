using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    private TapInput tapInput;
    [SerializeField] private TimingBar timingBar;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private Rigidbody playerRb;

    private bool isGameOver;
    private int missCount;

    private void Awake()
    {
        tapInput = GetComponent<TapInput>();
    }

    private void Update()
    {
        if (isGameOver)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
#endif
        }
    }

    private void OnEnable()
    {
        tapInput.OnTap += HandleTap;
    }

    private void OnDisable()
    {
        tapInput.OnTap -= HandleTap;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleTap()
    {
        if (isGameOver || playerMover.IsMoving)
            return;

        float v = timingBar.Value;

        if (v > 0.45f && v < 0.55f)
        {
            Debug.Log("PERFECT");

            missCount = 0;

            PlatformSpawner.SpawnResult result = platformSpawner.SpawnNextPlatform();
            playerMover.MoveTo(result.TargetPosition, result.Platform);
        }
        else
        {
            Debug.Log("MISS");

            missCount++;

            if (missCount == 1)
            {
                playerMover.CurrentPlatform.Shake();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        Debug.Log("GAME OVER");

        StartCoroutine(GameOverEffect());

        Invoke(nameof(Restart), 1.5f);
    }

    private IEnumerator GameOverEffect()
    {
        Vector3 startPos = playerMover.transform.position;

        float timer = 0f;

        while (timer < 0.3f)
        {
            timer += Time.deltaTime;

            playerMover.transform.position = startPos + new Vector3(
                Random.Range(-0.05f, 0.05f),
                0,
                Random.Range(-0.05f, 0.05f)
            );

            yield return null;
        }

        playerMover.transform.position = startPos;
    }
}

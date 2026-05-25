using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TapInput tapInput;
    [SerializeField] private TimingBar timingBar;
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private int maxMissesBeforeLose = 3;


    private Coroutine comboAnim;

    private bool isGameOver;
    private int missCount;
    private int score;
    private int combo;

    private void Awake()
    {
        tapInput = GetComponent<TapInput>();
    }

    private void Start()
    {
        comboText.gameObject.SetActive(false);
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
            score++;
            Debug.Log("Score: " + score);

            AddCombo();

            timingBar.IncreaseSpeed(0.05f);

            StartCoroutine(PerfectEffect());

            if (combo >= 5)
                comboText.text = "COMBO x" + combo;
            else
                comboText.text = "Combo x" + combo;

            PlatformSpawner.SpawnResult result = platformSpawner.SpawnNextPlatform();
            playerMover.MoveTo(result.TargetPosition, result.Platform);
        }
        else
        {
            Debug.Log("MISS");

            ResetCombo();
            missCount++;

            if (missCount < maxMissesBeforeLose)
            {
                playerMover.CurrentPlatform.Shake();
            }
            else
            {
                GameOver();
            }
        }
    }

    private void AddCombo()
    {
        combo++;
        UpdateComboUI();
        AnimateCombo();
    }

    private void ResetCombo()
    {
        combo = 0;
        UpdateComboUI();
    }

    private void UpdateComboUI()
    {
        if (combo <= 0)
        {
            comboText.gameObject.SetActive(false);
        }
        else
        {
            comboText.gameObject.SetActive(true);
            comboText.text = "Combo x" + combo;
        }
    }

    private void AnimateCombo()
    {
        if (comboAnim != null)
            StopCoroutine(comboAnim);

        comboAnim = StartCoroutine(ComboPop());
    }

    private IEnumerator ComboPop()
    {
        Vector3 originalScale = comboText.transform.localScale;
        Vector3 targetScale = originalScale * 1.3f;

        float time = 0f;
        float duration = 0.15f;

        // scale up
        while (time < duration)
        {
            time += Time.deltaTime;
            comboText.transform.localScale = Vector3.Lerp(originalScale, targetScale, time / duration);
            yield return null;
        }

        time = 0f;

        // scale back
        while (time < duration)
        {
            time += Time.deltaTime;
            comboText.transform.localScale = Vector3.Lerp(targetScale, originalScale, time / duration);
            yield return null;
        }

        comboText.transform.localScale = originalScale;
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

    private IEnumerator PerfectEffect()
    {
        Vector3 originalScale = playerMover.transform.localScale;

        playerMover.transform.localScale = originalScale * 1.1f;

        yield return new WaitForSeconds(0.1f);

        playerMover.transform.localScale = originalScale;
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TapInput tapInput;
    [SerializeField] private TimingBar timingBar;
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
        float v = timingBar.Value;

        if (v > 0.45f && v < 0.55f)
        {
            Debug.Log("PERFECT");
        }
        else
        {
            Debug.Log("MISS");
        }
    }
}

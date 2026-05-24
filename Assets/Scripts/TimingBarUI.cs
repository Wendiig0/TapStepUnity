using UnityEngine;
using UnityEngine.UI;

public class TimingBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform marker;
    [SerializeField] private RectTransform bar;

    private TimingBar timingBar;
    private float barWidth;

    private void Start()
    {
        timingBar = FindFirstObjectByType<TimingBar>();
        barWidth = bar.rect.width;
    }

    void Update()
    {
        float value = timingBar.Value;

        float x = Mathf.Lerp(-barWidth / 2f, barWidth / 2f, value);

        marker.anchoredPosition = new Vector2(x, marker.anchoredPosition.y);
    }
}

using UnityEngine;

public class TimingBar : MonoBehaviour
{

    [SerializeField] private float minSpeed = 1.5f;
    [SerializeField] private float speedIncreaseAmount = 0.05f;
    [SerializeField] private float speedDecreaseAmount = 0.15f;

    public float speed = 1.5f;
    private float value; // 0 to 1
    private bool goingUp = true;

    public float Speed => speed;

    public float Value => value;

    private void Update()
    {
        if (goingUp)
        {
            value += Time.deltaTime * speed;
            if (value >= 1f)
            {
                value = 1f;
                goingUp = false;
            }
        }
        else
        {
            value -= Time.deltaTime * speed;
            if (value <= 0f)
            {
                value = 0f;
                goingUp = true;
            }
        }
    }

    public void IncreaseSpeed()
    {
        speed += speedIncreaseAmount;
    }

    public void DecreaseSpeed()
    {
        speed = Mathf.Max(minSpeed, speed - speedDecreaseAmount);
    }
}

using UnityEngine;

public class TimingBar : MonoBehaviour
{
    public float speed = 1.5f;

    private float value; // 0 to 1
    private bool goingUp = true;

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
}

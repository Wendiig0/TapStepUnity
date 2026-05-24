using UnityEngine;
using System;

public class TapInput : MonoBehaviour
{
    public event Action OnTap;

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTap?.Invoke();
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            OnTap?.Invoke();
#endif
    }
}

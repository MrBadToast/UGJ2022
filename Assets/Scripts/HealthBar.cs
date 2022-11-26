using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private float targetPoint = 1.0f;

    public void SetHealthbar(float value)
    {
        targetPoint = value;
    }

    private void FixedUpdate()
    {
        if(targetPoint != slider.value)
        {
            slider.value = Mathf.Lerp(slider.value, targetPoint, 0.3f);
        }
    }
}

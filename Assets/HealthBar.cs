using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    // sätter max värdet på health vid start
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    // sätter värdet på health
    public void SetHealth(int health)
    {
        slider.value = health;
    }

}

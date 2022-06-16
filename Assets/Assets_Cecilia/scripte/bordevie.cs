using UnityEngine.UI;
using UnityEngine;

public class bordevie : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(int health)
    {
        slider.maxValua = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f)
    }
    public void SetHealth(int health)
    {  
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}


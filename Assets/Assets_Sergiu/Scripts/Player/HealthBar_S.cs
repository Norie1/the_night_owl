using UnityEngine;
using UnityEngine.UI;

public class HealthBar_S : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image Fill;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        Fill.color = gradient.Evaluate(1f);
    }

    public void setHealth(int health)
    {
        slider.value = health;
        
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

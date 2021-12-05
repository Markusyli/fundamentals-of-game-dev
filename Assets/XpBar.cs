using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinMax(int min, int max)
    {
        slider.minValue = min;
        slider.maxValue = max;
    }

    public void SetXp(int xp)
    {
        slider.value = xp;
    }
}

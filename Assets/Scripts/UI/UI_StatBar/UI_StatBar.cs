using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public virtual void SetStat(int newValue)
    {
        // SET THE CURRENT STAT VALUE
        slider.value = newValue;
    }

    public virtual void SetMaxStat(int maxValue)
    {
        // SET THE MAX STAT VALUE
        slider.maxValue = maxValue;
        slider.value = maxValue;
    }
}

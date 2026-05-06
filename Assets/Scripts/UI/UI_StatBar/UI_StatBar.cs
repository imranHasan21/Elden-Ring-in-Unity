using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatBar : MonoBehaviour
{
    private Slider slider;
    private RectTransform rectTransform;

    [Header("Bar Options")]
    [SerializeField] protected bool scaleBarLengthWithStats = true;
    [SerializeField] protected float widthScaleMultiplayer = 1;

    protected virtual void Awake()
    {
        slider = GetComponent<Slider>();
        rectTransform = GetComponent<RectTransform>();
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

        if (scaleBarLengthWithStats)
        {
            rectTransform.sizeDelta = new Vector2(maxValue * widthScaleMultiplayer, rectTransform.sizeDelta.y);

            // RESET THE POSITION OF THE BARS BASED ON THEIR LAYOUT GROUPS SETTINGS
            PlayerUIManager.Instance.playerUIHudManager.RefreshHUD();
        }
    }
}

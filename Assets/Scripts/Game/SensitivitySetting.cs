using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensitivitySetting : MonoBehaviour
{
    private const float Min = 0.01f;
    private const float Max = 4f;
    private const float Default = 1f;

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private RectTransform defaultMarker;

    private static float ToValue(float t) =>
        t < 0.5f
            ? Mathf.Lerp(Min, Default, t * 2f)
            : Mathf.Lerp(Default, Max, (t - 0.5f) * 2f);

    private static float ToSlider(float v) =>
        v < Default
            ? Mathf.InverseLerp(Min, Default, v) * 0.5f
            : 0.5f + Mathf.InverseLerp(Default, Max, v) * 0.5f;

    void Awake()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.onValueChanged.AddListener(OnChanged);
        PlaceMarker();
    }

    void OnEnable()
    {
        float saved = PlayerPrefs.GetFloat("sensitivity", Default);
        slider.SetValueWithoutNotify(ToSlider(saved));
        UpdateText(saved);
    }

    void OnChanged(float t)
    {
        if (Mathf.Abs(t - 0.5f) < 0.02f)
        {
            t = 0.5f;
            slider.SetValueWithoutNotify(t);
        }

        float value = ToValue(t);
        FirstPersonCamera.SensitivityMultiplier = value;
        PlayerPrefs.SetFloat("sensitivity", value);
        UpdateText(value);
    }

    public void ResetToDefault() => slider.value = 0.5f;

    private void UpdateText(float value)
    {
        valueText.text = value.ToString("0.00") + "x";
    }

    private void PlaceMarker()
    {
        if (defaultMarker == null) return;
        defaultMarker.anchorMin = new Vector2(0.5f, 0f);
        defaultMarker.anchorMax = new Vector2(0.5f, 1f);
        defaultMarker.anchoredPosition = Vector2.zero;
    }
}
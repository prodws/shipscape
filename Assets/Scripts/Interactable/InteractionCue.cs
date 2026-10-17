using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionCue : MonoBehaviour
{
    [SerializeField] private GameObject root; 
    [SerializeField] private TMP_Text cueText;

    private string currentText;

    void Awake()
    {
        cueText = GetComponentInChildren<TMP_Text>();
    }

    public void Show(string text)
    {
         if (text != currentText)
        {
            currentText = text;
            cueText.text = text;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)root.transform);
        }
        if (!root.activeSelf) root.SetActive(true);
    }

    public void Hide()
    {
        if (root.activeSelf) root.SetActive(false);
    }
}

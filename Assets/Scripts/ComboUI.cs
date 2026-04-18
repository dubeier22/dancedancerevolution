using UnityEngine;
using TMPro;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI comboNumber;
    public TextMeshProUGUI comboLabel;

    void OnEnable()
    {
        JudgementManager.OnComboChanged += UpdateCombo;
    }

    void OnDisable()
    {
        JudgementManager.OnComboChanged -= UpdateCombo;
    }

    void UpdateCombo(int combo)
    {
        if (combo <= 0)
        {
            comboNumber.text = "";
            comboLabel.text = "";
            return;
        }

        comboNumber.text = combo.ToString();
        comboLabel.text = "combo";

        StopAllCoroutines();
        StartCoroutine(Pop());
    }

    System.Collections.IEnumerator Pop()
    {
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            float scale = Mathf.Lerp(1.4f, 1f, t);
            transform.localScale = Vector3.one * scale;

            yield return null;
        }
    }
}
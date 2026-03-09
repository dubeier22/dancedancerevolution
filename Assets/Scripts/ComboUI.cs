using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI comboText;

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
            comboText.text = "";
        }
        else
        {
            comboText.text = $"Combo: {combo}";
        }
    }
}


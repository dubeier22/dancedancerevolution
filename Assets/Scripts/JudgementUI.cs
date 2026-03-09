using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JudgementUI : MonoBehaviour
{
    public TextMeshProUGUI judgementText;
    public float displayTime = 0.5f;

    Coroutine currentRoutine;

    void OnEnable()
    {
        JudgementManager.OnJudgement += ShowJudgement;
    }

    void OnDisable()
    {
        JudgementManager.OnJudgement -= ShowJudgement;
    }

    void ShowJudgement(JudgementType type)
    {
        if (type == JudgementType.HoldTick)
            return;

        if (type == JudgementType.HoldComplete)
            return;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(type));
    }

    IEnumerator ShowRoutine(JudgementType type)
    {
        judgementText.text = type.ToString();
        judgementText.alpha = 1f;
        judgementText.transform.localScale = Vector3.one * 1.2f;

        yield return new WaitForSeconds(displayTime);

        judgementText.alpha = 0f;
    }
}

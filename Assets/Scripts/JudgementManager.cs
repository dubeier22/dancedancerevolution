using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class JudgementManager : MonoBehaviour
{
    public static JudgementManager Instance;


    public static event Action<JudgementType> OnJudgement;
    public static event Action<int> OnComboChanged;

    public float stellarWindow = 0.05f;
    public float greatWindow = 0.15f;
    public float goodWindow = 0.25f;

    public int score = 0;
    public int combo = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void JudgeNote(Note note, float timingDifference)
    {
        JudgementType result;

        if (Mathf.Abs(timingDifference) <= stellarWindow)
        {
            result = JudgementType.Stellar;
            score += 300;
            combo++;
        }

        else if (Mathf.Abs(timingDifference) <= greatWindow)
        {
            result = JudgementType.Great;
            score += 100;
            combo++;
        }

        else if (Mathf.Abs(timingDifference) <= goodWindow)
        {
            result = JudgementType.Good;
            score += 50;
            combo++;
        }

        else
        {
            result = JudgementType.Miss;
            combo = 0;
        }

        OnJudgement?.Invoke(result);
        OnComboChanged?.Invoke(combo);

        Debug.Log($"{result} | Timing Diff: {timingDifference:F3} | Combo: {combo} | Score: {score}");
    }

    public void HoldMissed(Note note)
    {
        combo = 0;

        OnJudgement?.Invoke(JudgementType.Miss);
        OnComboChanged?.Invoke(combo);
    }

    public void HoldComplete(Note note)
    {
        score += 200;
        combo += 2;

        OnJudgement?.Invoke(JudgementType.HoldComplete);
        OnComboChanged?.Invoke(combo);
    }

    public void HoldTick()
    {
        score += 10;

        OnJudgement?.Invoke(JudgementType.HoldTick);
        OnComboChanged?.Invoke(combo);
    }
}

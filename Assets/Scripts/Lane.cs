using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public int laneIndex;

    public Transform spawnPoint;
    public Transform hitPoint;
    public Transform removePoint;

    public Vector2 direction => (spawnPoint.position - hitPoint.position).normalized;

    public float hitWindow = 0.40f;

    public float unitsPerBeat;

    private List<Note> ActiveNotes = new List<Note>();
    private Note heldNote;

    private Conductor conductor;
    private SongData songData;
    private InputManager input;

    // Start is called before the first frame update
    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        input = InputManager.Instance;
        songData = FindObjectOfType<NoteSpawner>().songData;

        float distance = Vector2.Distance(spawnPoint.position, hitPoint.position);
        unitsPerBeat = distance / songData.beatsShownInAdvance;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (input.GetLaneDown(laneIndex))
        {
            TryHit();
        }

        if (input.GetLaneUp(laneIndex) && heldNote != null)
        {
            heldNote.ReleaseEarly();
            heldNote = null;
        }

    }

    void TryHit()
    {
        if (ActiveNotes.Count == 0) return;
        if (heldNote != null) return;           // can't hit if note is held

        for (int i = 0; i < ActiveNotes.Count; i++)
        {
            Note note = ActiveNotes[i];
            float timingDiff = conductor.songPositionInBeats - note.beat;

            if (Mathf.Abs(timingDiff) <= hitWindow)
            {
                ActiveNotes.RemoveAt(i);       // remove the note we just hit
                JudgementManager.Instance.JudgeNote(note, timingDiff);
                note.Hit();

                if (note.durationInBeats > 0)
                    heldNote = note;

                break; // only one note per key press
            }
        }
    }

    public void Miss()
    {
        JudgementManager.Instance.HoldMissed(heldNote);
        heldNote = null;
    }

    public void RegisterNote(Note note)
    {
        ActiveNotes.Add(note);
    }

    public void ReleaseJudgement(Note note)
    {
        float releaseDiff = conductor.songPositionInBeats - note.endBeat;
        JudgementManager.Instance.JudgeNote(note, releaseDiff);
        heldNote = null;
    }

    public void HoldTick(Note note)
    {
        JudgementManager.Instance.HoldTick();
    }
}

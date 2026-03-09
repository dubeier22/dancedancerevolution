using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public SongData songData;
    public Conductor conductor;
    public LaneManager laneManager;
    public GameObject notePrefab;

    private int nextNoteIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (conductor == null)
            conductor = FindObjectOfType<Conductor>();

        if (laneManager == null)
            laneManager = FindObjectOfType<LaneManager>();

        if (songData == null)
            Debug.LogError("SongData not assigned in NoteSpawner!");
        if (notePrefab == null)
            Debug.LogError("NotePrefab not assigned in NoteSpawner!");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextNoteIndex >= songData.notes.Length)
            return; // All notes spawned

        float songBeat = conductor.songPositionInBeats;
        NoteData nextNote = songData.notes[nextNoteIndex];

        // spawning beatsShownInAdvance ahead of hit time
        if (nextNote.beat <= songBeat + songData.beatsShownInAdvance)
        {
            SpawnNote(nextNote);
            nextNoteIndex++;
        }
    }

    void SpawnNote(NoteData data)
    {
        Lane lane = laneManager.lanes[data.laneIndex];

        GameObject noteObj = Instantiate(
            notePrefab,
            lane.spawnPoint.position,
            Quaternion.identity
        );

        Note note = noteObj.GetComponent<Note>();
        note.Initialize(
            data.beat,
            lane,
            conductor,
            songData.beatsShownInAdvance,
            data.duration
        );

        lane.RegisterNote(note);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public KeyCode[] laneKeys = new KeyCode[6];     // X, C, V, B, N, M

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool GetLaneDown(int laneIndex)
    {
        if (laneIndex < 0 || laneIndex >= laneKeys.Length) return false;
        return Input.GetKeyDown(laneKeys[laneIndex]);
    }

    public bool GetLaneUp(int laneIndex)
    {
        if (laneIndex < 0 || laneIndex >= laneKeys.Length) return false;
        return Input.GetKeyUp(laneKeys[laneIndex]);
    }

    public bool GetLaneHeld(int laneIndex)
    {
        if (laneIndex < 0 || laneIndex >= laneKeys.Length) return false;
        return Input.GetKey(laneKeys[laneIndex]);
    }

    public void RebindLaneKey(int laneIndex, KeyCode newKey)
    {
        if (laneIndex < 0 || laneIndex >= laneKeys.Length) return;
        laneKeys[laneIndex] = newKey;
    }
}

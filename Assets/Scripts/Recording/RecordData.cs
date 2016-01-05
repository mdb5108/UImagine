using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public struct RecordData
{
    public float      timeSinceBegin;
    public Vector3    location;
    public Quaternion rotation;
    public string[]   actions;

    public RecordData(float timeSinceBegin, Vector3 location, Quaternion rotation, string[] actions = null)
    {
        this.timeSinceBegin = timeSinceBegin;
        this.location       = location;
        this.rotation       = rotation;
        this.actions        = actions;
    }
};

using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public struct RecordData
{
    public float    timeSinceBegin;
    public Vector3  location;
    public Vector3  facing;
    public string[] actions;

    public RecordData(float timeSinceBegin, Vector3 location, Vector3 facing, string[] actions = null)
    {
        this.timeSinceBegin = timeSinceBegin;
        this.location       = location;
        this.facing         = facing;
        this.actions        = actions;
    }
};

using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public class LevelPersistentData : ScriptableObject
{
    public List<List<RecordData> > records;
    public List<Vector3> indices = new List<Vector3>();
};

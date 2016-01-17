using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public class LevelPersistentData : ScriptableObject
{
    public List<List<RecordData> > records;
    public int lifes = 4;
    public List<Vector3> indices = new List<Vector3>();
    public Vector3[] spawnerIds;
};

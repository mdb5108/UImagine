using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public class LevelPersistentData : ScriptableObject
{
    public int lifes = 4;
    public List<List<RecordData> > records;
    public Vector3[] spawnerIds;
};

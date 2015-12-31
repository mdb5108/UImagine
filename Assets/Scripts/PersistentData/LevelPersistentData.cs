using UnityEngine;

using System;
using System.Collections.Generic;

[System.Serializable]
public class LevelPersistentData : ScriptableObject
{
    public List<List<RecordData> > records;
};

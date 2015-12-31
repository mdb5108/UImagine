using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordManager : MonoBehaviour
{
    private static RecordManager instance_;
    public static RecordManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                var go = new GameObject("RecordManager (Instantiated)");
                instance_ = go.AddComponent<RecordManager>();
            }

            return instance_;
        }
    }

    public int AddRecord(List<RecordData> record)
    {
        LevelPersistentData data = PersistentManager.Instance.GetLevelPersistentData();
        data.records.Add(record);
        return data.records.Count-1;
    }

    public List<RecordData> GetRecord(int index)
    {
        return PersistentManager.Instance.GetLevelPersistentData().records[index];
    }

    public IEnumerable AllRecords()
    {
        return PersistentManager.Instance.GetLevelPersistentData().records;
    }

    public int RecordCount()
    {
        return PersistentManager.Instance.GetLevelPersistentData().records.Count;
    }
}

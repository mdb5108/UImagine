using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentManager : MonoBehaviour
{
    private static PersistentManager instance_;
    public static PersistentManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                var go = new GameObject("PersistentManager (Instantiated)");
                instance_ = go.AddComponent<PersistentManager>();
                DontDestroyOnLoad(go);
            }

            return instance_;
        }
    }

    private LevelPersistentData levelData;

    public LevelPersistentData GetLevelPersistentData()
    {
        if(levelData == null)
        {
            levelData = ScriptableObject.CreateInstance<LevelPersistentData>();
            levelData.records = new List<List<RecordData> >();
        }

        return levelData;
    }

    public void ResetData()
    {
      levelData = null;
    }
}

using UnityEngine;
using UnityEngine.Assertions;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnerManager : MonoBehaviour
{
    private static SpawnerManager instance_;
    public static SpawnerManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                instance_ = FindObjectOfType<SpawnerManager>();
                Assert.IsTrue(instance_ != null, "There needs to be a SpawnerManager placed in the level!");
            }

            return instance_;
        }
    }

    public GameObject clonePrefab;
    public float timeFrozenAfterLast = 4f;
    private bool inited = false;

    private void Start()
    {
        if(!inited)
            init();
    }

    private void init()
    {
        Vector3[] ids = PersistentManager.Instance.GetLevelPersistentData().spawnerIds;
        if(ids == null)
        {
            ids = (from spawn in FindObjectsOfType<Spawner>() select spawn.GetSpawnLocation()).ToArray();
            ArrayUtil.ShuffleArray(ids);
        }

        int count = RecordManager.Instance.RecordCount();
        for(int i = 0; i < count; i++)
        {
            GameObject go = (GameObject)Instantiate(clonePrefab, transform.position, transform.rotation);
            go.GetComponent<Clone>().cloneIndex = i;
            go.transform.SetParent(transform);
        }

        CameraManager.Instance.SetUpViewports();
        if(count < ids.Length)
        {
            Player.Instance.Teleport(ids[count]);
        }
        else
        {
            Player.Instance.Teleport(ids[ids.Length-1]);
        }
        Player.Instance.DisconnectInput(timeFrozenAfterLast*count);
    }
}

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawnLocation;

    public Vector3 GetSpawnLocation()
    {
        return spawnLocation.transform.position;
    }
}

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject clonePrefab;

    private void Start()
    {
        int count = RecordManager.Instance.RecordCount();
        for(int i = 0; i < count; i++)
        {
            GameObject go = (GameObject)Instantiate(clonePrefab, transform.position, transform.rotation);
            go.GetComponent<Clone>().cloneIndex = i;
            go.transform.SetParent(transform);
        }
    }
}

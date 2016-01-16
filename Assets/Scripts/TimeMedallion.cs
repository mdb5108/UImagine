using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeMedallion : MonoBehaviour {
    private int i = 0;
    public bool token = false;
    void OnTriggerEnter(Collider collider)
    {
            if (collider.gameObject.tag == "Player")
            {

                Player.Instance.SaveRecording();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                PersistentManager.Instance.GetLevelPersistentData().indices.Add(gameObject.transform.localPosition);
                i += 1;
                Debug.Log("Indices count :" + PersistentManager.Instance.GetLevelPersistentData().indices.Count);
                foreach (Vector3 element in PersistentManager.Instance.GetLevelPersistentData().indices)
                {
                    Debug.Log("INDICES "+element);
                }
                Debug.Log("i" +i);
                //TimeMedallionManager.Instance.DisableCollider(PersistentManager.Instance.GetLevelPersistentData().indices);
                Destroy(gameObject);
            }
       
    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }
}

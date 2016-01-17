using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeMedallion : MonoBehaviour {
    private int i = 0;
    void OnTriggerEnter(Collider collider)
    {
            if (collider.gameObject.tag == "Player")
            {

                Player.Instance.SaveRecording();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                PersistentManager.Instance.GetLevelPersistentData().indices.Add(gameObject.transform.localPosition);
                i += 1;
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

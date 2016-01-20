using UnityEngine;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

public class TimeMedallion : MonoBehaviour {
    private int i = 0;
    private float startTime;
    void OnTriggerEnter(Collider collider)
    {
            if (collider.gameObject.tag == "Player")
            {

                Player.Instance.SaveRecording();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                PersistentManager.Instance.GetLevelPersistentData().indices.Add(gameObject.transform.localPosition);
                PersistentManager.Instance.GetLevelPersistentData().medallionTimeTaken.Add(Time.time - startTime);
                i += 1;
                Destroy(gameObject);
            }
       
    }
	// Use this for initialization
	void Start () {
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void DisappearAt(float time)
    {
        StartCoroutine(DisappearAtHelper(time));
    }

    private IEnumerator DisappearAtHelper(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}

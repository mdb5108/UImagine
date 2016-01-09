using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeMedallion : MonoBehaviour {
    //private int i = 0;
    //Vector3 vector = new Vector3(0, 0, 0);
    public List<Vector3> index = new List<Vector3>();
    GameObject[] medallions;

    void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.tag == "Player" )
        {
            Destroy(gameObject);
            Player.Instance.SaveRecording();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
       
    }
	// Use this for initialization
	void Start () {

        //medallions = GameObject.FindGameObjectsWithTag("Medallion");
        //foreach(GameObject item in medallions)
        //{
        //    //index[i] = item.transform.position;
        //    //i += 1;
        //    print(item);
        //}
	}
	
	// Update is called once per frame
	void Update () {


    }
}

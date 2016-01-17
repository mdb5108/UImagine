using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeMedallionManager : MonoBehaviour {

    private int i = 0;
    private int j ;
    public List<Vector3> tokenList;
    private List<Vector3> medallionLocation = new List<Vector3>();
    private bool isInit = false;
    private string findobject;
    private GameObject medallion;

    private static TimeMedallionManager instance_;
    public static TimeMedallionManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = FindObjectOfType<TimeMedallionManager>();
                if (instance_ == null)
                {
                    var go = new GameObject("TimeMedallionManager (Instantiated)");
                    instance_ = go.AddComponent<TimeMedallionManager>();

                }
                if (!instance_.isInit)
                {
                    instance_.Init();
                }
            }

            return instance_;
        }
    }

    private void Init()
    {
        for (i = 0; i < 7; i++)
        {
                findobject = "TimeMedallion" + i.ToString();
                if (GameObject.Find(findobject)!=null)
                    {
                     medallionLocation.Add(GameObject.Find(findobject).transform.localPosition);
                    }
        }
        isInit = true;
    }



    // Use this for initialization
    void Start () {
        if (!isInit)
        {
            Init();
        }
        tokenList = PersistentManager.Instance.GetLevelPersistentData().indices;
        //DisableCollider(tokenList);
        j = tokenList.Count - 1;
        if (tokenList.Count != 0)
        {
            for (i = 0; i < medallionLocation.Count; i++)
            {
                for (j = 0; j < tokenList.Count; j++)
                {
                    if (medallionLocation[i] == tokenList[j])
                    {
                        findobject = "TimeMedallion" + i.ToString();
                        medallion = GameObject.Find(findobject);
                        medallion.GetComponent<Collider>().enabled = false;
                    }
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
        
        if ( medallionLocation.Count == PersistentManager.Instance.GetLevelPersistentData().indices.Count )// Use for win state
        {
            SceneManager.LoadScene("WinScreen");
        }
	}
}

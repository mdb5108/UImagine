using UnityEngine;
using System.Collections.Generic;

public class TimeMedallionManager : MonoBehaviour {

    private int i = 0;
    private int j ;
    public List<Vector3> tokenList;
    private GameObject[] medallions;
    private List<Vector3> medallionLocation = new List<Vector3>();
    private bool isInit = false;
    public Vector3 zero;
    private List<Vector3> indexList = new List<Vector3>();
    private string findobject;

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
                    var go = new GameObject("GameManager (Instantiated)");
                    instance_ = go.AddComponent<TimeMedallionManager>();
                    if (!instance_.isInit)
                    {
                        instance_.Init();
                    }
                }
            }

            return instance_;
        }
    }

    public void Init()
    {
        
        for (i = 0; i < 7; i++)
        {
                findobject = "TimeMedallion" + i.ToString();
                if (GameObject.Find(findobject)!=null)
            {
                medallionLocation.Add(GameObject.Find(findobject).transform.localPosition);
                //Debug.Log(medallionLocation[i]);
            }
        }
        isInit = true;
    }


    // Use this for initialization
    void Start () {
        indexList = PersistentManager.Instance.GetLevelPersistentData().indices;
        instance_.DisableCollider(indexList);
    }
    public TimeMedallionManager ()
    {
        i = 0;
        zero = new Vector3(0, 0, 0);
        tokenList = new List<Vector3>();
    }
	public void DisableCollider (List<Vector3> index)
    {
        Debug.Log("Count"+index.Count);
        j = index.Count;
        Debug.Log("j :" + j);
        tokenList = index;
        for (i = 0; i < medallionLocation.Count; i++)
        {
            if (medallionLocation[i] == index[j-1])
            {
                //Debug.Log(index[i]);
                findobject = "TimeMedallion" + i.ToString();
                var obj = GameObject.Find(findobject);
                Debug.Log(findobject);
                Debug.Log("Testing Same in Function :"+ medallionLocation[i]);
                //medallions[i].GetComponent<Collider>().enabled = false;
                obj.GetComponent<Collider>().enabled = false;
                Debug.Log(obj.GetComponent<Collider>().enabled);
                Destroy(GameObject.Find(findobject));
                Debug.Log(GameObject.Find(findobject));
            }
        }
        j += 1;
    }
	// Update is called once per frame
	void Update () {
        
        //if ( tokenList[6] != new Vector3(0,0,0))
        //{

        //}
	}
}

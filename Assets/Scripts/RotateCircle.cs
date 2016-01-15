using UnityEngine;
using System.Collections;

public class RotateCircle : MonoBehaviour {

    GameObject arcaneCircle;
    int speed = 1;

	// Use this for initialization
	void Start () {
        arcaneCircle = GameObject.Find("ArcaneCircle");
	}
	
	// Update is called once per frame
	void Update () {
        arcaneCircle.transform.Rotate(0, speed, 0);
	}
}

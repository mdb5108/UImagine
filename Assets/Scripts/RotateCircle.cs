using UnityEngine;
using System.Collections;

public class RotateCircle : MonoBehaviour {

    GameObject arcaneCircle;
    int speed = 1;

	// Update is called once per frame
	void Update () {
        transform.Rotate(0, speed, 0);
	}
}

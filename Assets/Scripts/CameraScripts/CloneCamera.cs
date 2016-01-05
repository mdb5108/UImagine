using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class CloneCamera : MonoBehaviour
{
	// Use this for initialization
	private void Awake()
    {
        CameraManager.Instance.RegisterCamera(GetComponent<Camera>());
	}
}

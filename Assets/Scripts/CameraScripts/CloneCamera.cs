using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]
public class CloneCamera : MonoBehaviour
{
  private int cameraIndex;

  // Use this for initialization
  private void Awake()
  {
    cameraIndex = CameraManager.Instance.RegisterCamera(GetComponent<Camera>());
  }

  public void GoBlack()
  {
    CameraManager.Instance.GoBlack(cameraIndex, GetComponent<Camera>());
  }
}

using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
    float startLength;
    float length;
    int layerMask;
	
	void Start() {
		offset = target.transform.position - transform.position;
        length = offset.magnitude;
        startLength = length;
        offset.Normalize();
        layerMask = 1 << LayerMask.NameToLayer("Player");
	}

    private void FixedUpdate() {
        RaycastHit hit;
        Vector3 targetToUs = transform.position - target.transform.position;
        if(Physics.Raycast(target.transform.position, targetToUs, out hit, startLength, ~layerMask))
        {
            length = Vector3.Distance(hit.point, target.transform.position);
        }
        else
        {
            length = startLength;
        }
    }

	
	void LateUpdate() {
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);

		float desiredAngle = target.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.transform.position - length*(rotation * offset);
		
		transform.LookAt(target.transform);
	}
}

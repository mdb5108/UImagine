using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float movementSpeed = 5;
    public float turningSpeed = 60;
    public bool IsGrounded;

    void OnCollisionStay(Collision collisionInfo)
    {
        IsGrounded = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        IsGrounded = false;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        transform.Translate(0, 0, vertical);
        //Jump
        if (Input.GetKeyDown("space") && IsGrounded)
        {
            //Debug.Log("Jump");
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 8), ForceMode.Impulse);
        }
    }
}

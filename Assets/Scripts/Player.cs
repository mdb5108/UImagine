using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Record))]
public class Player : MonoBehaviour {
    public float movementSpeed = 5;
    public float turningSpeed = 60;
    public float jumpForce = 8;

    private float gravity;

    private CharacterController cc;
    private Record recording;

    private void Start()
    {
        gravity = 0;
        cc = GetComponent<CharacterController>();
        recording = GetComponent<Record>();
    }

    void Update()
    {
        if(!cc.isGrounded)
            gravity -= 9.81f * Time.deltaTime;
        else
            gravity = 0f;

        //Jump
        if (Input.GetKeyDown("space") && cc.isGrounded)
        {
            gravity += 8;
            recording.RegisterAction("jump");
        }

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward * movementSpeed * Time.deltaTime;
        cc.Move(vertical + gravity*Vector3.up*Time.deltaTime);
    }
}

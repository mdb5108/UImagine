using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof(Record))]
public class Player : PlayerBase {

    public float movementSpeed = 5;
    public float turningSpeed = 60;
    public float jumpForce = 8;

    private float gravity;

    private CharacterController cc;
    private Record recording;

    private static Player instance_;
    public static Player Instance
    {
        get
        {
            if(instance_ == null)
            {
                instance_ = FindObjectOfType<Player>().GetComponent<Player>();
            }

            return instance_;
        }
    }

    protected override void Start()
    {
        base.Start();
        gravity = 0;
        cc = GetComponent<CharacterController>();
        recording = GetComponent<Record>();
    }

    protected override void Update()
    {
        base.Update();
        if(cc.isGrounded)
            gravity = 0f;

        gravity -= GameManager.Instance.GRAVITY * Time.deltaTime;

        //Jump
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            gravity += jumpForce;
            recording.RegisterAction("jump");
        }

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward * movementSpeed * Time.deltaTime;
        Move((x) => cc.Move(x), vertical + gravity*Vector3.up*Time.deltaTime);
    }

    public void SaveRecording()
    {
        RecordManager.Instance.AddRecord(recording.GetRecords());
    }
}

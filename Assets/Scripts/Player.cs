using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof(Record))]
public class Player : PlayerBase {

    static readonly float GRAVITY = 9.81f;
    public float movementSpeed = 5;
    public float turningSpeed = 60;
    public float jumpForce = 8;

    private float gravity;

    private CharacterController cc;
    private Record recording;

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

        gravity -= GRAVITY * Time.deltaTime;

        //Jump
        if (Input.GetKeyDown("space") && cc.isGrounded)
        {
            gravity += jumpForce;
            recording.RegisterAction("jump");
        }

        float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);

        Vector3 vertical = Input.GetAxis("Vertical") * transform.forward * movementSpeed * Time.deltaTime;
        Move((x) => cc.Move(x), vertical + gravity*Vector3.up*Time.deltaTime);

        if (Input.GetKeyDown("r"))
        {
            SaveRecording();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SaveRecording()
    {
        RecordManager.Instance.AddRecord(recording.GetRecords());
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent (typeof(Record))]
public class Player : PlayerBase {

    public float movementSpeed = 5;
    public float turningSpeed = 60;
    public float jumpForce = 8;

    private bool inited = false;
    private bool teleportSet = false;
    private Vector3 teleportLocation;

    private float gravity;

    public MouseAimCamera cameraScript;
    private CharacterController cc;
    private Record recording;

    public Animator animations;
    public static readonly float RUNNING_TIME_MODIFIER = 3f;

    private bool disconnectInput;
    private IEnumerator disconnectedInput;
    public static readonly float PARTICLE_STOP_STOP = 2;
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

    protected void Awake()
    {
        disconnectInput = false;
        disconnectedInput = null;
    }

    protected override void Start()
    {
        base.Start();
        gravity = 0;
        cc = GetComponent<CharacterController>();
        recording = GetComponent<Record>();

        if(teleportSet)
            cc.transform.position = teleportLocation;
        inited = true;
    }

    protected override void Update()
    {
        base.Update();
        if(cc.isGrounded)
            gravity = 0f;

        gravity -= GameManager.Instance.GRAVITY * Time.deltaTime;

        //Jump
        if (!disconnectInput && Input.GetButton("Jump") && cc.isGrounded)
        {
            gravity += jumpForce;
            recording.RegisterAction("jump");
            animations.SetTrigger("Jump");
        }
        float horizontal = 0;
        Vector3 vertical = Vector3.zero;

        if (!disconnectInput)
        {
          horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
          transform.Rotate(0, horizontal, 0);
          vertical = Input.GetAxis("Vertical") * transform.forward * movementSpeed * Time.deltaTime;
        }

        float animationSpeed = RUNNING_TIME_MODIFIER * Vector3.Dot(transform.forward, vertical);
        animations.SetBool("Moving", vertical != Vector3.zero);
        animations.SetFloat("Direction", animationSpeed);
        animations.SetBool("InAir", !cc.isGrounded);
        recording.RegisterAction(cc.isGrounded ? "Grounded" : "InAir");
        recording.RegisterAction(vertical != Vector3.zero ? "Moving" : "NotMoving");

        Move((x) => cc.Move(x), vertical + gravity*Vector3.up*Time.deltaTime);
    }

    public void Teleport(Vector3 position)
    {
        if(!inited)
        {
            teleportSet = true;
            teleportLocation = position;
        }
        else
        {
            cc.transform.position = position;
        }
    }

    public void SaveRecording()
    {
        //TODO:
        //Temporarily disappear on saving recording.  Should move this to when
        //we hit a time medallion
        recording.RegisterAction("disappear");
        RecordManager.Instance.AddRecord(recording.GetRecords());
    }

    private IEnumerator DisconnectInput_Coroutine(float time)
    {
        disconnectInput = true;
        cameraScript.enabled = false;
        yield return new WaitForSeconds(time);
        disconnectInput = false;
        cameraScript.enabled = true;
    }

    public void DisconnectInput(float time)
    {
        ConnectInput();
        disconnectedInput = DisconnectInput_Coroutine(time);
        StartCoroutine(disconnectedInput);
    }

    public void ConnectInput()
    {
        if(disconnectedInput != null)
        {
            StopCoroutine(disconnectedInput);
            disconnectedInput = null;
        }
        disconnectInput = false;
        cameraScript.enabled = true;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Clone : PlayerBase
{
    public int cloneIndex;
    public CloneCamera cloneCamera;
    private LineOfSight LineOfSight;
    private List<RecordData>        recording;
    private IEnumerator<RecordData> curRecording;
    private RecordData previousRecord;
    private RecordData currentRecord;

    private float startTime;

    public Animator animations;

    protected override void Start()
    {
        base.Start();
        recording = RecordManager.Instance.GetRecord(cloneIndex);
        curRecording = recording.GetEnumerator();
        previousRecord = curRecording.Current;
        currentRecord = curRecording.Current;
        LineOfSight = GetComponent<LineOfSight>();
    }

    protected override void Update()
    {
        base.Update();

        if(startTime == 0f)
        {
            startTime = Time.time;
        }

        float curTime = Time.time - startTime;

        currentRecord = curRecording.Current;

        bool entered = false;
        while(currentRecord.timeSinceBegin < curTime && curRecording.MoveNext())
        {
            entered = true;
            previousRecord = currentRecord;
            currentRecord = curRecording.Current;
        }
        if(entered)
        {
            transform.rotation = previousRecord.rotation;
            Move((x) => transform.position = x, previousRecord.location);
        }

        float t = (curTime - previousRecord.timeSinceBegin)/(currentRecord.timeSinceBegin-previousRecord.timeSinceBegin);
        if(Single.IsNaN(t))
        {
          t = 1f;
        }

        Vector3 newDestination = Vector3.Lerp(previousRecord.location, currentRecord.location, t);
        Move((x) => transform.position = x, newDestination);

        Quaternion newRotation;
        float angle = Quaternion.Angle(previousRecord.rotation, currentRecord.rotation);
        if(angle == 0 || angle == 180)
        {
            newRotation = currentRecord.rotation;
        }
        else
        {
            newRotation = Quaternion.Lerp(previousRecord.rotation, currentRecord.rotation, t);
        }
        transform.rotation = newRotation;

        Vector3 velocity = (currentRecord.location-previousRecord.location);
        float animationSpeed = Player.RUNNING_TIME_MODIFIER * Vector3.Dot(transform.forward, velocity);
        animations.SetFloat("Direction", animationSpeed);

        if(currentRecord.actions != null)
        {
            for(int i = 0; i < currentRecord.actions.Length; i++)
            {
                switch(currentRecord.actions[i])
                {
                    case "disappear":
                      if(t >= 1f)
                      {
                          cloneCamera.GoBlack();
                          this.gameObject.SetActive(false);
                            LineOfSight.disablerenderer();
                      }
                      break;
                    case "InAir":
                      animations.SetBool("InAir", true);
                      break;
                    case "Grounded":
                      animations.SetBool("InAir", false);
                      break;
                    case "Moving":
                      animations.SetBool("Moving", true);
                      break;
                    case "NotMoving":
                      animations.SetBool("Moving", false);
                      break;
                    case "jump":
                      animations.SetTrigger("Jump");
                      break;
                    default:
                      //Do Nothing
                      break;
                }
            }
        }
    }
}

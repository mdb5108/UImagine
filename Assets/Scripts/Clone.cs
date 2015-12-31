using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Clone : PlayerBase
{
    public int cloneIndex;

    private List<RecordData>        recording;
    private IEnumerator<RecordData> curRecording;
    private RecordData previousRecord;
    private RecordData currentRecord;

    private float startTime;

    protected override void Start()
    {
        base.Start();
        recording = RecordManager.Instance.GetRecord(cloneIndex);
        curRecording = recording.GetEnumerator();
        previousRecord = curRecording.Current;
        currentRecord = curRecording.Current;
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
            if(previousRecord.facing == Vector3.zero)
              Debug.Log(previousRecord.timeSinceBegin);
            transform.forward = previousRecord.facing;
            Move((x) => transform.position = x, previousRecord.location);
        }

        float t = (curTime - previousRecord.timeSinceBegin)/(currentRecord.timeSinceBegin-previousRecord.timeSinceBegin);
        if(Single.IsNaN(t))
        {
          t = 1f;
        }
        Vector3 newDestination = Vector3.Lerp(previousRecord.location, currentRecord.location, t);

        Move((x) => transform.position = x, newDestination);
    }
}

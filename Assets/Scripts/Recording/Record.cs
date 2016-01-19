using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Transform))]
public class Record : MonoBehaviour
{
    public  float recordDeltaTime;
    private float curRecordTime;
    private float startTime;

    private List<RecordData> records;

    private RecordData lastRecord;
    private bool recorded;

    // Use this for initialization
    private void Start ()
    {
        curRecordTime = 0;
        lastRecord = new RecordData(0, Vector3.zero, new Quaternion(), null);
        records = new List<RecordData>();
        recorded = false;
    }

    // Update is called once per frame
    private void Update ()
    {
        curRecordTime -= Time.deltaTime;
        if(curRecordTime <= 0)
        {
            MakeRecord(Time.time, transform.position, transform.rotation);
        }
    }

    private void LateUpdate()
    {
        if(recorded)
        {
            records.Add(lastRecord);
        }
        recorded = false;
    }

    public void RegisterAction(string action)
    {
        MakeRecord(Time.time, transform.position, transform.rotation, action);
    }

    private void MakeRecord(float curTime, Vector3 location, Quaternion rotation, string action = "")
    {
        if(startTime == 0f)
        {
          startTime = curTime;
        }

        if(recorded && action != "")
        {
            if(lastRecord.actions != null)
            {
                var size = lastRecord.actions.Length+1;
                var tmp = new string[size];
                Array.Copy(lastRecord.actions, tmp, size-1);
                tmp[size-1] = action;
                lastRecord.actions = tmp;
            }
            else
                lastRecord.actions = new string[]{ action };
        }
        else
        {
            lastRecord.timeSinceBegin = curTime - startTime;
            lastRecord.location = location;
            lastRecord.rotation = rotation;
            if(action != "")
                lastRecord.actions = new string[]{ action };
            else
                lastRecord.actions = null;
            curRecordTime = recordDeltaTime;
            recorded = true;
        }
    }

    public List<RecordData> GetRecords()
    {
      return records;
    }
}

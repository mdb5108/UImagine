using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(Transform))]
public class Record : MonoBehaviour
{
    public  float recordDeltaTime;
    private float curRecordTime;

    private List<RecordData> records;

    private RecordData lastRecord;
    private bool recorded;

    // Use this for initialization
    private void Start ()
    {
        curRecordTime = recordDeltaTime;
        lastRecord = new RecordData(0, Vector3.zero, Vector3.zero, null);
        records = new List<RecordData>();
        recorded = false;
    }

    // Update is called once per frame
    private void Update ()
    {
        curRecordTime -= Time.deltaTime;
        if(curRecordTime <= 0)
        {
            MakeRecord(recordDeltaTime - curRecordTime, transform.position, transform.forward);
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
        MakeRecord(recordDeltaTime-curRecordTime, transform.position, transform.forward, action);
    }

    private void MakeRecord(float timeSinceLast, Vector3 location, Vector3 facing, string action = "")
    {
        if(recorded && action != "")
        {
            if(lastRecord.actions != null)
            {
                var size = lastRecord.actions.Length+1;
                var tmp = new string[size];
                Array.Copy(tmp, lastRecord.actions, size);
                lastRecord.actions = tmp;
            }
            else
                lastRecord.actions = new string[]{ action };
        }
        else
        {
            lastRecord.timeSinceLast = timeSinceLast;
            lastRecord.location = location;
            lastRecord.facing = facing;
            if(action != "")
                lastRecord.actions = new string[]{ action };
            else
                lastRecord.actions = null;
            curRecordTime = recordDeltaTime;
            recorded = true;
        }
    }
}

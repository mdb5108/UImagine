using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected delegate void MoveDelegate(Vector3 newPosition);
    protected void Move(MoveDelegate moveFunction, Vector3 newPosition)
    {
        //Do anything needed such as animation and sound
        moveFunction(newPosition);
    }
}

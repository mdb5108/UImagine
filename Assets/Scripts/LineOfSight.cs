using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour
{
    public float fieldOfViewAngle;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;               // Whether or not the player is currently sighted.
    private SphereCollider col;              // Reference to the sphere collider trigger component.
    private GameObject player;               // Reference to the player.

    void Awake()
    {
        // Setting up the references.
        col = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        // If the player has entered the trigger sphere...
        if (other.gameObject.tag == "Player")
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        playerInSight = true;
                        Debug.Log("InSight");
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            // ... the player is not in sight.
            playerInSight = false;
    }
}

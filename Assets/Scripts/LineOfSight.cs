using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LineOfSight : MonoBehaviour
{
    public float fieldOfViewAngle;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;               // Whether or not the player is currently sighted.
    private SphereCollider col;              // Reference to the sphere collider trigger component.
    //private GameObject player;               // Reference to the player.
    private LineRenderer lineRenderer;
    public Material Found;
    public Material Death;
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
                        var distance = Vector3.Distance(Player.Instance.transform.position, transform.position);
                        //Debug.Log(distance);
                        if (distance <= 15)
                        {
                            lineRenderer = GetComponent<LineRenderer>();
                            lineRenderer.enabled = true;
                            lineRenderer.material = Found;
                            lineRenderer.SetPosition(0, Player.Instance.transform.position);
                            lineRenderer.SetPosition(1, transform.position);
                            lineRenderer.SetWidth(.45f, .45f);
                            playerInSight = true;
                        }

                        if (distance <= 7)
                        {
                            
                            lineRenderer.enabled = true;
                            lineRenderer.material = Death;
                            lineRenderer.SetPosition(0, Player.Instance.transform.position);
                            lineRenderer.SetPosition(1, transform.position);
                            lineRenderer.SetWidth(.45f, .45f);
                            playerInSight = true;
                            //GameManager.Instance.LoseLifeRedo();
                        }

                    }
                }
            }
            else
            {
                disablerenderer();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {// ... the player is not in sight.
            playerInSight = false;
            disablerenderer();
        }
    }
    void disablerenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }
}

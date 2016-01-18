using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LineOfSight : MonoBehaviour
{
    public float fieldOfViewAngle;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;               // Whether or not the player is currently sighted.
    private SphereCollider col;              // Reference to the sphere collider trigger component.
    private LineRenderer lineRenderer;
    public Material Found;
    public Material Death;
    private GetStoned getstoned;
    private GlowWarning glowwarning;
    private bool Stoned= false;
    public float duration = 5.0F;

    private static readonly float SAFE_ZONE = 15;
    private static readonly float DANGER_ZONE = 10;
    private static readonly float DEATH_ZONE = 4;

    void Awake()
    {
        // Setting up the references.
        col = GetComponent<SphereCollider>();
        getstoned   = Player.Instance.GetComponent<GetStoned>();
        glowwarning = Player.Instance.GetComponent<GlowWarning>();
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
                        if (distance <= DEATH_ZONE)
                        {
                            if (Stoned == false)
                            {
                                glowwarning.GlowWarningEffect(0);
                                Player.Instance.DisconnectInput(Player.PARTICLE_STOP_STOP);
                                getstoned.StoneEffect();
                                Stoned = true;
                            }
                        }
                        else if (distance <= DANGER_ZONE)
                        {

                            lineRenderer.enabled = true;
                            float dangerLength = DANGER_ZONE-DEATH_ZONE;
                            float lerp = 1 - (distance - DEATH_ZONE)/dangerLength;
                            lineRenderer.material.Lerp(Found, Death, lerp);
                            lineRenderer.SetPosition(0, Player.Instance.transform.position);
                            lineRenderer.SetPosition(1, transform.position);
                            lineRenderer.SetWidth(.45f, .45f);
                            playerInSight = true;
                            glowwarning.GlowWarningEffect(2);

                        }
                        else if (distance <= SAFE_ZONE)
                        {
                            lineRenderer = GetComponent<LineRenderer>();
                            lineRenderer.enabled = true;
                            lineRenderer.material = Found;
                            lineRenderer.SetPosition(0, Player.Instance.transform.position);
                            lineRenderer.SetPosition(1, transform.position);
                            lineRenderer.SetWidth(.45f, .45f);
                            playerInSight = true;
                            glowwarning.GlowWarningEffect(1);
                        }


                    }
                }
            }
            else
            {
                disablerenderer();
                glowwarning.GlowWarningEffect(0);
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
    public void disablerenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        glowwarning.GlowWarningEffect(0);
    }
}

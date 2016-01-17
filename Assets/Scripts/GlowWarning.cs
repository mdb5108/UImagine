using UnityEngine;
using System.Collections;

public class GlowWarning : MonoBehaviour {

    int count;
	
    public GameObject CharObj;    //the name of the object

    public GameObject GlowWarningY;
    public GameObject GlowWarningR;
    public ParticleSystem GlowYP;
    public ParticleSystem GlowRP;
    // Use this for initialization
    void Start () {
        GlowYP.Stop();
        GlowRP.Stop();
    }

	public void GlowWarningEffect(int type){
        if(type == 0)
        {
            GlowYP.Stop();
            GlowRP.Stop();
        }
        else if (type == 1)
        {
	    Debug.Log("hit yellow");
            GlowYP.Play();
            GlowRP.Stop();
        }
        else if (type == 2)
        {
	    Debug.Log("hit red");
            GlowRP.Play();
            GlowYP.Stop();
        }
    }
}

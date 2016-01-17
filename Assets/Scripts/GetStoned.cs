using UnityEngine;
using System.Collections;


/**** this file changed the material the character uses and emit the stoned particle effects ****/
public class GetStoned : MonoBehaviour {
    public GameObject CharObj;    //the name of the object
    public GameObject StonedEffect1;
    public GameObject StonedEffect2;
    public ParticleSystem stoneSteam;
    public ParticleSystem stoneLight;
    

    // Use this for initialization
    void Start () {
	if(stoneSteam.isPlaying)
		stoneSteam.Stop();

	if(stoneLight.isPlaying)
		stoneLight.Stop();
    }

	public void StoneEffect(){
				Debug.Log("Hit");
        //Debug.Log("Called");
	if(!stoneSteam.isPlaying)
		stoneSteam.Play();
        Renderer render1 = CharObj.GetComponent<Renderer>();
        render1.material = Resources.Load("Stone_M") as Material;
	if(!stoneLight.isPlaying)
		stoneLight.Play();

        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(Player.PARTICLE_STOP_STOP);
	if(stoneSteam.isPlaying)
		stoneSteam.Stop();
	if(stoneLight.isPlaying)
		stoneLight.Stop();
        GameManager.Instance.LoseLifeRedo();
    }
}

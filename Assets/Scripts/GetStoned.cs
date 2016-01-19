using UnityEngine;
using System.Collections;


/**** this file changed the material the character uses and emit the stoned particle effects ****/
public class GetStoned : MonoBehaviour {
    public GameObject CharObj;    //the name of the object
    public ParticleSystem stoneSteam;
    public ParticleSystem stoneLight;

    public Material stoneMaterial;


    // Use this for initialization
    void Start () {
        stoneSteam.Stop();
        stoneLight.Stop();
    }

    public void StoneEffect(){
        stoneSteam.Play();
        Renderer render1 = CharObj.GetComponent<Renderer>();
        Material[] newMaterials = new Material[render1.materials.Length];
        for(int i = 0; i < render1.materials.Length; i++)
        {
            newMaterials[i] = stoneMaterial;
        }
        render1.materials = newMaterials;
        stoneLight.Play();

        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        yield return new WaitForSeconds(Player.PARTICLE_STOP_STOP);
        stoneSteam.Stop();
        stoneLight.Stop();
        GameManager.Instance.LoseLifeRedo();
    }
}

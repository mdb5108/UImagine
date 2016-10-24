using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameMainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey && !Input.GetButton("Cancel"))
        {
            PersistentManager.Instance.ResetData();
            SceneManager.LoadScene("Vega2016");
        }
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}

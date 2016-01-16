using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour {
    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            PersistentManager.Instance.ResetData();
            SceneManager.LoadScene("YueTestLevel");
        }
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}

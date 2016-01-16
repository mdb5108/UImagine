using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance_;
    public static GameManager Instance
    {
        get
        {
            if(instance_ == null)
            {
                instance_ = FindObjectOfType<GameManager>();
                if(instance_ == null)
                {
                    var go = new GameObject("GameManager (Instantiated)");
                    instance_ = go.AddComponent<GameManager>();
                }
            }

            return instance_;
        }
    }

    public readonly float GRAVITY = 9.81f;
    public void TimerZero()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}

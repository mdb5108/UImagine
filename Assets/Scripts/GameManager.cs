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

    public readonly float GRAVITY = 15f;
    public void NextIteration()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LifeLost()
    {
        PersistentManager.Instance.GetLevelPersistentData().lifes -= 1;
    }

    public void LoseLifeRedo()
    {
        LifeLost();
        NextIteration();
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

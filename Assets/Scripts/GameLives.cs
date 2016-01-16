using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
public class GameLives : MonoBehaviour
{
    public Sprite health1;//One Life Left
    public Sprite health2;//Two Life Left
    public Sprite health3;//Three Life Left
    public Sprite health4;//Four Life Left
    public Image LifesImage;
    void Update()
    {
        switch(PersistentManager.Instance.GetLevelPersistentData().lifes)
        {
            case 4:
                LifesImage.sprite = health4;
                break;
            case 3:
                LifesImage.sprite = health3;
                break;
            case 2:
                LifesImage.sprite = health2;
                break;
            case 1:
                LifesImage.sprite = health1;
                break;
            case 0:
                SceneManager.LoadScene("DeathScreen");
                break;
        }
    }
}
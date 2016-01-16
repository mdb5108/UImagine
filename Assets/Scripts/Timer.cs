using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private float startTime;
    private float restSeconds;
    private int roundedRestSeconds;
    private float displaySeconds;
    private float displayMinutes;
    public GUISkin skin;
    public int CountDownSeconds;
    private float TimeProgressed;
    private Text timertext;
    private string timetext;
    // Use this for initialization 

        public void MedallionCollected()
    {
        startTime = Time.time;
    }
    void Start()
    {
        startTime = Time.time;
        timertext = GameObject.Find("Canvas").transform.FindChild("Timer").GetComponent<Text>();
    }

    void Update()
    {
        TimeProgressed = Time.time - startTime;
        restSeconds = CountDownSeconds - (TimeProgressed);
        roundedRestSeconds = Mathf.CeilToInt(restSeconds);
        displaySeconds = roundedRestSeconds % 60;
        displayMinutes = (roundedRestSeconds / 60) % 60;
        timetext = (displayMinutes.ToString("0") + ":");
        if (roundedRestSeconds > 0)
        {
            timetext = timetext + displaySeconds.ToString("00");
            timertext.text = "Time Left : " + timetext;
        }
        else if (roundedRestSeconds <= 0)
        {
            startTime = Time.time;

            GameManager.Instance.TimerZero();
        }
        else
        {
            timetext = timetext + "0" + displaySeconds.ToString("00");
            
        }
    }
}
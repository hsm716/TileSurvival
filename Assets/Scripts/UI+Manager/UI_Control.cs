using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    public static UI_Control instance;
    [SerializeField]
    private GameObject gameui;
    [SerializeField]
    private Text Timer;
    [SerializeField]
    private Text Point;
    [SerializeField]
    private Image hpbar;


    AudioSource clicksound;

    private Vector3 MouseStartPos;
    void Awake()
    {
        instance = this;
        clicksound = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        Timer.text = string.Format("{0:00}:{1:00}", GameManager.instance.m, GameManager.instance.s);
        Point.text = "SCORE : " + GameManager.instance.point_.ToString();
        hpbar.fillAmount = Player.instance.Player_HP / 100f;
        if (GameManager.instance.paused)
        {
            gameui.SetActive(true);
            
            Time.timeScale = 0f;
        }
        if (!GameManager.instance.paused)
        {
            gameui.SetActive(false);
            Time.timeScale = 1f;
        }


    }
    public void OnMenuClick()
    {
        clicksound.Play();
        LoadingSceneController.Instance.LoadScene("SelectScene");
    }
    public void OnButtonClick()
    {
        clicksound.Play();
        GameManager.instance.paused = true;
    }

    public void ClickKeepGoing()
    {
        clicksound.Play();
        GameManager.instance.paused = false;

    }
    public void ClickRestart()
    {
        clicksound.Play();
        if (SceneManager.GetActiveScene().name == "GameScene_Night")
        {
            LoadingSceneController.Instance.LoadScene("GameScene_Night");
            GameManager.instance.paused = false;
        }
        else
        {
            int rn = Random.Range(2, 4);
            if (rn == 2)
            {
                LoadingSceneController.Instance.LoadScene("GameScene2");
                GameManager.instance.paused = false;
            }
            else if (rn == 3)
            {
                LoadingSceneController.Instance.LoadScene("GameScene3");
                GameManager.instance.paused = false;
            }
        }

    }
    public void ClickTest()
    {
        clicksound.Play();
        LoadingSceneController.Instance.LoadScene("GameScene_Night");
    }
    public void ClickExit()
    {
        clicksound.Play();
        Application.Quit();
    }
    public void OnLogOut()
    {
        clicksound.Play();
        SceneManager.LoadScene("LoginScene");
    }

}

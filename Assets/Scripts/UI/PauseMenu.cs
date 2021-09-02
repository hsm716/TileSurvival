using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject gameui;
    public bool paused;

    AudioSource clicksound;

    private Vector3 MouseStartPos;
    void Awake()
    {
        clicksound = GetComponent<AudioSource>();
        paused = false;
    }
    private void Update()
    {
        if (paused)
        {
            gameui.SetActive(true);
            
            Time.timeScale = 0f;
        }
        if (!paused)
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
        paused = true;
    }

    public void ClickKeepGoing()
    {
        clicksound.Play();
        paused = false;

    }
    public void ClickRestart()
    {
        clicksound.Play();
        if (SceneManager.GetActiveScene().name == "GameScene_Night")
        {
            LoadingSceneController.Instance.LoadScene("GameScene_Night");
            paused = false;
        }
        else
        {
            int rn = Random.Range(2, 4);
            if (rn == 2)
            {
                LoadingSceneController.Instance.LoadScene("GameScene2");
                paused = false;
            }
            else if (rn == 3)
            {
                LoadingSceneController.Instance.LoadScene("GameScene3");
                paused = false;
            }
        }
        //SceneManager.LoadScene(Random.Range(2, 4));
        //SceneManager.LoadScene("GameScene2");

    }
    public void ClickTest()
    {
        clicksound.Play();
        LoadingSceneController.Instance.LoadScene("GameScene_Night");
        //SceneManager.LoadScene("TestScene");
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

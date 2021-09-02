using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hp_bar : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Player playerLogic;
    [SerializeField]
    private Image hpbar;
    [SerializeField]
    private Text Timer;
    [SerializeField]
    private Text Point;

    /*[SerializeField]
    private Text PP;*/


    public static int point_;
    string strTime;
    public static float s;
    public static int m;
    void Awake()
    {
        playerLogic = player.GetComponent<Player>();
        s = 0f;
        m = 0;
        point_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        s += Time.deltaTime;
        if (s > 59)
        {
            s = 0;
            m += 1;
        }
       

        Timer.text = string.Format("{0:00}:{1:00}", m,s);
        Point.text = "점수 : "+ point_.ToString();
       // PP.text = "좌표 : " + playerLogic.ShotPoint.ToString();
        PlayerHPbar();
    }
    public void PlayerHPbar()
    {
        float HP = playerLogic.Player_HP;
        hpbar.fillAmount = HP / 100f;
       
    }

  
}

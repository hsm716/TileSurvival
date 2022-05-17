using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World_UI_Control : MonoBehaviour
{
    public Image FlameBar;
    public Image DashBar;
    public Image GunBulletBar;
    [SerializeField]
    private GameObject player;

    private AudioSource wind;
    Player playerLogic;


    Vector3 offset_;
    Vector3 FlameBar_offset;
    Vector3 GunBulletBar_offset;

    private bool Dash_Onoff;
    private bool Flame_Onoff;
    private bool Gun_Onoff;
    void Awake()
    {
        wind = GetComponent<AudioSource>();
        playerLogic = player.GetComponent<Player>();
        Flame_Onoff = false;
        Dash_Onoff = false;
        Gun_Onoff = false;
        offset_ = new Vector3(0f, 3f, 2f);
    }
    void Update()
    {
        transform.position = player.transform.position+ offset_;
        if (playerLogic.DashGage==100){ Dash_Onoff = true;  wind.Play();}
        if (playerLogic.FlameGage == 100){  Flame_Onoff = true;         }
        if (playerLogic.gunBulletGage > 0){  Gun_Onoff = true;       }

        
        if (playerLogic.DashGage <= 0)
        {
            Dash_Onoff = false;
            wind.Stop();
            playerLogic.DashGage = 0f;
        }
        if (playerLogic.FlameGage <= 0)
        {
            Flame_Onoff = false;
            playerLogic.FlameGage = 0;
        }
        if (playerLogic.gunBulletGage <= 0)
        {
            Gun_Onoff = false;
        }

        if (Dash_Onoff)
        {
            playerLogic.DashGage -= 0.6f;
        }
        if (Flame_Onoff)
        {
            playerLogic.FlameGage -= 0.3f;
        }


        float DashGage = playerLogic.DashGage;
        DashBar.fillAmount = DashGage / 100f;
        if (Dash_Onoff) DashBar.gameObject.SetActive(true);
        else DashBar.gameObject.SetActive(false);

        float gunGage = playerLogic.gunBulletGage;
        GunBulletBar.fillAmount = gunGage / 100f;
        if (Gun_Onoff) GunBulletBar.gameObject.SetActive(true);
        else GunBulletBar.gameObject.SetActive(false);

        float FlameGage = playerLogic.FlameGage;
        FlameBar.fillAmount = FlameGage / 100f;
        if (Flame_Onoff) FlameBar.gameObject.SetActive(true);
        else FlameBar.gameObject.SetActive(false);

    }
}

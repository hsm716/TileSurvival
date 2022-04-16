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


    Vector3 DashBar_offset;
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
        DashBar_offset = new Vector3(0f, 3f, 2f);
        FlameBar_offset = new Vector3(0f, 3f, 3f);
        GunBulletBar_offset = new Vector3(0f, 3f, -2f);
    }
    void Update()
    {
        if (playerLogic.DashGage==100)
        {
            Dash_Onoff = true;
            wind.Play();
        }
        if (playerLogic.DashGage <= 0)
        {
            Dash_Onoff = false;
            wind.Stop();
            playerLogic.DashGage = 0f;
        }
        if (Dash_Onoff)
        {
            playerLogic.DashGage -= 0.6f;
            
            DashBar_();
        }

        if (playerLogic.FlameGage == 100)
        {
            Flame_Onoff = true;

            
        }
        if (playerLogic.FlameGage <= 0)
        {
            Flame_Onoff = false;

            playerLogic.FlameGage = 0;
        }
        if (Flame_Onoff)
        {
            playerLogic.FlameGage -= 0.3f;
            FlameBar_();
        }

        if (playerLogic.gunBulletGage == 100)
        {
            Gun_Onoff = true;
        }
        if (Gun_Onoff)
        {
            GunBar_();
        }
        if (playerLogic.gunBulletGage <= 0)
        {
            Gun_Onoff = false;
        }
    }
    public void GunBar_()
    {
        float gunGage = playerLogic.gunBulletGage;
        GunBulletBar.fillAmount = gunGage / 100f;
        GunBulletBar.transform.position = player.transform.position+GunBulletBar_offset;
    }
    public void DashBar_()
    {
        float DashGage = playerLogic.DashGage;
        DashBar.fillAmount = DashGage / 100f;
        DashBar.transform.position = player.transform.position+DashBar_offset;
    }
    public void FlameBar_()
    {
        float FlameGage = playerLogic.FlameGage;
        FlameBar.fillAmount = FlameGage / 100f;
        FlameBar.transform.position = player.transform.position+FlameBar_offset;
    }
}

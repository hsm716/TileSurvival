using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_act : MonoBehaviour
{
    AudioSource shoot;

    [SerializeField]
    private Transform ShotPos;
    [SerializeField]
    private GameObject ShotFlash;


    
    public bool isAct = false;
    private void Awake()
    {
        shoot = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Player.instance.gunBulletGage == 100&&!isAct)
        {
            isAct = true;
            StartCoroutine(bullet_spawn(0.1f));
            StartCoroutine(flash_spawn());
        }
        if (Player.instance.gunBulletGage <= 0)
        {
            isAct = false;
        }

    }

    IEnumerator bullet_spawn(float sec)
    {
        while (Player.instance.gunBulletGage>=0)
        {
            shoot.Play();
            GameObject GO = Pooling_Control.instance.GetQueue(200);
            GO.transform.position = ShotPos.position;
            GO.transform.rotation = ShotPos.rotation;
            GO.SetActive(true);
            GO.GetComponent<Bullet>().LifeTime();

            Rigidbody b_rgbd = GO.GetComponent<Rigidbody>();
            b_rgbd.velocity = Vector3.zero;
            b_rgbd.AddForce(ShotPos.forward * 75f, ForceMode.Impulse);
            
            Player.instance.gunBulletGage -= 1;
            yield return new WaitForSeconds(sec);
        }

    }
    IEnumerator flash_spawn()
    {
        while (Player.instance.gunBulletGage >= 0)
        {
            ShotFlash.SetActive(true);
            yield return new WaitForSeconds(0.005f);
            ShotFlash.SetActive(false);
            yield return new WaitForSeconds(0.095f);
        }
    }
   
    
}

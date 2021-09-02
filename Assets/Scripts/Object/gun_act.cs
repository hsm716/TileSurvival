using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_act : MonoBehaviour
{
    AudioSource shoot;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject ShotFlash;
    [SerializeField]
    private GameObject player;
    private Player playerLogic;

    Rigidbody rgbd;
    private void Awake()
    {
        shoot = GetComponent<AudioSource>();
        playerLogic = player.GetComponent<Player>();
    }
    private void Update()
    {
        if (playerLogic.gunBulletGage == 100)
        {
            StartCoroutine(bullet_spawn(0.1f));
            StartCoroutine(flash_spawn());
        }

    }

    IEnumerator bullet_spawn(float sec)
    {
        while (playerLogic.gunBulletGage>=0)
        {
            shoot.Play();
            Instantiate(bullet, transform.position, player.transform.rotation);
            playerLogic.gunBulletGage -= 1;
            yield return new WaitForSeconds(sec);
        }
    }
    IEnumerator flash_spawn()
    {
        while (playerLogic.gunBulletGage >= 0)
        {
            ShotFlash.SetActive(true);
            yield return new WaitForSeconds(0.005f);
            ShotFlash.SetActive(false);
            yield return new WaitForSeconds(0.095f);
        }
    }
   
    
}

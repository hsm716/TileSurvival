using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject Trap;
    [SerializeField]
    private GameObject DestroyEffect;
    private int GunN;
    private GameObject player;
    private Player playerLogic;
    float time = 0;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<Player>();
        GunN = 2;

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLogic.gunBulletGage = 100;
            GunN -= 1;
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            if (GunN == 0)
            {
                Instantiate(Trap, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

    }

}

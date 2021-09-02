using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTile : MonoBehaviour
{
    [SerializeField]
    private GameObject Trap;
    [SerializeField]
    private GameObject DestroyEffect;
    private int dashN;
    private GameObject player;
    float time = 0;
    Player playerLogic;
    void Awake()
    {
        player= GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<Player>();
        dashN = 2;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLogic.DashGage = 100;
            dashN -= 1;
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            if (dashN == 0)
            {
                Instantiate(Trap, transform.position, transform.rotation);

                Destroy(this.gameObject);
            }
            
        }
    }

}

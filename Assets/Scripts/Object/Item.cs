using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int ItemMode;

    GameObject player;
    
    private AudioSource ItemSound;
    Player playerLogic;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<Player>();
        ItemSound = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ItemMode == 1)
            {
                if (playerLogic.Player_HP == 100f)
                {
                    Hp_bar.point_ += 100;
                }
                playerLogic.Player_HP += 10f;
                

            }
            ItemSound.Play();
            Destroy(gameObject,0.4f);
        }
        
        
    }
}

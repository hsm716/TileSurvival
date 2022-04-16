using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int ItemMode;


    private AudioSource ItemSound;
    void Awake()
    {
        ItemSound = GetComponent<AudioSource>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ItemMode == 1)
            {
                if (Player.instance.Player_HP >= 100f)
                {
                    GameManager.instance.GetPoint(100);
                }
               Player.instance.Player_HP += 10f;
                

            }
            ItemSound.Play();
            StartCoroutine(StopAct());
            
        }
        
        
    }
    IEnumerator StopAct()
    {
        yield return new WaitForSeconds(0.4f);
        Pooling_Control.instance.InsertQueue(this.gameObject, 7);
    }
}

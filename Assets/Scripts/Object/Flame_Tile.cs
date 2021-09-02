using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject Trap;
    [SerializeField]
    private GameObject DestroyEffect;
    private int FlameN;
    private GameObject player;
    private Player playerLogic;
    float time = 0;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLogic = player.GetComponent<Player>();
        FlameN = 2;

    }
  /*  private void Update()
    {
        time += Time.deltaTime;

        if (time > 60)
        {
            Destroy(this.gameObject);
        }
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLogic.FlameGage = 100f;
            FlameN -= 1;
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            if (FlameN == 0)
            {
                Instantiate(Trap, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

    }


}

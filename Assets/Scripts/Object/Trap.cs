using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private GameObject DashTile;
    [SerializeField]
    private GameObject FlameTile;
    [SerializeField]
    private GameObject AggroTile;
    [SerializeField]
    private GameObject GunTile;

    [SerializeField]
    private GameObject DestroyEffect;



    [SerializeField]
    private GameObject Arroow;


    AudioSource pew;

    float time = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Arroow, transform.position+new Vector3(2f,2f,0f), Quaternion.LookRotation(new Vector3(1f, 0, 0)));
            Instantiate(Arroow, transform.position+ new Vector3(-2f, 2f, 0f), Quaternion.LookRotation(new Vector3(-1f, 0, 0)));
            Instantiate(Arroow, transform.position+ new Vector3(0f, 2f, -2f), Quaternion.LookRotation(new Vector3(0, 0, -1f)));
            Instantiate(Arroow, transform.position+ new Vector3(0f, 2f, 2f), Quaternion.LookRotation(new Vector3(0, 0, 1f)));
            Instantiate(Arroow, transform.position + new Vector3(2f, 2f, 2f), Quaternion.LookRotation(new Vector3(1f, 0, 1f)));
            Instantiate(Arroow, transform.position + new Vector3(2f, 2f, -2f), Quaternion.LookRotation(new Vector3(1f, 0, -1f)));
            Instantiate(Arroow, transform.position + new Vector3(-2f, 2f, 2f), Quaternion.LookRotation(new Vector3(-1f, 0, 1f)));
            Instantiate(Arroow, transform.position + new Vector3(-2f, 2f, -2f), Quaternion.LookRotation(new Vector3(-1f, 0, -1f)));
            Instantiate(DestroyEffect, transform.position, Quaternion.LookRotation(new Vector3(0f,1f,0f)));

            this.gameObject.SetActive(false);

            Invoke("Trap_act", 1f);
           


        }
    }

    void Trap_act()
    {
        int rand_value = Random.Range(1, 100);

        if (rand_value >= 85 && rand_value <= 89)
        {

            Instantiate(DashTile, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (rand_value >= 90 && rand_value <= 95)
        {
            Instantiate(FlameTile, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (rand_value >= 4 && rand_value <= 10)
        {
            Instantiate(GunTile, transform.position, Quaternion.LookRotation(new Vector3(0f,0f,-1f)));
            Destroy(this.gameObject);
        }
        else if (rand_value >= 1 && rand_value <= 3)
        {
            Instantiate(AggroTile, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(true);
            transform.position = new Vector3(Random.Range(-68, 65), transform.position.y, Random.Range(-300, -162));
        }
    }
}

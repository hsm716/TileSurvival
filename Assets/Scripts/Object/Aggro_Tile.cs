using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro_Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject Trap;
    [SerializeField]
    private GameObject DestroyEffect;
    [SerializeField]
    private GameObject Aggro;

    Vector3 Aggro_offset = new Vector3(0f, 2f, 0f);
    int AggroN = 2;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Aggro, transform.position+Aggro_offset, transform.rotation);
            AggroN -= 1;
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            if (AggroN == 0)
            {
                Instantiate(Trap, transform.position, transform.rotation);

                Destroy(this.gameObject);
            }
        }
    }

}

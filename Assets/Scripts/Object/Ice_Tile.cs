using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject Ice;
    private Vector3 position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(Ice, new Vector3(transform.position.x+5f,transform.position.y+2f,transform.position.z+5f), Quaternion.LookRotation(new Vector3(1f, 0, 1f)));
            Instantiate(Ice, new Vector3(transform.position.x-5f, transform.position.y+2f, transform.position.z-5f), Quaternion.LookRotation(new Vector3(-1f, 0, -1f)));
            Instantiate(Ice, new Vector3(transform.position.x+5f, transform.position.y+2f, transform.position.z-5f), Quaternion.LookRotation(new Vector3(1f, 0, -1f)));
            Instantiate(Ice, new Vector3(transform.position.x-5f, transform.position.y+2f, transform.position.z+5f), Quaternion.LookRotation(new Vector3(-1f, 0, 1f)));



        }
    }
}

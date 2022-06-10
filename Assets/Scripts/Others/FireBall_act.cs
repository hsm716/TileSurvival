using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_act : MonoBehaviour
{
    public GameObject Explosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("floor"))
        {
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}

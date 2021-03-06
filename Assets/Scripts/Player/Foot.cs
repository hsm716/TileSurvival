using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem dust;
    private AudioSource foot1;

    void Awake()
    {
        foot1 = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("floor"))
        {
            foot1.Play();
            dust.Play();
        }
    }

}

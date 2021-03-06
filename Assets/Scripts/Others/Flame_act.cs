using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame_act : MonoBehaviour
{
    [SerializeField]
    private AudioSource euk;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(5f,0, Quaternion.identity);
            euk.Play();
        }
    }
}

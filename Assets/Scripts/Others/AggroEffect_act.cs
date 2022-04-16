using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroEffect_act : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().EnemyHp -= 15f;
        }
    }
}

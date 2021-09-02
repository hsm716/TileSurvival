﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_act : MonoBehaviour
{
    [SerializeField]
    private AudioSource euk;
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().EnemyHp -= 20f;
            euk.Play();
        }
    }
}

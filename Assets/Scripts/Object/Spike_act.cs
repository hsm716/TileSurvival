﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_act : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().EnemyHp -= 10f;
        }
    }
}

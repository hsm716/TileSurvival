using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro_act : MonoBehaviour
{
    [SerializeField]
    private GameObject AggroEffect;
    void Awake()
    {
        StartCoroutine(aggro_effect(3f));
    }

    IEnumerator aggro_effect(float sec)
    {
        yield return new WaitForSeconds(sec);
        Instantiate(AggroEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}
